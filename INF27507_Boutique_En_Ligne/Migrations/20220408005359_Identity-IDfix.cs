using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INF27507_Boutique_En_Ligne.Migrations
{
    public partial class IdentityIDfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Clients");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Sellers",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Clients",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PhoneNumber" },
                values: new object[] { "c94b4c97-4384-432e-a273-5a97f75b7d5c", "418-888-9999" });

            migrationBuilder.UpdateData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PhoneNumber" },
                values: new object[] { "fe7ab66c-0aac-4bee-90d6-e046e0d17a48", "581-456-3263" });

            migrationBuilder.UpdateData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PhoneNumber" },
                values: new object[] { "2254d7f4-ea2c-474e-aa24-42c0490693f2", "418-753-1596" });

            migrationBuilder.UpdateData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PhoneNumber" },
                values: new object[] { "59e914e3-a4da-4bc8-ac6d-91b9926200ca", "918-852-1122" });

            migrationBuilder.UpdateData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PhoneNumber" },
                values: new object[] { "523fab5a-8efa-4abc-8cb1-767351b18a24", "418-874-5632" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Sellers",
                keyColumn: "Email",
                keyValue: null,
                column: "Email",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Sellers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Sellers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Email",
                keyValue: null,
                column: "Email",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Clients",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Clients",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Phone", "PhoneNumber" },
                values: new object[] { "4bc39ba9-01a3-4c4b-85fd-a1c3ecf87b47", "418-888-9999", null });

            migrationBuilder.UpdateData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Phone", "PhoneNumber" },
                values: new object[] { "abafb10d-e654-4660-9858-a5d7e209ff60", "581-456-3263", null });

            migrationBuilder.UpdateData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "Phone", "PhoneNumber" },
                values: new object[] { "374763c6-d51d-49bb-a457-e3a59308b173", "418-753-1596", null });

            migrationBuilder.UpdateData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "Phone", "PhoneNumber" },
                values: new object[] { "db62d5a4-45be-48d9-8e0d-02cbac91ad29", "918-852-1122", null });

            migrationBuilder.UpdateData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "Phone", "PhoneNumber" },
                values: new object[] { "cc7a01bf-6f44-4559-b6b0-db93baa24d03", "418-874-5632", null });
        }
    }
}
