using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCExamProject.Migrations
{
    /// <inheritdoc />
    public partial class dropUselessCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedOptionId",
                table: "ExamQuestions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SelectedOptionId",
                table: "ExamQuestions",
                type: "int",
                nullable: true);
        }
    }
}
