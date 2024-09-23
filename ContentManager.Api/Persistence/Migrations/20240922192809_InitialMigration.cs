using System;
using ContentManager.Api.Contracts.Domain.Enum;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentManager.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:content_type", "picture,gif,video,music");

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    parent_name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tags", x => x.name);
                    table.ForeignKey(
                        name: "fk_tags_tags_parent_name",
                        column: x => x.parent_name,
                        principalTable: "tags",
                        principalColumn: "name");
                });

            migrationBuilder.CreateTable(
                name: "content_collections",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    is_public = table.Column<bool>(type: "boolean", nullable: false),
                    is_draft = table.Column<bool>(type: "boolean", nullable: false),
                    reader_group_id = table.Column<Guid>(type: "uuid", nullable: true),
                    editor_group_id = table.Column<Guid>(type: "uuid", nullable: true),
                    owner_group_id = table.Column<Guid>(type: "uuid", nullable: true),
                    owner_user_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_content_collections", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "content_post_tag",
                columns: table => new
                {
                    content_posts_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tags_name = table.Column<string>(type: "character varying(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_content_post_tag", x => new { x.content_posts_id, x.tags_name });
                    table.ForeignKey(
                        name: "fk_content_post_tag_tags_tags_name",
                        column: x => x.tags_name,
                        principalTable: "tags",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "content_posts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    is_public = table.Column<bool>(type: "boolean", nullable: false),
                    is_draft = table.Column<bool>(type: "boolean", nullable: false),
                    can_users_edit_tags = table.Column<bool>(type: "boolean", nullable: false),
                    reader_group_id = table.Column<Guid>(type: "uuid", nullable: true),
                    editor_group_id = table.Column<Guid>(type: "uuid", nullable: true),
                    owner_group_id = table.Column<Guid>(type: "uuid", nullable: true),
                    owner_user_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_content_posts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "content_posts_collections",
                columns: table => new
                {
                    post_id = table.Column<Guid>(type: "uuid", nullable: false),
                    collection_id = table.Column<Guid>(type: "uuid", nullable: false),
                    collection_order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_content_posts_collections", x => new { x.post_id, x.collection_id });
                    table.ForeignKey(
                        name: "fk_content_posts_collections_content_collections_collection_id",
                        column: x => x.collection_id,
                        principalTable: "content_collections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_content_posts_collections_content_posts_post_id",
                        column: x => x.post_id,
                        principalTable: "content_posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contents",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    content_type = table.Column<ContentType>(type: "content_type", nullable: false),
                    local_file_path = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    post_order = table.Column<int>(type: "integer", nullable: false),
                    post_variant = table.Column<int>(type: "integer", nullable: false),
                    post_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contents", x => x.id);
                    table.ForeignKey(
                        name: "fk_contents_content_posts_post_id",
                        column: x => x.post_id,
                        principalTable: "content_posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_groups",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    reader_group_id = table.Column<Guid>(type: "uuid", nullable: true),
                    editor_group_id = table.Column<Guid>(type: "uuid", nullable: true),
                    owner_group_id = table.Column<Guid>(type: "uuid", nullable: true),
                    owner_user_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    is_public = table.Column<bool>(type: "boolean", nullable: false),
                    is_draft = table.Column<bool>(type: "boolean", nullable: false)
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
                name: "ix_content_post_tag_tags_name",
                table: "content_post_tag",
                column: "tags_name");

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
                name: "ix_content_posts_collections_collection_id",
                table: "content_posts_collections",
                column: "collection_id");

            migrationBuilder.CreateIndex(
                name: "ix_contents_post_id",
                table: "contents",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "ix_tags_parent_name",
                table: "tags",
                column: "parent_name");

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
                name: "fk_content_post_tag_content_posts_content_posts_id",
                table: "content_post_tag",
                column: "content_posts_id",
                principalTable: "content_posts",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_user_groups_group_id",
                table: "users");

            migrationBuilder.DropTable(
                name: "content_post_tag");

            migrationBuilder.DropTable(
                name: "content_posts_collections");

            migrationBuilder.DropTable(
                name: "contents");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "content_collections");

            migrationBuilder.DropTable(
                name: "content_posts");

            migrationBuilder.DropTable(
                name: "user_groups");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
