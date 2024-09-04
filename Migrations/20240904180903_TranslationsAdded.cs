using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_ods_mace_erasmus.Migrations
{
    /// <inheritdoc />
    public partial class TranslationsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    translation_state = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    language_code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    translation_json = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.id);
                    table.ForeignKey(
                        name: "FK_Translations_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_user_id",
                table: "Translations",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Translations");
        }
    }
}
