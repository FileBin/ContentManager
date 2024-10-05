using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentManager.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedPreviewAndTimestamps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "contents",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "contents",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "content_posts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "preview_id",
                table: "content_posts",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "content_posts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "content_collections",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "content_collections",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_content_posts_preview_id",
                table: "content_posts",
                column: "preview_id");

            migrationBuilder.AddForeignKey(
                name: "fk_content_posts_contents_preview_id",
                table: "content_posts",
                column: "preview_id",
                principalTable: "contents",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_content_posts_contents_preview_id",
                table: "content_posts");

            migrationBuilder.DropIndex(
                name: "ix_content_posts_preview_id",
                table: "content_posts");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "contents");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "contents");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "content_posts");

            migrationBuilder.DropColumn(
                name: "preview_id",
                table: "content_posts");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "content_posts");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "content_collections");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "content_collections");
        }
    }
}
