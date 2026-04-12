using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SurvStation.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddSubmissionModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "form_submissions",
                columns: table => new
                {
                    key = table.Column<Guid>(type: "uuid", nullable: false),
                    index = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    form_version_id = table.Column<Guid>(type: "uuid", nullable: false),
                    form_id = table.Column<Guid>(type: "uuid", nullable: false),
                    archived_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_form_submissions", x => x.key);
                    table.UniqueConstraint("ak_form_submissions_index", x => x.index);
                    table.ForeignKey(
                        name: "fk_form_submissions_form_versions_form_version_id",
                        column: x => x.form_version_id,
                        principalTable: "form_versions",
                        principalColumn: "key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_form_submissions_forms_form_id",
                        column: x => x.form_id,
                        principalTable: "forms",
                        principalColumn: "key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "form_submission_responses",
                columns: table => new
                {
                    key = table.Column<Guid>(type: "uuid", nullable: false),
                    submission_id = table.Column<Guid>(type: "uuid", nullable: false),
                    form_id = table.Column<Guid>(type: "uuid", nullable: false),
                    form_version_id = table.Column<Guid>(type: "uuid", nullable: false),
                    form_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<byte[]>(type: "bytea", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_form_submission_responses", x => x.key);
                    table.UniqueConstraint("ak_form_submission_responses_submission_id_form_id_form_versio", x => new { x.submission_id, x.form_id, x.form_version_id, x.form_item_id });
                    table.ForeignKey(
                        name: "fk_form_submission_responses_form_submissions_submission_id",
                        column: x => x.submission_id,
                        principalTable: "form_submissions",
                        principalColumn: "key");
                    table.ForeignKey(
                        name: "fk_form_submission_responses_form_version_item_guid_form_versi",
                        column: x => x.form_version_id,
                        principalTable: "form_version_item_guid",
                        principalColumn: "key",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_form_submission_responses_form_versions_form_version_id",
                        column: x => x.form_version_id,
                        principalTable: "form_versions",
                        principalColumn: "key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_form_submission_responses_forms_form_id",
                        column: x => x.form_id,
                        principalTable: "forms",
                        principalColumn: "key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_form_submission_responses_form_id",
                table: "form_submission_responses",
                column: "form_id");

            migrationBuilder.CreateIndex(
                name: "ix_form_submission_responses_form_version_id",
                table: "form_submission_responses",
                column: "form_version_id");

            migrationBuilder.CreateIndex(
                name: "ix_form_submissions_form_id",
                table: "form_submissions",
                column: "form_id");

            migrationBuilder.CreateIndex(
                name: "ix_form_submissions_form_version_id",
                table: "form_submissions",
                column: "form_version_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "form_submission_responses");

            migrationBuilder.DropTable(
                name: "form_submissions");
        }
    }
}
