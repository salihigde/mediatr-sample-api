using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediatrSampleApi.Migrations
{
    /// <summary>
    /// </summary>
    public partial class InitialCreate : Migration
    {
        /// <summary>
        /// </summary>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    Email = table.Column<string>(maxLength: 120, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedDate", "Email", "Name" },
                values: new object[] { new Guid("7f422462-d926-4837-b0e2-7d390a33f4e4"), new DateTime(2019, 9, 15, 18, 26, 24, 350, DateTimeKind.Local).AddTicks(5640), "salihigde@gmail.com", "Salih Igde" });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedDate", "CustomerId", "Price" },
                values: new object[] { new Guid("5635eb3b-5d2e-4339-b23a-665e375d2efe"), new DateTime(2019, 9, 15, 18, 26, 24, 356, DateTimeKind.Local).AddTicks(3970), new Guid("7f422462-d926-4837-b0e2-7d390a33f4e4"), 1000m });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedDate", "CustomerId", "Price" },
                values: new object[] { new Guid("afc54779-1641-4ab5-a124-992736bcb515"), new DateTime(2019, 9, 15, 18, 26, 24, 356, DateTimeKind.Local).AddTicks(5060), new Guid("7f422462-d926-4837-b0e2-7d390a33f4e4"), 1200m });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");
        }

        /// <summary>
        /// </summary>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
