using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymSharp.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkouts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO WorkoutPlan (MuscleGroupID, ExerciseID) VALUES (1, 6)");
            migrationBuilder.Sql("INSERT INTO WorkoutPlan (MuscleGroupID, ExerciseID) VALUES (1, 22)");
            migrationBuilder.Sql("INSERT INTO WorkoutPlan (MuscleGroupID, ExerciseID) VALUES (1, 23)");
            migrationBuilder.Sql("INSERT INTO WorkoutPlan (MuscleGroupID, ExerciseID) VALUES (1, 34)");
            migrationBuilder.Sql("INSERT INTO WorkoutPlan (MuscleGroupID, ExerciseID) VALUES (1, 35)");
            migrationBuilder.Sql("INSERT INTO WorkoutPlan (MuscleGroupID, ExerciseID) VALUES (2, 2)");
            migrationBuilder.Sql("INSERT INTO WorkoutPlan (MuscleGroupID, ExerciseID) VALUES (2, 6)");
            migrationBuilder.Sql("INSERT INTO WorkoutPlan (MuscleGroupID, ExerciseID) VALUES (2, 13)");
       
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
