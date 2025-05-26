using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shipping.Migrations
{
    /// <inheritdoc />
    public partial class updatedanhmuc1905 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "DanhMuc");

            migrationBuilder.CreateTable(
                name: "DanhMucChild",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDDanhMuc = table.Column<int>(type: "int", nullable: false),
                    Images = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucChild", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DanhMucChild_DanhMuc_IDDanhMuc",
                        column: x => x.IDDanhMuc,
                        principalTable: "DanhMuc",
                        principalColumn: "IDDanhMuc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DanhMucImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDDanhMuc = table.Column<int>(type: "int", nullable: false),
                    Images = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DanhMucImages_DanhMuc_IDDanhMuc",
                        column: x => x.IDDanhMuc,
                        principalTable: "DanhMuc",
                        principalColumn: "IDDanhMuc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DanhMucChild_IDDanhMuc",
                table: "DanhMucChild",
                column: "IDDanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_DanhMucImages_IDDanhMuc",
                table: "DanhMucImages",
                column: "IDDanhMuc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DanhMucChild");

            migrationBuilder.DropTable(
                name: "DanhMucImages");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "DanhMuc",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
