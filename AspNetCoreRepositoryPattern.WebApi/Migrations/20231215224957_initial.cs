using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AspNetCoreRepositoryPattern.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    Done = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(54)", maxLength: 54, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CreatedAt", "Description", "Title" },
                values: new object[,]
                {
                    { new Guid("3c1fd1eb-3ad3-4108-b236-b5275d140baa"), "Gabriel Garcia Marquez", new DateTime(2023, 12, 15, 23, 49, 56, 866, DateTimeKind.Local).AddTicks(8920), "One of the 20th century's enduring works, One Hundred Years of Solitude is a widely beloved and acclaimed novel known throughout the world...", "One Hundred Years of Solitude" },
                    { new Guid("457d57c6-8514-4d5c-b0b7-d72246df5dea"), "Miguel de Cervantes", new DateTime(2023, 12, 15, 23, 49, 56, 866, DateTimeKind.Local).AddTicks(8920), "Alonso Quixano, a retired country gentleman in his fifties, lives in an unnamed section of La Mancha with his niece and a housekeeper.", "Don Quixote" },
                    { new Guid("9175edd8-6ee3-426f-83b6-9d3d90fd8116"), "James Joyce", new DateTime(2023, 12, 15, 23, 49, 56, 866, DateTimeKind.Local).AddTicks(8920), "Ulysses chronicles the passage of Leopold Bloom through Dublin during an ordinary day, June 16, 1904.", "Ulysses" },
                    { new Guid("a560d8e6-ed98-42cf-8556-c5c12a553c8d"), "Marcel Proust", new DateTime(2023, 12, 15, 23, 49, 56, 866, DateTimeKind.Local).AddTicks(8910), "Swanns Way, the first part of A la recherche de temps perdu, Marcel Prousts seven-part cycle, was published in 1913.", "In Search of Lost Time" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "Email", "FirstName", "LastName", "Mobile" },
                values: new object[,]
                {
                    { new Guid("0826373d-c58d-4db3-957e-d83c7705a4b8"), new DateTime(2023, 12, 15, 23, 49, 56, 866, DateTimeKind.Local).AddTicks(8950), new DateTime(2023, 12, 15, 0, 0, 0, 0, DateTimeKind.Local), "noraolsen@gmail.com", "Nora", "Olsen", "90896745" },
                    { new Guid("0a3ecdf5-4685-4eab-811f-a2545777030a"), new DateTime(2023, 12, 15, 23, 49, 56, 866, DateTimeKind.Local).AddTicks(8960), new DateTime(2023, 12, 15, 23, 49, 56, 866, DateTimeKind.Local).AddTicks(8960), "emiljohansen@gmail.com", "Emil", "Johansen", "90452378" },
                    { new Guid("1758c6df-5d92-4eff-a5c0-f65c40c01621"), new DateTime(2023, 12, 15, 23, 49, 56, 866, DateTimeKind.Local).AddTicks(8960), new DateTime(2023, 12, 15, 0, 0, 0, 0, DateTimeKind.Local), "emmahansen@gmail.com", "Emma", "Hansen", "90129034" },
                    { new Guid("b8c8b904-d7ed-48bf-90d7-297e412e2320"), new DateTime(2023, 12, 15, 23, 49, 56, 866, DateTimeKind.Local).AddTicks(8950), new DateTime(2023, 12, 15, 0, 0, 0, 0, DateTimeKind.Local), "jakoblarsen@gmail.com", "Jakob", "Larsen", "90901234" }
                });

            migrationBuilder.InsertData(
                table: "Todos",
                columns: new[] { "Id", "CreatedAt", "Done", "Name" },
                values: new object[,]
                {
                    { new Guid("892ed1f0-52bc-455f-999a-c3be73790dfa"), new DateTime(2023, 12, 15, 23, 49, 56, 866, DateTimeKind.Local).AddTicks(8780), true, "Coding" },
                    { new Guid("90d4e85a-54d8-48a9-b3a8-63d5560ea4bc"), new DateTime(2023, 12, 15, 23, 49, 56, 866, DateTimeKind.Local).AddTicks(8770), true, "Cleaning" },
                    { new Guid("a7c222b7-7e82-4a01-8085-b5abc191f777"), new DateTime(2023, 12, 15, 23, 49, 56, 866, DateTimeKind.Local).AddTicks(8770), true, "Shopping" },
                    { new Guid("cfa7b2b2-08a0-4361-9dce-fe2dbe789190"), new DateTime(2023, 12, 15, 23, 49, 56, 866, DateTimeKind.Local).AddTicks(8780), true, "Travelling" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "LastName", "Password" },
                values: new object[,]
                {
                    { new Guid("8e20e519-85d1-4e6e-b44b-024a28a2f870"), new DateTime(2023, 12, 15, 23, 49, 56, 866, DateTimeKind.Local).AddTicks(8980), "rubyjane88@gmail.com", "Ruby Jane", "Cabagnot", "Pass123!" },
                    { new Guid("b01e8a73-3b16-4a3b-bb3a-cc6974281447"), new DateTime(2023, 12, 15, 23, 49, 56, 866, DateTimeKind.Local).AddTicks(8980), "webmasterdevlin@gmail.com", "Devlin", "Duldulao", "Pass123!" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Todos");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
