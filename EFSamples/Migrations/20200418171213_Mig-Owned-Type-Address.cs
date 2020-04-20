using Microsoft.EntityFrameworkCore.Migrations;

namespace EFSamples.Migrations
{
    public partial class MigOwnedTypeAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "StudentAddressUseFluents",
                newName: "HomeAddress_ZipCode");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "StudentAddressUseFluents",
                newName: "HomeAddress_State");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "StudentAddressUseFluents",
                newName: "HomeAddress_Country");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "StudentAddressUseFluents",
                newName: "HomeAddress_City");

            migrationBuilder.RenameColumn(
                name: "Address2",
                table: "StudentAddressUseFluents",
                newName: "HomeAddress_Address2");

            migrationBuilder.RenameColumn(
                name: "Address1",
                table: "StudentAddressUseFluents",
                newName: "HomeAddress_Address1");

            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "StudentAddressFKAnnotations",
                newName: "HomeAddress_ZipCode");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "StudentAddressFKAnnotations",
                newName: "HomeAddress_State");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "StudentAddressFKAnnotations",
                newName: "HomeAddress_Country");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "StudentAddressFKAnnotations",
                newName: "HomeAddress_City");

            migrationBuilder.RenameColumn(
                name: "Address2",
                table: "StudentAddressFKAnnotations",
                newName: "HomeAddress_Address2");

            migrationBuilder.RenameColumn(
                name: "Address1",
                table: "StudentAddressFKAnnotations",
                newName: "HomeAddress_Address1");

            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "StudentAddresses",
                newName: "HomeAddress_ZipCode");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "StudentAddresses",
                newName: "HomeAddress_State");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "StudentAddresses",
                newName: "HomeAddress_Country");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "StudentAddresses",
                newName: "HomeAddress_City");

            migrationBuilder.RenameColumn(
                name: "Address2",
                table: "StudentAddresses",
                newName: "HomeAddress_Address2");

            migrationBuilder.RenameColumn(
                name: "Address1",
                table: "StudentAddresses",
                newName: "HomeAddress_Address1");

            // The AlterColumn operation is not supported in EF Core 3.x.
            // I can't figure out why this code is here. I didn't ask for
            // this property to be nullable. Using the Required annotation
            // did not help.
            // Therefore I am going to manually remove this code.
#if false
            migrationBuilder.AlterColumn<int>(
                name: "HomeAddress_ZipCode",
                table: "StudentAddressUseFluents",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "HomeAddress_ZipCode",
                table: "StudentAddressFKAnnotations",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "HomeAddress_ZipCode",
                table: "StudentAddresses",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");
#endif
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HomeAddress_ZipCode",
                table: "StudentAddressUseFluents",
                newName: "ZipCode");

            migrationBuilder.RenameColumn(
                name: "HomeAddress_State",
                table: "StudentAddressUseFluents",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "HomeAddress_Country",
                table: "StudentAddressUseFluents",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "HomeAddress_City",
                table: "StudentAddressUseFluents",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "HomeAddress_Address2",
                table: "StudentAddressUseFluents",
                newName: "Address2");

            migrationBuilder.RenameColumn(
                name: "HomeAddress_Address1",
                table: "StudentAddressUseFluents",
                newName: "Address1");

            migrationBuilder.RenameColumn(
                name: "HomeAddress_ZipCode",
                table: "StudentAddressFKAnnotations",
                newName: "ZipCode");

            migrationBuilder.RenameColumn(
                name: "HomeAddress_State",
                table: "StudentAddressFKAnnotations",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "HomeAddress_Country",
                table: "StudentAddressFKAnnotations",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "HomeAddress_City",
                table: "StudentAddressFKAnnotations",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "HomeAddress_Address2",
                table: "StudentAddressFKAnnotations",
                newName: "Address2");

            migrationBuilder.RenameColumn(
                name: "HomeAddress_Address1",
                table: "StudentAddressFKAnnotations",
                newName: "Address1");

            migrationBuilder.RenameColumn(
                name: "HomeAddress_ZipCode",
                table: "StudentAddresses",
                newName: "ZipCode");

            migrationBuilder.RenameColumn(
                name: "HomeAddress_State",
                table: "StudentAddresses",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "HomeAddress_Country",
                table: "StudentAddresses",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "HomeAddress_City",
                table: "StudentAddresses",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "HomeAddress_Address2",
                table: "StudentAddresses",
                newName: "Address2");

            migrationBuilder.RenameColumn(
                name: "HomeAddress_Address1",
                table: "StudentAddresses",
                newName: "Address1");

            // See explanation above.
#if false
            migrationBuilder.AlterColumn<int>(
                name: "ZipCode",
                table: "StudentAddressUseFluents",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ZipCode",
                table: "StudentAddressFKAnnotations",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ZipCode",
                table: "StudentAddresses",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
#endif
        }
    }
}
