using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymSharp.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkouts2 : Migration
    {
        /// <inheritdoc />
        
              protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO WorkoutPlan (MuscleGroupID, ExerciseID) VALUES (4, 7)");
            migrationBuilder.Sql("INSERT INTO WorkoutPlan (MuscleGroupID, ExerciseID) VALUES (4, 21)");
            migrationBuilder.Sql("INSERT INTO WorkoutPlan (MuscleGroupID, ExerciseID) VALUES (4, 27)");
            migrationBuilder.Sql("INSERT INTO WorkoutPlan (MuscleGroupID, ExerciseID) VALUES (4, 36)");
            migrationBuilder.Sql("INSERT INTO WorkoutPlan (MuscleGroupID, ExerciseID) VALUES (4, 37)");
            migrationBuilder.Sql("INSERT INTO WorkoutPlan (MuscleGroupID, ExerciseID) VALUES (5, 44)");
            migrationBuilder.Sql("INSERT INTO WorkoutPlan (MuscleGroupID, ExerciseID) VALUES (5, 8)");
            migrationBuilder.Sql("INSERT INTO WorkoutPlan (MuscleGroupID, ExerciseID) VALUES (5, 14)");

        }
    

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
