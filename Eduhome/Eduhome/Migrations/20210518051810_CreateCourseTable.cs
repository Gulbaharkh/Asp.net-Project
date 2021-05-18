using Microsoft.EntityFrameworkCore.Migrations;

namespace Eduhome.Migrations
{
    public partial class CreateCourseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AboutCourse = table.Column<string>(nullable: true),
                    HowToApply = table.Column<string>(nullable: true),
                    Certification = table.Column<string>(nullable: true),
                    Details = table.Column<string>(nullable: true),
                    Start = table.Column<string>(nullable: true),
                    Duration = table.Column<string>(nullable: true),
                    ClassDuration = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    Students = table.Column<string>(nullable: true),
                    Assesments = table.Column<string>(nullable: true),
                    CourseFee = table.Column<int>(nullable: false),
                    CaptionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseDetails_CourseCaptions_CaptionId",
                        column: x => x.CaptionId,
                        principalTable: "CourseCaptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseDetails_CaptionId",
                table: "CourseDetails",
                column: "CaptionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseDetails");
        }
    }
}
