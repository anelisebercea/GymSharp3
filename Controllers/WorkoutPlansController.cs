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

namespace GymSharp.Controllers
{
    [Authorize(Policy = "OnlyCertified")]
    public class WorkoutPlansController : Controller
    {
        private readonly GymContext _context;

        public WorkoutPlansController(GymContext context)
        {
            _context = context;
        }

        // GET: WorkoutPlans
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var gymContext = _context.WorkoutPlans.Include(w => w.Exercise).Include(w => w.MuscleGroup);
            return View(await gymContext.ToListAsync());
        }

        // GET: WorkoutPlans/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WorkoutPlans == null)
            {
                return NotFound();
            }

            var workoutPlan = await _context.WorkoutPlans
                .Include(w => w.Exercise)
                .Include(w => w.MuscleGroup)
                .FirstOrDefaultAsync(m => m.ExerciseID == id);
            if (workoutPlan == null)
            {
                return NotFound();
            }

            return View(workoutPlan);
        }

        // GET: WorkoutPlans/Create
        public IActionResult Create()
        {
            ViewData["ExerciseID"] = new SelectList(_context.Exercises, "ID", "ExerciseName");
            ViewData["MuscleGroupID"] = new SelectList(_context.MuscleGroups, "ID", "Group");
            return View();
        }

        // POST: WorkoutPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MuscleGroupID,ExerciseID")] WorkoutPlan workoutPlan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(workoutPlan);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(DbUpdateException) 
            {
                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists ");
            }
            ViewData["ExerciseID"] = new SelectList(_context.Exercises, "ID", "ID", workoutPlan.ExerciseID);
            ViewData["MuscleGroupID"] = new SelectList(_context.MuscleGroups, "ID", "Group", workoutPlan.MuscleGroupID);
            return View(workoutPlan);
        }

        // GET: WorkoutPlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WorkoutPlans == null)
            {
                return NotFound();
            }

            var workoutPlan = await _context.WorkoutPlans.FindAsync(id);
            if (workoutPlan == null)
            {
                return NotFound();
            }
            ViewData["ExerciseID"] = new SelectList(_context.Exercises, "ID", "ID", workoutPlan.ExerciseID);
            ViewData["MuscleGroupID"] = new SelectList(_context.MuscleGroups, "ID", "Group", workoutPlan.MuscleGroupID);
            return View(workoutPlan);
        }

        // POST: WorkoutPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var workoutPlanToUpdate = await _context.WorkoutPlans.FirstOrDefaultAsync(s => s.MuscleGroupID == id);
            
            if (await TryUpdateModelAsync<WorkoutPlan>(workoutPlanToUpdate))
            {
                try
                {
                    
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists");
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExerciseID"] = new SelectList(_context.Exercises, "ID", "ID", workoutPlanToUpdate.ExerciseID);
            ViewData["MuscleGroupID"] = new SelectList(_context.MuscleGroups, "ID", "Group", workoutPlanToUpdate.MuscleGroupID);
            return View(workoutPlanToUpdate);

        }

        // GET: WorkoutPlans/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null || _context.WorkoutPlans == null)
            {
                return NotFound();
            }

            var workoutPlan = await _context.WorkoutPlans
                .AsNoTracking()
                .Include(w => w.Exercise)
                .Include(w => w.MuscleGroup)
                .FirstOrDefaultAsync(m => m.ExerciseID == id);
            if (workoutPlan == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed. Try again";
            }
            return View(workoutPlan);
        }

        // POST: WorkoutPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WorkoutPlans == null)
            {
                return Problem("Entity set 'GymContext.WorkoutPlans'  is null.");
            }
            var workoutPlan = await _context.WorkoutPlans.FindAsync(id);
            if (workoutPlan == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.WorkoutPlans.Remove(workoutPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
            
        }

        private bool WorkoutPlanExists(int id)
        {
          return _context.WorkoutPlans.Any(e => e.ExerciseID == id);
        }
    }
}
