using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFAR.Classwork.Data.Migrations
{
    /// <inheritdoc />
    public partial class patientIniiiiit123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PatientAccounts",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "PatientAccounts");
        }
    }
}
