using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectBlogs.Migrations
{
    public partial class updatePhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhotoUrl",
                table: "Blogs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhotoUrl",
                table: "Blogs",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
