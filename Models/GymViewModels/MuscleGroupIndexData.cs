namespace GymSharp.Models.GymViewModels
{
    public class MuscleGroupIndexData
    {
        public IEnumerable<MuscleGroup> MuscleGroups { get; set; }
        public IEnumerable<Exercise> Exercises { get; set; }
        public IEnumerable<Measurement> Measurements { get; set; }
    }
}
