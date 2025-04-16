using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project1.Migrations
{
    /// <inheritdoc />
    public partial class fkVisitIsConfusing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Visits_CustomerId",
                table: "Visits",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_StoreId",
                table: "Visits",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Customers_CustomerId",
                table: "Visits",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Stores_StoreId",
                table: "Visits",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreNumber",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Customers_CustomerId",
                table: "Visits");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Stores_StoreId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_CustomerId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_StoreId",
                table: "Visits");
        }
    }
}
