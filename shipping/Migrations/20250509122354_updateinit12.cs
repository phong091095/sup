using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shipping.Migrations
{
    /// <inheritdoc />
    public partial class updateinit12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChiTietDVVanChuyen",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDCuaHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDDonViVanChuyen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhiVanChuyen = table.Column<int>(type: "int", nullable: false),
                    ThoiGianDuKien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDVVanChuyen", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DonViVanChuyen",
                columns: table => new
                {
                    IDDonViVanChuyen = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenDonVi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonViVanChuyen", x => x.IDDonViVanChuyen);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietDVVanChuyen");

            migrationBuilder.DropTable(
                name: "DonViVanChuyen");
        }
    }
}
