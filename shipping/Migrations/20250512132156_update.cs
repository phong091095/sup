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
            migrationBuilder.DropColumn(
                name: "HinhAnhChinh",
                table: "SanPham");

            migrationBuilder.DropColumn(
                name: "HinhAnhBienThe",
                table: "BienTheSanPham");

            migrationBuilder.AddColumn<int>(
                name: "IDHinhAnh",
                table: "BienTheSanPham",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BienTheSanPham_IDHinhAnh",
                table: "BienTheSanPham",
                column: "IDHinhAnh");

            migrationBuilder.AddForeignKey(
                name: "FK_BienTheSanPham_Images_IDHinhAnh",
                table: "BienTheSanPham",
                column: "IDHinhAnh",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BienTheSanPham_Images_IDHinhAnh",
                table: "BienTheSanPham");

            migrationBuilder.DropIndex(
                name: "IX_BienTheSanPham_IDHinhAnh",
                table: "BienTheSanPham");

            migrationBuilder.DropColumn(
                name: "IDHinhAnh",
                table: "BienTheSanPham");

            migrationBuilder.AddColumn<byte[]>(
                name: "HinhAnhChinh",
                table: "SanPham",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "HinhAnhBienThe",
                table: "BienTheSanPham",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
