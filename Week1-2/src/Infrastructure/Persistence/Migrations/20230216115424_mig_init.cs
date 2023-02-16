using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedDate", "Description", "IsActive", "Name", "UnitPrice", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("212ba8fe-3016-41db-a510-de4cf0e05c02"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Expensive prdouct", true, "Product 5", 48m, null },
                    { new Guid("6c17b68d-1041-4b57-a8c0-ed51371a2b0b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "Product 1", 15m, null },
                    { new Guid("9a8815bc-11cc-48bd-abf9-0fcec82adf0b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "Product 2", 23m, null },
                    { new Guid("e0e2173f-7663-41d6-8d1f-765fed3e343b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is product 4", true, "Product 4", 20m, null },
                    { new Guid("eabbed82-6c11-4419-b7ae-cf1af7a94cfd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is product 3", true, "Product 3", 12m, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
