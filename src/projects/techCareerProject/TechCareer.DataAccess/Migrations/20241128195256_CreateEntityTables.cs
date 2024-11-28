using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechCareer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateEntityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationDeadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ParticipationText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoEducations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalHour = table.Column<double>(type: "float", nullable: false),
                    IsCertified = table.Column<bool>(type: "bit", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstructorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgrammingLanguage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoEducations", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 224, 134, 234, 10, 107, 54, 131, 227, 108, 40, 211, 204, 148, 166, 97, 61, 92, 61, 194, 253, 152, 74, 169, 126, 39, 47, 212, 208, 156, 56, 37, 90, 83, 189, 218, 114, 172, 222, 89, 208, 241, 245, 147, 71, 253, 68, 224, 124, 37, 136, 186, 148, 86, 40, 249, 251, 61, 222, 37, 69, 57, 236, 204, 51 }, new byte[] { 167, 51, 122, 164, 25, 243, 126, 200, 15, 28, 16, 85, 28, 239, 108, 63, 12, 102, 199, 220, 38, 212, 17, 164, 130, 101, 89, 177, 20, 108, 154, 138, 80, 190, 161, 205, 99, 120, 204, 185, 48, 59, 224, 59, 92, 149, 35, 1, 98, 171, 83, 115, 124, 50, 113, 154, 217, 186, 60, 201, 66, 204, 196, 240, 119, 153, 65, 135, 77, 159, 163, 107, 141, 194, 180, 10, 205, 43, 42, 147, 86, 101, 95, 239, 21, 152, 179, 220, 228, 84, 159, 5, 242, 25, 20, 89, 116, 193, 228, 143, 198, 52, 126, 217, 186, 143, 63, 69, 245, 217, 79, 101, 45, 215, 83, 33, 116, 164, 206, 156, 139, 199, 88, 78, 185, 3, 67, 25 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "VideoEducations");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 202, 143, 232, 240, 34, 138, 97, 155, 88, 54, 47, 52, 219, 38, 41, 123, 89, 197, 212, 176, 60, 246, 97, 42, 135, 215, 56, 149, 130, 68, 223, 113, 124, 57, 147, 24, 151, 46, 117, 61, 65, 36, 235, 67, 24, 180, 175, 120, 189, 239, 74, 232, 248, 185, 33, 48, 157, 101, 194, 71, 224, 2, 71, 231 }, new byte[] { 3, 91, 155, 9, 74, 4, 198, 138, 226, 129, 246, 146, 11, 4, 46, 35, 152, 124, 72, 220, 3, 144, 17, 160, 137, 100, 196, 114, 103, 25, 247, 111, 150, 51, 191, 133, 254, 131, 7, 238, 34, 240, 202, 49, 151, 9, 92, 36, 237, 56, 120, 147, 195, 109, 250, 100, 133, 106, 55, 193, 20, 254, 82, 224, 251, 83, 180, 188, 164, 224, 128, 221, 138, 244, 185, 13, 201, 182, 20, 140, 27, 122, 67, 244, 10, 20, 48, 148, 208, 17, 159, 36, 126, 206, 180, 92, 216, 234, 159, 6, 135, 156, 117, 192, 111, 48, 90, 142, 29, 83, 219, 138, 137, 10, 44, 218, 225, 186, 158, 43, 70, 168, 229, 134, 239, 45, 66, 231 } });
        }
    }
}
