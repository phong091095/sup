using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shipping.Migrations
{
    /// <inheritdoc />
    public partial class createinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DanhMuc",
                columns: table => new
                {
                    IDDanhMuc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CapDanhMuc = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsLeaf = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMuc", x => x.IDDanhMuc);
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

            migrationBuilder.CreateTable(
                name: "ThuocTinhBTSP",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenThuocTinh = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuocTinhBTSP", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    IDSanPham = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDDanhMuc = table.Column<int>(type: "int", nullable: false),
                    IDCuaHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HinhAnhChinh = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.IDSanPham);
                    table.ForeignKey(
                        name: "FK_SanPham_DanhMuc_IDDanhMuc",
                        column: x => x.IDDanhMuc,
                        principalTable: "DanhMuc",
                        principalColumn: "IDDanhMuc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDVVanChuyen",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDCuaHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDDonViVanChuyen = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhiVanChuyen = table.Column<int>(type: "int", nullable: false),
                    ThoiGianDuKien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDVVanChuyen", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ChiTietDVVanChuyen_DonViVanChuyen_IDDonViVanChuyen",
                        column: x => x.IDDonViVanChuyen,
                        principalTable: "DonViVanChuyen",
                        principalColumn: "IDDonViVanChuyen",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GiaTriBTSP",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDThuocTinh = table.Column<int>(type: "int", nullable: false),
                    TenGiaTri = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaTriBTSP", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GiaTriBTSP_ThuocTinhBTSP_IDThuocTinh",
                        column: x => x.IDThuocTinh,
                        principalTable: "ThuocTinhBTSP",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BienTheSanPham",
                columns: table => new
                {
                    IDBienTheSanPham = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HinhAnhBienThe = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    IDSanPham = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BienTheSanPham", x => x.IDBienTheSanPham);
                    table.ForeignKey(
                        name: "FK_BienTheSanPham_SanPham_IDSanPham",
                        column: x => x.IDSanPham,
                        principalTable: "SanPham",
                        principalColumn: "IDSanPham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietBienTheSanPham",
                columns: table => new
                {
                    IDChiTietBTSanPham = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDBienTheSanPham = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDGiaTriBienTheSanPham = table.Column<int>(type: "int", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietBienTheSanPham", x => x.IDChiTietBTSanPham);
                    table.ForeignKey(
                        name: "FK_ChiTietBienTheSanPham_BienTheSanPham_IDBienTheSanPham",
                        column: x => x.IDBienTheSanPham,
                        principalTable: "BienTheSanPham",
                        principalColumn: "IDBienTheSanPham",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietBienTheSanPham_GiaTriBTSP_IDGiaTriBienTheSanPham",
                        column: x => x.IDGiaTriBienTheSanPham,
                        principalTable: "GiaTriBTSP",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BienTheSanPham_IDSanPham",
                table: "BienTheSanPham",
                column: "IDSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBienTheSanPham_IDBienTheSanPham",
                table: "ChiTietBienTheSanPham",
                column: "IDBienTheSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBienTheSanPham_IDGiaTriBienTheSanPham",
                table: "ChiTietBienTheSanPham",
                column: "IDGiaTriBienTheSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDVVanChuyen_IDDonViVanChuyen",
                table: "ChiTietDVVanChuyen",
                column: "IDDonViVanChuyen");

            migrationBuilder.CreateIndex(
                name: "IX_GiaTriBTSP_IDThuocTinh",
                table: "GiaTriBTSP",
                column: "IDThuocTinh");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_IDDanhMuc",
                table: "SanPham",
                column: "IDDanhMuc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietBienTheSanPham");

            migrationBuilder.DropTable(
                name: "ChiTietDVVanChuyen");

            migrationBuilder.DropTable(
                name: "BienTheSanPham");

            migrationBuilder.DropTable(
                name: "GiaTriBTSP");

            migrationBuilder.DropTable(
                name: "DonViVanChuyen");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "ThuocTinhBTSP");

            migrationBuilder.DropTable(
                name: "DanhMuc");
        }
    }
}
