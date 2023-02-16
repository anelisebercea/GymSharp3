using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymSharp.Data;
using GymSharp.Models;
using GymSharp.Models.GymViewModels;
using System.Security.Policy;
using Microsoft.AspNetCore.Authorization;

namespace GymSharp.Controllers
{
    [Authorize(Policy = "OnlyCertified")]
    public class MuscleGroupsController : Controller
    {
        private readonly GymContext _context;

        public MuscleGroupsController(GymContext context)
        {
            _context = context;
        }

        // GET: MuscleGroups INDEX
        /*
        public async Task<IActionResult> Index()
        {
              return View(await _context.MuscleGroups.ToListAsync());
        }
        */

        [AllowAnonymous]
        public async Task<IActionResult> Index(int? id, int? exerciseID)
        {
            var viewModel = new MuscleGroupIndexData();
            viewModel.MuscleGroups = await _context.MuscleGroups
            .Include(i => i.WorkoutPlans)
            .ThenInclude(i => i.Exercise)
            .ThenInclude(i => i.Measurements)
            .ThenInclude(i => i.User)
            .AsNoTracking()
            .OrderBy(i => i.Group)
            .ToListAsync();
            if (id != null)
            {
                ViewData["MuscleGroupID"] = id.Value;
                MuscleGroup musclegroup = viewModel.MuscleGroups.Where(
                i => i.ID == id.Value).Single();
                viewModel.Exercises = musclegroup.WorkoutPlans.Select(s => s.Exercise);
            }
            if (exerciseID != null)
            {
                ViewData["ExerciseID"] = exerciseID.Value;
                viewModel.Measurements = viewModel.Exercises.Where(
                x => x.ID == exerciseID).Single().Measurements;
            }
            return View(viewModel);
        }






        // GET: MuscleGroups/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MuscleGroups == null)
            {
                return NotFound();
            }

            var muscleGroup = await _context.MuscleGroups
                .FirstOrDefaultAsync(m => m.ID == id);
            if (muscleGroup == null)
            {
                return NotFound();
            }

            return View(muscleGroup);
        }

        // GET: MuscleGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MuscleGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Group")] MuscleGroup muscleGroup)
        {
            if (ModelState.IsValid)
            {
                _context.MuscleGroups.Add(muscleGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(muscleGroup);
        }

        // GET: MuscleGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MuscleGroups == null)
            {
                return NotFound();
            }

            var muscleGroup = await _context.MuscleGroups
                .Include(i => i.WorkoutPlans).ThenInclude(i => i.Exercise)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (muscleGroup == null)
            {
                return NotFound();
            }
            PopulateWorkoutPlanData(muscleGroup);

            return View(muscleGroup);
        }

        private void PopulateWorkoutPlanData(MuscleGroup muscleGroup)
        {
            var allBooks = _context.Exercises;
            var musclegroupExercises = new HashSet<int>(muscleGroup.WorkoutPlans.Select(c => c.ExerciseID));
            var viewModel = new List<WorkoutPlanData>();
            foreach (var exercise in allBooks)
            {
                viewModel.Add(new WorkoutPlanData
                {
                    ExerciseID = exercise.ID,
                    ExerciseName = exercise.ExerciseName,
                    IsPlanned = musclegroupExercises.Contains(exercise.ID)
                });
            }
            ViewData["Exercises"] = viewModel;
        }


        // POST: MuscleGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedExercises)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muscleGroupToUpdate = await _context.MuscleGroups
                .Include(i => i.WorkoutPlans)
                .ThenInclude(i => i.Exercise)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (await TryUpdateModelAsync<MuscleGroup>( muscleGroupToUpdate, "", i => i.Group))
            {
                UpdateWorkoutPlans(selectedExercises, muscleGroupToUpdate);
                try
                {
                    
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists, ");

                }
                return RedirectToAction(nameof(Index));
            }
            UpdateWorkoutPlans(selectedExercises, muscleGroupToUpdate);
            PopulateWorkoutPlanData(muscleGroupToUpdate);

            return View(muscleGroupToUpdate);
        }


        private void UpdateWorkoutPlans(string[] selectedExercises, MuscleGroup muscleGroupToUpdate)
        {
            if (selectedExercises == null)
            {
                muscleGroupToUpdate.WorkoutPlans = new List<WorkoutPlan>();
                return;
            }
            var selectedExercisesHS = new HashSet<string>(selectedExercises);
            var workoutPlans = new HashSet<int>
            (muscleGroupToUpdate.WorkoutPlans.Select(c => c.Exercise.ID));
            foreach (var exercise in _context.Exercises)
            {
                if (selectedExercisesHS.Contains(exercise.ID.ToString()))
                {
                    if (!workoutPlans.Contains(exercise.ID))
                    {
                        muscleGroupToUpdate.WorkoutPlans.Add(new WorkoutPlan
                        {
                            MuscleGroupID = muscleGroupToUpdate.ID,
                            ExerciseID = exercise.ID
                        });
                    }
                }
                else
                {
                    if (workoutPlans.Contains(exercise.ID))
                    {
                        WorkoutPlan exerciseToRemove = muscleGroupToUpdate.WorkoutPlans.FirstOrDefault(i => i.ExerciseID == exercise.ID);
                        _context.Remove(exerciseToRemove);
                    }
                }
            }
        }



        // GET: MuscleGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MuscleGroups == null)
            {
                return NotFound();
            }

            var muscleGroup = await _context.MuscleGroups
                .FirstOrDefaultAsync(m => m.ID == id);
            if (muscleGroup == null)
            {
                return NotFound();
            }

            return View(muscleGroup);
        }

        // POST: MuscleGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MuscleGroups == null)
            {
                return Problem("Entity set 'GymContext.MuscleGroups'  is null.");
            }
            var muscleGroup = await _context.MuscleGroups.FindAsync(id);
            if (muscleGroup != null)
            {
                _context.MuscleGroups.Remove(muscleGroup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MuscleGroupExists(int id)
        {
          return _context.MuscleGroups.Any(e => e.ID == id);
        }
    }
}
