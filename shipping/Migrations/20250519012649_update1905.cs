using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shipping.Migrations
{
    /// <inheritdoc />
    public partial class update1905 : Migration
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
                oldType: "nvarchar(max)"
                );
            migrationBuilder.AddForeignKey(
                name: "FK_Images_SanPham_IDSanPham",
                table: "Images",
                column: "IDSanPham",
                principalTable: "SanPham",
                principalColumn: "IDSanPham");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
