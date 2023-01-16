using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymSharp.Migrations
{
    /// <inheritdoc />
    public partial class AddExercise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.InsertData(
                table: "Trainer",
                columns: new[] { "Id", "FirstName", "LastName", "Specialization", "Qualification" },
                values: new object[,]
                {
                    { 1, "David", "Jones", "Strength", "Expert" },
                    { 2, "Sarah", "Tomson", "Yoga", "Beginner" },
                    { 3, "John", "Smith", "Strength", "Intermediate" },
                    { 4, "Michael", "Williams", "Crossfit", "Beginner" },
                    { 5, "Jennifer", "Jones", "Yoga", "Expert" },
                    { 6, "Nicholas", "Garcia", "Crossfit", "Expert" },
                    { 7, "James", "Brown", "Strength", "Beginner" },
                    { 8, "Elizabeth", "Bayton", "Yoga", "Intermediate" },
                    { 9, "Tom", "Anderson", "Crossfit", "Intermediate" },

                 }

                );


            migrationBuilder.InsertData(
                table: "Exercise",
                columns: new[] { "ExerciseName", "TrainerId", "Description", "DifficultyLevel" },
                values: new object[,]
                {
                    { "Bicep Curl", 7, "Isolation exercise for biceps", "easy" },
                    { "Tricep Dips", 9, "Isolation exercise for triceps", "moderate" },
                    { "Hammer Curl", 3, "Isolation exercise for biceps", "moderate" },
                    { "Deadlift", 1, "A compound movement that works your lower back, glutes, hamstrings, and core.", "hard" },
                    { "Bench Press", 3, "A compound movement that works your chest, triceps, and shoulders.", "moderate"},
                    { "Cable Fly", 2, "Isolation exercise for chest", "easy" },
                    { "Incline bench press", 3, "Compound exercise for upper chest", "moderate" },
                    { "Dumbbell press", 1, "Isolation exercise for chest", "hard" },
                    { "Cable Crossover", 8, "Isolation exercise for chest", "moderate" },
                    { "Push ups", 4, "Bodyweight exercise for chest", "easy" },
                    { "Dumbbell Fly", 3, "Isolation exercise for chest", "moderate" },
                    { "Incline Dumbbell Fly", 1, "Isolation exercise for upper chest", "hard" },
                    { "Barbell press", 1, "Compound exercise for chest", "hard" },
                    { "Lunges", 2, "Compound exercise for legs and glutes", "easy" },
                    { "Squat", 8, "Compound exercise for legs and glutes", "hard" },
                    { "Leg press", 6, "Compound exercise for legs and glutes", "hard" },
                    { "Calf raise", 7, "Isolation exercise for calf muscles", "easy" },
                    { "Seated leg curl", 9, "Isolation exercise for hamstrings", "moderate" },
                    { "Standing leg curl", 3, "Isolation exercise for hamstrings", "moderate" },
                    { "Romanian deadlift", 1, "Isolation exercise for hamstrings", "hard" },
                    { "Back Extension", 3, "Isolation exercise for lower back", "easy" },
                    { "Good mornings", 2, "Isolation exercise for lower back", "hard" },
                    { "Pull ups", 8, "Isolation exercise for back", "hard" },
                    { "Chin ups", 4, "Isolation exercise for back", "easy" },
                    { "Lat pulldown", 3, "Isolation exercise for back", "moderate" },
                    { "Bent over rows", 1, "Compound exercise for back", "hard" },
                    { "Shrugs", 1, "Isolation exercise for traps", "easy" },
                    { "Upright rows", 3, "Isolation exercise for shoulders", "moderate" },
                    { "Seated military press", 8, "Compound exercise for shoulders", "hard" },
                    { "Side lateral raise", 2, "Isolation exercise for shoulders", "easy" },
                    { "Front raise", 3, "Isolation exercise for shoulders", "moderate" },
                    { "Rear delt fly", 1, "Isolation exercise for shoulders", "easy" },
                    { "Barbell curl", 3, "Isolation exercise for biceps", "moderate" },
                    { "Dumbbell curl", 2, "Isolation exercise for biceps", "easy" },
                    { "Concentration curl", 9, "Isolation exercise for biceps", "moderate" },
                    { "Preacher curl", 1, "Isolation exercise for biceps", "hard" },
                    { "Tricep pushdown", 8, "Isolation exercise for triceps", "easy" },
                    { "Tricep extension", 4, "Isolation exercise for triceps", "easy" },
                    { "Tricep dip", 3, "Isolation exercise for triceps", "moderate" },
                    { "Skull crusher", 1, "Isolation exercise for triceps", "hard" }
                 }
                
                ); 
            

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
