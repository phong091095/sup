using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shipping.Migrations
{
    /// <inheritdoc />
    public partial class updateimages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HinhAnh = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IDSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SanPhamIDSanPham = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_SanPham_SanPhamIDSanPham",
                        column: x => x.SanPhamIDSanPham,
                        principalTable: "SanPham",
                        principalColumn: "IDSanPham");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_SanPhamIDSanPham",
                table: "Images",
                column: "SanPhamIDSanPham");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}
