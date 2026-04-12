using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurvStation.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "forms",
                columns: table => new
                {
                    key = table.Column<Guid>(type: "uuid", nullable: false),
                    slug = table.Column<string>(type: "text", nullable: false),
                    logo = table.Column<string>(type: "text", nullable: true),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_forms", x => x.key);
                    table.UniqueConstraint("ak_forms_slug", x => x.slug);
                });

            migrationBuilder.CreateTable(
                name: "form_item_guid",
                columns: table => new
                {
                    key = table.Column<Guid>(type: "uuid", nullable: false),
                    form_id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    tags = table.Column<string[]>(type: "text[]", nullable: false),
                    config = table.Column<string>(type: "JSONB", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_form_item_guid", x => x.key);
                    table.ForeignKey(
                        name: "fk_form_item_guid_forms_form_id",
                        column: x => x.form_id,
                        principalTable: "forms",
                        principalColumn: "key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "form_versions",
                columns: table => new
                {
                    key = table.Column<Guid>(type: "uuid", nullable: false),
                    form_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_current = table.Column<bool>(type: "boolean", nullable: false),
                    archived_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    form_definition_guid_key = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_form_versions", x => x.key);
                    table.ForeignKey(
                        name: "fk_form_versions_forms_form_definition_guid_key",
                        column: x => x.form_definition_guid_key,
                        principalTable: "forms",
                        principalColumn: "key");
                    table.ForeignKey(
                        name: "fk_form_versions_forms_form_id",
                        column: x => x.form_id,
                        principalTable: "forms",
                        principalColumn: "key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "form_version_item_guid",
                columns: table => new
                {
                    key = table.Column<Guid>(type: "uuid", nullable: false),
                    item_id = table.Column<Guid>(type: "uuid", nullable: true),
                    form_id = table.Column<Guid>(type: "uuid", nullable: false),
                    form_version_id = table.Column<Guid>(type: "uuid", nullable: false),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    path = table.Column<string>(type: "text", nullable: false),
                    meta_tag = table.Column<string>(type: "text", nullable: true),
                    config = table.Column<string>(type: "JSONB", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    relevance = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_form_version_item_guid", x => x.key);
                    table.UniqueConstraint("ak_form_version_item_guid_form_id_form_version_id", x => new { x.form_id, x.form_version_id });
                    table.ForeignKey(
                        name: "fk_form_version_item_guid_form_item_guid_item_id",
                        column: x => x.item_id,
                        principalTable: "form_item_guid",
                        principalColumn: "key",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_form_version_item_guid_form_version_item_guid_parent_id",
                        column: x => x.parent_id,
                        principalTable: "form_version_item_guid",
                        principalColumn: "key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_form_version_item_guid_form_versions_form_version_id",
                        column: x => x.form_version_id,
                        principalTable: "form_versions",
                        principalColumn: "key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_form_version_item_guid_forms_form_id",
                        column: x => x.form_id,
                        principalTable: "forms",
                        principalColumn: "key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_form_item_guid_form_id",
                table: "form_item_guid",
                column: "form_id");

            migrationBuilder.CreateIndex(
                name: "ix_form_version_item_guid_form_version_id",
                table: "form_version_item_guid",
                column: "form_version_id");

            migrationBuilder.CreateIndex(
                name: "ix_form_version_item_guid_item_id",
                table: "form_version_item_guid",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "ix_form_version_item_guid_parent_id",
                table: "form_version_item_guid",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "ix_form_versions_form_definition_guid_key",
                table: "form_versions",
                column: "form_definition_guid_key");

            migrationBuilder.CreateIndex(
                name: "ix_form_versions_form_id",
                table: "form_versions",
                column: "form_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "form_version_item_guid");

            migrationBuilder.DropTable(
                name: "form_item_guid");

            migrationBuilder.DropTable(
                name: "form_versions");

            migrationBuilder.DropTable(
                name: "forms");
        }
    }
}
