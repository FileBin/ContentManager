using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentManager.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemovedAuthEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_content_collections_user_groups_editor_group_id",
                table: "content_collections");

            migrationBuilder.DropForeignKey(
                name: "fk_content_collections_user_groups_owner_group_id",
                table: "content_collections");

            migrationBuilder.DropForeignKey(
                name: "fk_content_collections_user_groups_reader_group_id",
                table: "content_collections");

            migrationBuilder.DropForeignKey(
                name: "fk_content_collections_users_owner_user_id",
                table: "content_collections");

            migrationBuilder.DropForeignKey(
                name: "fk_content_posts_user_groups_editor_group_id",
                table: "content_posts");

            migrationBuilder.DropForeignKey(
                name: "fk_content_posts_user_groups_owner_group_id",
                table: "content_posts");

            migrationBuilder.DropForeignKey(
                name: "fk_content_posts_user_groups_reader_group_id",
                table: "content_posts");

            migrationBuilder.DropForeignKey(
                name: "fk_content_posts_users_owner_user_id",
                table: "content_posts");

            migrationBuilder.DropForeignKey(
                name: "fk_user_groups_users_owner_user_id",
                table: "user_groups");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "user_groups");

            migrationBuilder.DropIndex(
                name: "ix_content_posts_editor_group_id",
                table: "content_posts");

            migrationBuilder.DropIndex(
                name: "ix_content_posts_owner_group_id",
                table: "content_posts");

            migrationBuilder.DropIndex(
                name: "ix_content_posts_owner_user_id",
                table: "content_posts");

            migrationBuilder.DropIndex(
                name: "ix_content_posts_reader_group_id",
                table: "content_posts");

            migrationBuilder.DropIndex(
                name: "ix_content_collections_editor_group_id",
                table: "content_collections");

            migrationBuilder.DropIndex(
                name: "ix_content_collections_owner_group_id",
                table: "content_collections");

            migrationBuilder.DropIndex(
                name: "ix_content_collections_owner_user_id",
                table: "content_collections");

            migrationBuilder.DropIndex(
                name: "ix_content_collections_reader_group_id",
                table: "content_collections");

            migrationBuilder.DropColumn(
                name: "editor_group_id",
                table: "content_posts");

            migrationBuilder.DropColumn(
                name: "owner_group_id",
                table: "content_posts");

            migrationBuilder.DropColumn(
                name: "reader_group_id",
                table: "content_posts");

            migrationBuilder.DropColumn(
                name: "editor_group_id",
                table: "content_collections");

            migrationBuilder.DropColumn(
                name: "owner_group_id",
                table: "content_collections");

            migrationBuilder.DropColumn(
                name: "reader_group_id",
                table: "content_collections");

            migrationBuilder.AddColumn<string>(
                name: "editor_group_name",
                table: "content_posts",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "owner_group_name",
                table: "content_posts",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "reader_group_name",
                table: "content_posts",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "owner_user_id",
                table: "content_collections",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(36)",
                oldMaxLength: 36);

            migrationBuilder.AddColumn<string>(
                name: "editor_group_name",
                table: "content_collections",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "owner_group_name",
                table: "content_collections",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "reader_group_name",
                table: "content_collections",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "editor_group_name",
                table: "content_posts");

            migrationBuilder.DropColumn(
                name: "owner_group_name",
                table: "content_posts");

            migrationBuilder.DropColumn(
                name: "reader_group_name",
                table: "content_posts");

            migrationBuilder.DropColumn(
                name: "editor_group_name",
                table: "content_collections");

            migrationBuilder.DropColumn(
                name: "owner_group_name",
                table: "content_collections");

            migrationBuilder.DropColumn(
                name: "reader_group_name",
                table: "content_collections");

            migrationBuilder.AddColumn<Guid>(
                name: "editor_group_id",
                table: "content_posts",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "owner_group_id",
                table: "content_posts",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "reader_group_id",
                table: "content_posts",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "owner_user_id",
                table: "content_collections",
                type: "character varying(36)",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "editor_group_id",
                table: "content_collections",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "owner_group_id",
                table: "content_collections",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "reader_group_id",
                table: "content_collections",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "user_groups",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    editor_group_id = table.Column<Guid>(type: "uuid", nullable: true),
                    owner_group_id = table.Column<Guid>(type: "uuid", nullable: true),
                    owner_user_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    reader_group_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_draft = table.Column<bool>(type: "boolean", nullable: false),
                    is_public = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_groups", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_groups_user_groups_editor_group_id",
                        column: x => x.editor_group_id,
                        principalTable: "user_groups",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_user_groups_user_groups_owner_group_id",
                        column: x => x.owner_group_id,
                        principalTable: "user_groups",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_user_groups_user_groups_reader_group_id",
                        column: x => x.reader_group_id,
                        principalTable: "user_groups",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    group_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_user_groups_group_id",
                        column: x => x.group_id,
                        principalTable: "user_groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_content_posts_editor_group_id",
                table: "content_posts",
                column: "editor_group_id");

            migrationBuilder.CreateIndex(
                name: "ix_content_posts_owner_group_id",
                table: "content_posts",
                column: "owner_group_id");

            migrationBuilder.CreateIndex(
                name: "ix_content_posts_owner_user_id",
                table: "content_posts",
                column: "owner_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_content_posts_reader_group_id",
                table: "content_posts",
                column: "reader_group_id");

            migrationBuilder.CreateIndex(
                name: "ix_content_collections_editor_group_id",
                table: "content_collections",
                column: "editor_group_id");

            migrationBuilder.CreateIndex(
                name: "ix_content_collections_owner_group_id",
                table: "content_collections",
                column: "owner_group_id");

            migrationBuilder.CreateIndex(
                name: "ix_content_collections_owner_user_id",
                table: "content_collections",
                column: "owner_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_content_collections_reader_group_id",
                table: "content_collections",
                column: "reader_group_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_groups_editor_group_id",
                table: "user_groups",
                column: "editor_group_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_groups_owner_group_id",
                table: "user_groups",
                column: "owner_group_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_groups_owner_user_id",
                table: "user_groups",
                column: "owner_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_groups_reader_group_id",
                table: "user_groups",
                column: "reader_group_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_group_id",
                table: "users",
                column: "group_id");

            migrationBuilder.AddForeignKey(
                name: "fk_content_collections_user_groups_editor_group_id",
                table: "content_collections",
                column: "editor_group_id",
                principalTable: "user_groups",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_content_collections_user_groups_owner_group_id",
                table: "content_collections",
                column: "owner_group_id",
                principalTable: "user_groups",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_content_collections_user_groups_reader_group_id",
                table: "content_collections",
                column: "reader_group_id",
                principalTable: "user_groups",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_content_collections_users_owner_user_id",
                table: "content_collections",
                column: "owner_user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_content_posts_user_groups_editor_group_id",
                table: "content_posts",
                column: "editor_group_id",
                principalTable: "user_groups",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_content_posts_user_groups_owner_group_id",
                table: "content_posts",
                column: "owner_group_id",
                principalTable: "user_groups",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_content_posts_user_groups_reader_group_id",
                table: "content_posts",
                column: "reader_group_id",
                principalTable: "user_groups",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_content_posts_users_owner_user_id",
                table: "content_posts",
                column: "owner_user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_groups_users_owner_user_id",
                table: "user_groups",
                column: "owner_user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
