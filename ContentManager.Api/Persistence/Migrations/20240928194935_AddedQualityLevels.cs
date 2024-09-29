using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentManager.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedQualityLevels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_contents_post_id",
                table: "contents");

            migrationBuilder.AddColumn<Dictionary<string, string>>(
                name: "quality_levels",
                table: "contents",
                type: "jsonb",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_contents_post_id_post_order_post_variant",
                table: "contents",
                columns: new[] { "post_id", "post_order", "post_variant" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_contents_post_id_post_order_post_variant",
                table: "contents");

            migrationBuilder.DropColumn(
                name: "quality_levels",
                table: "contents");

            migrationBuilder.CreateIndex(
                name: "ix_contents_post_id",
                table: "contents",
                column: "post_id");
        }
    }
}
