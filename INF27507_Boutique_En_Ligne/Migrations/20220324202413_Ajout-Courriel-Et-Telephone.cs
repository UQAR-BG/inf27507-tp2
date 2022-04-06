using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INF27507_Boutique_En_Ligne.Migrations
{
    public partial class AjoutCourrielEtTelephone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Sellers",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Clients",
                newName: "Phone");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Sellers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Clients",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Phone" },
                values: new object[] { "default.user@gmail.com", "418-888-9999" });

            migrationBuilder.UpdateData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Phone" },
                values: new object[] { "albert.vendeur@gmail.com", "581-456-3263" });

            migrationBuilder.UpdateData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Phone" },
                values: new object[] { "amelie.vendeur@gmail.com", "418-753-1596" });

            migrationBuilder.UpdateData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "Phone" },
                values: new object[] { "julie.vendeur@gmail.com", "918-852-1122" });

            migrationBuilder.UpdateData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Email", "Phone" },
                values: new object[] { "xavier.vendeur@gmail.com", "418-874-5632" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Sellers",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Clients",
                newName: "Username");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "Username",
                value: "Default-User");

            migrationBuilder.UpdateData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Username",
                value: "Albert");

            migrationBuilder.UpdateData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Username",
                value: "Amélie");

            migrationBuilder.UpdateData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Username",
                value: "Julie");

            migrationBuilder.UpdateData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: 4,
                column: "Username",
                value: "Xavier");
        }
    }
}
