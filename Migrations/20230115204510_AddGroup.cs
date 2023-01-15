using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymSharp.Migrations
{
    /// <inheritdoc />
    public partial class AddGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                            table: "MuscleGroup",
                            columns: new[] { "Group" },
                            values: new object[,]
                            {
                                { "legs" },

                            }

                            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
