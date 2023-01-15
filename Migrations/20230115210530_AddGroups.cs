using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymSharp.Migrations
{
    /// <inheritdoc />
    public partial class AddGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Group",
                table: "MuscleGroup",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.InsertData(
                        table: "MuscleGroup",
                        columns: new[] { "Group" },
                        values: new object[,]
                        {
                                { "legs" },
                                { "arms" },
                                { "chest" },
                                { "back" },
                                { "shoulders" },
                                { "core" },

                        }

                        );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Group",
                table: "MuscleGroup",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
