using System.ComponentModel.DataAnnotations;

namespace GymSharp.Models
{
    public class MuscleGroup
    {
        public int ID { get; set; }
       
        public string Group { get; set; }

        public ICollection<WorkoutPlan> WorkoutPlans { get; set; }
        

    }
}