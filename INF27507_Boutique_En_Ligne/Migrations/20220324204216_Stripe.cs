using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INF27507_Boutique_En_Ligne.Migrations
{
    public partial class Stripe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Stripe");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "PayPal");
        }
    }
}
