using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INF27507_Boutique_En_Ligne.Migrations
{
    public partial class Identity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Sellers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Sellers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Sellers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Sellers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Sellers",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Sellers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Sellers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Sellers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Sellers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Sellers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Sellers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Sellers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Sellers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Clients",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Clients",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Clients",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Clients",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Clients",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Clients",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Clients",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Clients",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Clients",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Clients",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Clients",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Clients",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "4bc39ba9-01a3-4c4b-85fd-a1c3ecf87b47");

            migrationBuilder.UpdateData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "abafb10d-e654-4660-9858-a5d7e209ff60");

            migrationBuilder.UpdateData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "374763c6-d51d-49bb-a457-e3a59308b173");

            migrationBuilder.UpdateData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "db62d5a4-45be-48d9-8e0d-02cbac91ad29");

            migrationBuilder.UpdateData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "cc7a01bf-6f44-4559-b6b0-db93baa24d03");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Clients");
        }
    }
}
