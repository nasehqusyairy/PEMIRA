using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PEMIRA.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "menusegments",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint(20)", nullable: false, comment: "Primary Key")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, defaultValueSql: "'New Segment'", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, defaultValueSql: "'New Permission'", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, defaultValueSql: "'New Role'", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, defaultValueSql: "'New User'", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    code = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    more = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, defaultValueSql: "'-'", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gender = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "'1'"),
                    password = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "current_timestamp()"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "current_timestamp()")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    created_by = table.Column<long>(type: "bigint(20)", nullable: true),
                    updated_by = table.Column<long>(type: "bigint(20)", nullable: true),
                    deleted_by = table.Column<long>(type: "bigint(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_users_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_users_updated_by",
                        column: x => x.updated_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "menus",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint(20)", nullable: false, comment: "Primary Key")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    menusegment_id = table.Column<long>(type: "bigint(20)", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, defaultValueSql: "'New Menu'", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    url = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, defaultValueSql: "'/'", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    icon = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, defaultValueSql: "'file-earmark'", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_menusegment_id",
                        column: x => x.menusegment_id,
                        principalTable: "menusegments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "elections",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, defaultValueSql: "'New Candidate'", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "current_timestamp()"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "current_timestamp()")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    created_by = table.Column<long>(type: "bigint(20)", nullable: true),
                    updated_by = table.Column<long>(type: "bigint(20)", nullable: true),
                    deleted_by = table.Column<long>(type: "bigint(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_elections_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_elections_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_elections_updated_by",
                        column: x => x.updated_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "permission_user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<long>(type: "bigint(20)", nullable: false),
                    permission_id = table.Column<long>(type: "bigint(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_permission_id",
                        column: x => x.permission_id,
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, defaultValueSql: "'New Tag'", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "current_timestamp()"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "current_timestamp()")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    created_by = table.Column<long>(type: "bigint(20)", nullable: true),
                    updated_by = table.Column<long>(type: "bigint(20)", nullable: true),
                    deleted_by = table.Column<long>(type: "bigint(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_tags_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_tags_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_tags_updated_by",
                        column: x => x.updated_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "menu_role",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    menu_id = table.Column<long>(type: "bigint(20)", nullable: false),
                    role_id = table.Column<long>(type: "bigint(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "menu_role_ibfk_1",
                        column: x => x.menu_id,
                        principalTable: "menus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "menu_role_ibfk_2",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "candidates",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<long>(type: "bigint(20)", nullable: false),
                    election_id = table.Column<long>(type: "bigint(20)", nullable: false),
                    img = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, defaultValueSql: "'nophoto.jpg'", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    color = table.Column<string>(type: "char(6)", fixedLength: true, maxLength: 6, nullable: false, defaultValueSql: "'00bcd4'", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "current_timestamp()"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "current_timestamp()")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    deleted_at = table.Column<DateTime>(type: "timestamp", nullable: true),
                    created_by = table.Column<long>(type: "bigint(20)", nullable: true),
                    updated_by = table.Column<long>(type: "bigint(20)", nullable: true),
                    deleted_by = table.Column<long>(type: "bigint(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "candidates_elections_FK",
                        column: x => x.election_id,
                        principalTable: "elections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "candidates_users_FK",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_candidates_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_candidates_deleted_by",
                        column: x => x.deleted_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_candidates_updated_by",
                        column: x => x.updated_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "election_user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<long>(type: "bigint(20)", nullable: false),
                    election_id = table.Column<long>(type: "bigint(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "election_user_elections_FK",
                        column: x => x.election_id,
                        principalTable: "elections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "election_user_users_FK",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "role_user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<long>(type: "bigint(20)", nullable: false),
                    role_id = table.Column<long>(type: "bigint(20)", nullable: false),
                    election_id = table.Column<long>(type: "bigint(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "role_user_elections_FK",
                        column: x => x.election_id,
                        principalTable: "elections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "role_user_roles_FK",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "role_user_users_FK",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "tag_user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<long>(type: "bigint(20)", nullable: false),
                    tag_id = table.Column<long>(type: "bigint(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "tag_user_tags_FK",
                        column: x => x.tag_id,
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "tag_user_users_FK",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "candidate_user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<long>(type: "bigint(20)", nullable: false),
                    candidate_id = table.Column<long>(type: "bigint(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "user_candidate_candidates_FK",
                        column: x => x.candidate_id,
                        principalTable: "candidates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "user_candidate_users_FK",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "user_candidate_candidates_FK",
                table: "candidate_user",
                column: "candidate_id");

            migrationBuilder.CreateIndex(
                name: "user_candidate_users_FK",
                table: "candidate_user",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "candidates_elections_FK",
                table: "candidates",
                column: "election_id");

            migrationBuilder.CreateIndex(
                name: "candidates_users_FK",
                table: "candidates",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "fk_candidates_created_by",
                table: "candidates",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "fk_candidates_deleted_by",
                table: "candidates",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "fk_candidates_updated_by",
                table: "candidates",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "election_user_elections_FK",
                table: "election_user",
                column: "election_id");

            migrationBuilder.CreateIndex(
                name: "election_user_users_FK",
                table: "election_user",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "fk_elections_created_by",
                table: "elections",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "fk_elections_deleted_by",
                table: "elections",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "fk_elections_updated_by",
                table: "elections",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "menu_role_ibfk_1",
                table: "menu_role",
                column: "menu_id");

            migrationBuilder.CreateIndex(
                name: "menu_role_ibfk_2",
                table: "menu_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "fk_menusegment_id",
                table: "menus",
                column: "menusegment_id");

            migrationBuilder.CreateIndex(
                name: "fk_permission_id",
                table: "permission_user",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "fk_user_id",
                table: "permission_user",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "role_user_elections_FK",
                table: "role_user",
                column: "election_id");

            migrationBuilder.CreateIndex(
                name: "role_user_roles_FK",
                table: "role_user",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "role_user_users_FK",
                table: "role_user",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "tag_user_tags_FK",
                table: "tag_user",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "tag_user_users_FK",
                table: "tag_user",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "fk_tags_created_by",
                table: "tags",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "fk_tags_deleted_by",
                table: "tags",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "fk_tags_updated_by",
                table: "tags",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "code",
                table: "users",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_users_created_by",
                table: "users",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "fk_users_deleted_by",
                table: "users",
                column: "deleted_by");

            migrationBuilder.CreateIndex(
                name: "fk_users_updated_by",
                table: "users",
                column: "updated_by");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "candidate_user");

            migrationBuilder.DropTable(
                name: "election_user");

            migrationBuilder.DropTable(
                name: "menu_role");

            migrationBuilder.DropTable(
                name: "permission_user");

            migrationBuilder.DropTable(
                name: "role_user");

            migrationBuilder.DropTable(
                name: "tag_user");

            migrationBuilder.DropTable(
                name: "candidates");

            migrationBuilder.DropTable(
                name: "menus");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "elections");

            migrationBuilder.DropTable(
                name: "menusegments");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
