using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shipping.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_SanPham_SanPhamIDSanPham",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_SanPhamIDSanPham",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "SanPhamIDSanPham",
                table: "Images");

            migrationBuilder.AlterColumn<string>(
                name: "IDSanPham",
                table: "Images",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Images_IDSanPham",
                table: "Images",
                column: "IDSanPham");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_SanPham_IDSanPham",
                table: "Images",
                column: "IDSanPham",
                principalTable: "SanPham",
                principalColumn: "IDSanPham",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_SanPham_IDSanPham",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_IDSanPham",
                table: "Images");

            migrationBuilder.AlterColumn<string>(
                name: "IDSanPham",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "SanPhamIDSanPham",
                table: "Images",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_SanPhamIDSanPham",
                table: "Images",
                column: "SanPhamIDSanPham");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_SanPham_SanPhamIDSanPham",
                table: "Images",
                column: "SanPhamIDSanPham",
                principalTable: "SanPham",
                principalColumn: "IDSanPham");
        }
    }
}
