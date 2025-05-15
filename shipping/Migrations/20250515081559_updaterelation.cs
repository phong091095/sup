using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shipping.Migrations
{
    /// <inheritdoc />
    public partial class updaterelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IDCuaHang",
                table: "SanPham",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
            migrationBuilder.AlterColumn<string>(
                      name: "IDCuaHang",
                      table: "ChiTietDVVanChuyen",
                      type: "nvarchar(450)",
                      maxLength: 450,
                      nullable: true,
                      oldClrType: typeof(string),
                      oldType: "nvarchar(max)",
                      oldNullable: true);
            migrationBuilder.AddForeignKey(
                   name: "FK_SanPham_CuaHang_IDCuaHang",
                   table: "SanPham",
                   column: "IDCuaHang",
                   principalTable: "CuaHang",
                   principalColumn: "IDCuaHang",
                   onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                   name: "FK_ChitietDVVC_CuaHang_IDCuaHang",
                   table: "ChiTietDVVanChuyen",
                   column: "IDCuaHang",
                   principalTable: "CuaHang",
                   principalColumn: "IDCuaHang",
                   onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                   name: "FK_CuaHang_User_IDUser",
                   table: "CuaHang",
                   column: "ID",
                   principalTable: "AspNetUsers",
                   principalColumn: "ID",
                   onDelete: ReferentialAction.Cascade);

        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
