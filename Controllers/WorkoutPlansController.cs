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
    public class WorkoutPlansController : Controller
    {
        private readonly GymContext _context;

        public WorkoutPlansController(GymContext context)
        {
            _context = context;
        }

        // GET: WorkoutPlans
        public async Task<IActionResult> Index()
        {
            var gymContext = _context.WorkoutPlans.Include(w => w.Exercise).Include(w => w.MuscleGroup);
            return View(await gymContext.ToListAsync());
        }

        // GET: WorkoutPlans/Details/5
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
            if (ModelState.IsValid)
            {
                _context.Add(workoutPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MuscleGroupID,ExerciseID")] WorkoutPlan workoutPlan)
        {
            if (id != workoutPlan.ExerciseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workoutPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkoutPlanExists(workoutPlan.ExerciseID))
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
            ViewData["ExerciseID"] = new SelectList(_context.Exercises, "ID", "ID", workoutPlan.ExerciseID);
            ViewData["MuscleGroupID"] = new SelectList(_context.MuscleGroups, "ID", "Group", workoutPlan.MuscleGroupID);
            return View(workoutPlan);
        }

        // GET: WorkoutPlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
            if (workoutPlan != null)
            {
                _context.WorkoutPlans.Remove(workoutPlan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkoutPlanExists(int id)
        {
          return _context.WorkoutPlans.Any(e => e.ExerciseID == id);
        }
    }
}
