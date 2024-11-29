using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechCareer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class VideoEducationUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsCertified",
                table: "VideoEducations",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 72, 96, 129, 252, 40, 43, 239, 105, 129, 86, 65, 196, 191, 12, 205, 175, 6, 205, 157, 59, 159, 188, 163, 250, 177, 230, 248, 209, 132, 180, 200, 107, 37, 5, 20, 54, 239, 201, 226, 176, 34, 21, 83, 185, 65, 204, 72, 174, 90, 104, 164, 136, 234, 137, 193, 45, 198, 93, 14, 181, 4, 62, 213, 161 }, new byte[] { 128, 109, 91, 213, 186, 12, 34, 43, 228, 162, 178, 21, 202, 49, 142, 239, 241, 207, 180, 168, 23, 69, 207, 18, 36, 47, 45, 150, 97, 229, 118, 243, 87, 90, 123, 161, 170, 217, 248, 200, 49, 84, 162, 114, 138, 198, 107, 153, 68, 116, 75, 188, 240, 231, 206, 56, 253, 244, 143, 86, 134, 197, 193, 87, 108, 146, 202, 217, 40, 88, 250, 118, 221, 188, 209, 22, 249, 27, 182, 6, 100, 123, 16, 211, 169, 106, 0, 172, 190, 245, 236, 92, 235, 230, 171, 232, 241, 164, 42, 41, 36, 251, 179, 59, 12, 70, 220, 1, 60, 98, 19, 138, 88, 72, 53, 213, 13, 51, 132, 119, 129, 21, 147, 207, 109, 39, 10, 52 } });

            migrationBuilder.CreateIndex(
                name: "IX_VideoEducations_InstructorId",
                table: "VideoEducations",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoEducations_Instructors_InstructorId",
                table: "VideoEducations",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoEducations_Instructors_InstructorId",
                table: "VideoEducations");

            migrationBuilder.DropIndex(
                name: "IX_VideoEducations_InstructorId",
                table: "VideoEducations");

            migrationBuilder.AlterColumn<bool>(
                name: "IsCertified",
                table: "VideoEducations",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 224, 134, 234, 10, 107, 54, 131, 227, 108, 40, 211, 204, 148, 166, 97, 61, 92, 61, 194, 253, 152, 74, 169, 126, 39, 47, 212, 208, 156, 56, 37, 90, 83, 189, 218, 114, 172, 222, 89, 208, 241, 245, 147, 71, 253, 68, 224, 124, 37, 136, 186, 148, 86, 40, 249, 251, 61, 222, 37, 69, 57, 236, 204, 51 }, new byte[] { 167, 51, 122, 164, 25, 243, 126, 200, 15, 28, 16, 85, 28, 239, 108, 63, 12, 102, 199, 220, 38, 212, 17, 164, 130, 101, 89, 177, 20, 108, 154, 138, 80, 190, 161, 205, 99, 120, 204, 185, 48, 59, 224, 59, 92, 149, 35, 1, 98, 171, 83, 115, 124, 50, 113, 154, 217, 186, 60, 201, 66, 204, 196, 240, 119, 153, 65, 135, 77, 159, 163, 107, 141, 194, 180, 10, 205, 43, 42, 147, 86, 101, 95, 239, 21, 152, 179, 220, 228, 84, 159, 5, 242, 25, 20, 89, 116, 193, 228, 143, 198, 52, 126, 217, 186, 143, 63, 69, 245, 217, 79, 101, 45, 215, 83, 33, 116, 164, 206, 156, 139, 199, 88, 78, 185, 3, 67, 25 } });
        }
    }
}
