using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymSharp.Data;
using GymSharp.Models;

namespace GymSharp.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly GymContext _context;

        public ExercisesController(GymContext context)
        {
            _context = context;
        }

        // GET: Exercises INDEX
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            IQueryable<Exercise> exercise = _context.Exercises.AsNoTracking();
            ViewData["CurrentSort"] = sortOrder;
            ViewData["ExerciseNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "exercise_name_desc" : "exercise_name";
            ViewData["DifficultyLevelSortParm"] = sortOrder == "difficulty_level" ? "difficulty_level_desc" : "difficulty_level";

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
                case "difficulty_level_desc":
                    exercise = exercise.OrderByDescending(x => x.DifficultyLevel);
                    break;
                case "difficulty_level":
                    exercise = exercise.OrderBy(x => x.DifficultyLevel);
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
            var trainers = _context.Trainer.Select(x => new
            {
                x.Id,
                FullName = x.FirstName + " " + x.LastName
            });
            ViewData["TrainerId"] = new SelectList(trainers, "Id", "FullName");
            return View();

        }



        // POST: Exercises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ExerciseName,TrainerId,Description,DifficultyLevel")] Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrainerId"] = new SelectList(_context.Trainer, "Id", "Id", exercise.TrainerId);
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
            ViewData["TrainerId"] = new SelectList(_context.Trainer, "Id", "Id", exercise.TrainerId);
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

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseExists(exercise.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrainerId"] = new SelectList(_context.Trainer, "Id", "Id", exercise.TrainerId);
            return View(exercise);
        }

        // GET: Exercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
            if (exercise != null)
            {
                _context.Exercises.Remove(exercise);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseExists(int id)
        {
          return _context.Exercises.Any(e => e.ID == id);
        }
    }
}
