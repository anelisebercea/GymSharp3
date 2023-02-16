using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymSharp.Data;
using GymSharp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using GymSharp.Models.GymViewModels;

//using GymSharp.Models.GymViewModels;
//using static System.Reflection.Metadata.BlobBuilder;
//using System.Net;
//using System.Security.Policy;

namespace GymSharp.Controllers
{
    [Authorize(Roles = "Employee, Manager")]
    public class ExercisesController : Controller
    {
        private readonly GymContext _context;

        public ExercisesController(GymContext context)
        {
            _context = context;
        }

        // GET: Exercises INDEX
        [AllowAnonymous]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            IQueryable<Exercise> exercise = _context.Exercises.AsNoTracking();
            ViewData["CurrentSort"] = sortOrder;
            ViewData["ExerciseNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "exercise_name_desc" : "exercise_name";
            ViewData["TrainerSortParm"] = sortOrder == "trainer" ? "trainer_desc" : "trainer";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;


            if (!String.IsNullOrEmpty(searchString))
            {
                exercise = exercise.Where(s => s.DifficultyLevel.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "exercise_name_desc":
                    exercise = exercise.OrderByDescending(x => x.ExerciseName);
                    break;
                case "exercise_name":
                    exercise = exercise.OrderBy(x => x.ExerciseName);
                    break;
                case "trainer_desc":
                    exercise = exercise.OrderByDescending(x => x.TrainerId);
                    break;
                case "trainer":
                    exercise = exercise.OrderBy(x => x.TrainerId);
                    break;
                default:
                    exercise = exercise.OrderBy(x => x.ExerciseName);
                    break;
            }

            int pageSize = 8;
            return View(await PaginatedList<Exercise>.CreateAsync(exercise, pageNumber ?? 1, pageSize));
        }
        // GET: Exercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Exercises == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises
                .Include(e => e.Trainer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // GET: Exercises/Create
        public IActionResult Create()
        {

            /*
            ViewBag.TrainerId = new SelectList(_context.Trainers, "Id", "FullName");

            return View();             
             */
            
            var trainers = _context.Trainers.Select(x => new
            {
                x.Id,
                FullName = x.FirstName + " " + x.LastName
            });
            ViewBag.TrainerId = new SelectList(trainers, "Id", "FullName");
            return View();
            
        }



        // POST: Exercises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ExerciseName,TrainerId,Description,DifficultyLevel")] Exercise exercise)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Exercises.Add(exercise);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex*/)
            {

                ModelState.AddModelError("", "Unable to save changes. " + "Try again");
            }
            ViewBag.TrainerId = new SelectList(_context.Trainers, "Id", "FullName", exercise.TrainerId);

            return View(exercise);
        }

        // GET: Exercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Exercises == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }
            // ViewBag...
            ViewBag.TrainerId = new SelectList(_context.Trainers, "Id", "FullName", exercise.TrainerId);
            
            return View(exercise);
        }
       

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ExerciseName,TrainerId,Description,DifficultyLevel")] Exercise exercise)
        {
            if (id != exercise.ID)
            {
                return NotFound();
            }
            var exerciseToUpdate = await _context.Exercises
                .Include(i => i.Trainer)
                .Include(i => i.WorkoutPlans)
                .FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Exercise>(exerciseToUpdate, "",
                s => s.ExerciseName,
                s => s.TrainerId,
                s => s.Description,
                s => s.DifficultyLevel
                ))
            {
                try
                {
                    _context.Update(exercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again");
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.TrainerId = new SelectList(_context.Trainers, "Id", "FullName", exercise.TrainerId);
            return View(exercise);
        }

        // GET: Exercises/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null || _context.Exercises == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises
                .AsNoTracking()
                .Include(e => e.Trainer)
                .Include(i => i.WorkoutPlans)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (exercise == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed. Try again";
            }

            return View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Exercises == null)
            {
                return Problem("Entity set 'GymContext.Exercises'  is null.");
            }
            var exercise = await _context.Exercises.FindAsync(id);

            if (exercise == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try 
            {
                _context.Exercises.Remove(exercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }

        }

        private bool ExerciseExists(int id)
        {
          return _context.Exercises.Any(e => e.ID == id);
        }



        
        public async Task<ActionResult> Statistics()
        {
            IQueryable<MeasurementGroup> data =
            from measurement in _context.Measurements
            group measurement by measurement.Date into dateGroup
            select new MeasurementGroup()
            {
                Date = dateGroup.Key,
                ExerciseCount = dateGroup.Count()
            };
            return View(await data.AsNoTracking().ToListAsync());
        }
        

        public IActionResult Chat()
        {
            return View();
        }


    }
}
