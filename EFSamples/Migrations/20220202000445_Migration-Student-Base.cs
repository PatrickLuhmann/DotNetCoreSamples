using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFSamples.Migrations
{
    public partial class MigrationStudentBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Section = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentName = table.Column<string>(type: "TEXT", nullable: true),
                    GradeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HomeAddress_Address1 = table.Column<string>(type: "TEXT", nullable: true),
                    HomeAddress_Address2 = table.Column<string>(type: "TEXT", nullable: true),
                    HomeAddress_City = table.Column<string>(type: "TEXT", nullable: true),
                    HomeAddress_State = table.Column<string>(type: "TEXT", nullable: true),
                    HomeAddress_Country = table.Column<string>(type: "TEXT", nullable: true),
                    HomeAddress_ZipCode = table.Column<int>(type: "INTEGER", nullable: true),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAddresses_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentAddressFKAnnotations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    HomeAddress_Address1 = table.Column<string>(type: "TEXT", nullable: true),
                    HomeAddress_Address2 = table.Column<string>(type: "TEXT", nullable: true),
                    HomeAddress_City = table.Column<string>(type: "TEXT", nullable: true),
                    HomeAddress_State = table.Column<string>(type: "TEXT", nullable: true),
                    HomeAddress_Country = table.Column<string>(type: "TEXT", nullable: true),
                    HomeAddress_ZipCode = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAddressFKAnnotations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAddressFKAnnotations_Students_Id",
                        column: x => x.Id,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentAddressUseFluents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HomeAddress_Address1 = table.Column<string>(type: "TEXT", nullable: true),
                    HomeAddress_Address2 = table.Column<string>(type: "TEXT", nullable: true),
                    HomeAddress_City = table.Column<string>(type: "TEXT", nullable: true),
                    HomeAddress_State = table.Column<string>(type: "TEXT", nullable: true),
                    HomeAddress_Country = table.Column<string>(type: "TEXT", nullable: true),
                    HomeAddress_ZipCode = table.Column<int>(type: "INTEGER", nullable: true),
                    StudentForeignKey = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAddressUseFluents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAddressUseFluents_Students_StudentForeignKey",
                        column: x => x.StudentForeignKey,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentAddresses_StudentId",
                table: "StudentAddresses",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAddressUseFluents_StudentForeignKey",
                table: "StudentAddressUseFluents",
                column: "StudentForeignKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_GradeId",
                table: "Students",
                column: "GradeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentAddresses");

            migrationBuilder.DropTable(
                name: "StudentAddressFKAnnotations");

            migrationBuilder.DropTable(
                name: "StudentAddressUseFluents");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Grades");
        }
    }
}
