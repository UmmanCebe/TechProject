using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechCareer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CategoryConfigurationChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Categories_CategoryId",
                table: "Events");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 218, 89, 253, 245, 209, 63, 151, 28, 32, 199, 31, 238, 184, 211, 98, 144, 171, 63, 225, 63, 148, 201, 178, 162, 105, 183, 112, 169, 44, 83, 176, 32, 100, 108, 164, 174, 83, 7, 186, 134, 19, 183, 220, 113, 138, 109, 2, 180, 39, 102, 23, 135, 138, 237, 214, 141, 16, 53, 209, 243, 67, 63, 1, 77 }, new byte[] { 117, 173, 247, 59, 204, 196, 45, 155, 163, 206, 94, 167, 155, 55, 0, 142, 237, 71, 228, 113, 32, 39, 75, 119, 92, 252, 99, 122, 57, 192, 138, 120, 45, 58, 211, 85, 186, 34, 155, 21, 40, 248, 112, 132, 142, 111, 46, 5, 240, 229, 50, 133, 86, 174, 1, 69, 145, 245, 95, 180, 176, 1, 155, 148, 46, 223, 130, 238, 129, 190, 229, 211, 26, 8, 39, 30, 77, 31, 148, 40, 246, 30, 179, 241, 185, 53, 236, 115, 25, 177, 224, 232, 115, 172, 98, 44, 194, 97, 135, 128, 50, 18, 46, 111, 118, 44, 45, 158, 252, 152, 191, 72, 67, 55, 148, 142, 98, 197, 19, 34, 153, 123, 69, 109, 112, 38, 49, 224 } });

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Categories_CategoryId",
                table: "Events",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Categories_CategoryId",
                table: "Events");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 188, 171, 241, 189, 247, 38, 234, 208, 39, 128, 45, 59, 27, 135, 2, 126, 8, 22, 38, 167, 116, 228, 83, 229, 227, 69, 34, 61, 118, 232, 186, 152, 241, 132, 87, 30, 44, 59, 228, 179, 31, 68, 131, 175, 185, 227, 26, 176, 233, 105, 243, 183, 175, 13, 194, 141, 45, 231, 219, 53, 88, 118, 61, 77 }, new byte[] { 83, 42, 49, 251, 106, 178, 20, 13, 75, 250, 53, 134, 168, 107, 130, 22, 150, 0, 29, 231, 205, 184, 250, 17, 140, 125, 126, 28, 203, 123, 218, 143, 177, 172, 188, 222, 247, 160, 79, 120, 182, 215, 121, 84, 178, 191, 11, 69, 127, 156, 7, 39, 246, 73, 22, 86, 5, 225, 195, 70, 45, 189, 59, 211, 247, 0, 242, 29, 203, 167, 39, 201, 9, 252, 232, 213, 18, 31, 252, 203, 37, 16, 88, 12, 197, 148, 26, 13, 113, 88, 5, 254, 223, 155, 253, 200, 224, 0, 15, 214, 109, 202, 96, 220, 136, 87, 96, 231, 145, 53, 4, 10, 58, 79, 37, 210, 90, 140, 76, 34, 91, 89, 206, 215, 145, 252, 203, 244 } });

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Categories_CategoryId",
                table: "Events",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
