using Microsoft.EntityFrameworkCore.Migrations;

namespace OwnedEntityTypes.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MainAddress_Street = table.Column<string>(nullable: true),
                    MainAddress_City = table.Column<string>(nullable: true),
                    MainAddress_Country = table.Column<string>(nullable: true),
                    SecondaryAddress_Street = table.Column<string>(nullable: true),
                    SecondaryAddress_City = table.Column<string>(nullable: true),
                    SecondaryAddress_Country = table.Column<string>(nullable: true),
                    Phone_Primary = table.Column<string>(nullable: true),
                    Phone_Secondary = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
