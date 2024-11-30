using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechCareer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class instructor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "About",
                table: "Instructors",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Events",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ParticipationText",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 188, 171, 241, 189, 247, 38, 234, 208, 39, 128, 45, 59, 27, 135, 2, 126, 8, 22, 38, 167, 116, 228, 83, 229, 227, 69, 34, 61, 118, 232, 186, 152, 241, 132, 87, 30, 44, 59, 228, 179, 31, 68, 131, 175, 185, 227, 26, 176, 233, 105, 243, 183, 175, 13, 194, 141, 45, 231, 219, 53, 88, 118, 61, 77 }, new byte[] { 83, 42, 49, 251, 106, 178, 20, 13, 75, 250, 53, 134, 168, 107, 130, 22, 150, 0, 29, 231, 205, 184, 250, 17, 140, 125, 126, 28, 203, 123, 218, 143, 177, 172, 188, 222, 247, 160, 79, 120, 182, 215, 121, 84, 178, 191, 11, 69, 127, 156, 7, 39, 246, 73, 22, 86, 5, 225, 195, 70, 45, 189, 59, 211, 247, 0, 242, 29, 203, 167, 39, 201, 9, 252, 232, 213, 18, 31, 252, 203, 37, 16, 88, 12, 197, 148, 26, 13, 113, 88, 5, 254, 223, 155, 253, 200, 224, 0, 15, 214, 109, 202, 96, 220, 136, 87, 96, 231, 145, 53, 4, 10, 58, 79, 37, 210, 90, 140, 76, 34, 91, 89, 206, 215, 145, 252, 203, 244 } });

            migrationBuilder.CreateIndex(
                name: "IX_Events_CategoryId",
                table: "Events",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Categories_CategoryId",
                table: "Events",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Categories_CategoryId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_CategoryId",
                table: "Events");

            migrationBuilder.AlterColumn<string>(
                name: "About",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ParticipationText",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 72, 96, 129, 252, 40, 43, 239, 105, 129, 86, 65, 196, 191, 12, 205, 175, 6, 205, 157, 59, 159, 188, 163, 250, 177, 230, 248, 209, 132, 180, 200, 107, 37, 5, 20, 54, 239, 201, 226, 176, 34, 21, 83, 185, 65, 204, 72, 174, 90, 104, 164, 136, 234, 137, 193, 45, 198, 93, 14, 181, 4, 62, 213, 161 }, new byte[] { 128, 109, 91, 213, 186, 12, 34, 43, 228, 162, 178, 21, 202, 49, 142, 239, 241, 207, 180, 168, 23, 69, 207, 18, 36, 47, 45, 150, 97, 229, 118, 243, 87, 90, 123, 161, 170, 217, 248, 200, 49, 84, 162, 114, 138, 198, 107, 153, 68, 116, 75, 188, 240, 231, 206, 56, 253, 244, 143, 86, 134, 197, 193, 87, 108, 146, 202, 217, 40, 88, 250, 118, 221, 188, 209, 22, 249, 27, 182, 6, 100, 123, 16, 211, 169, 106, 0, 172, 190, 245, 236, 92, 235, 230, 171, 232, 241, 164, 42, 41, 36, 251, 179, 59, 12, 70, 220, 1, 60, 98, 19, 138, 88, 72, 53, 213, 13, 51, 132, 119, 129, 21, 147, 207, 109, 39, 10, 52 } });
        }
    }
}
