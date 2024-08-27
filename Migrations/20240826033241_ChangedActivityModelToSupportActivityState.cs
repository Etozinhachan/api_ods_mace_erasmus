using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_ods_mace_erasmus.Migrations
{
    /// <inheritdoc />
    public partial class ChangedActivityModelToSupportActivityState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "activity_state",
                table: "Activities",
                type: "tinyint unsigned",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "activity_state",
                table: "Activities");
        }
    }
}
