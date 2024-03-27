using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ToaPro.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:pg_catalog.adminpack", ",,");

            migrationBuilder.CreateTable(
                name: "classes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("class_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "judges",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    f_name = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: false),
                    l_name = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: false),
                    affiliation = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("judge_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "semesters",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    term = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("semester_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    f_name = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: false),
                    l_name = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: false),
                    net_id = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("student_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "graders",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    class_id = table.Column<int>(type: "integer", nullable: false),
                    f_name = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: false),
                    l_name = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: false),
                    net_id = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    is_professor = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("grader_pk", x => x.id);
                    table.ForeignKey(
                        name: "class_fk",
                        column: x => x.class_id,
                        principalTable: "classes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "requirements",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    class_id = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    points = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("requirement_pk", x => x.id);
                    table.ForeignKey(
                        name: "class_fk",
                        column: x => x.class_id,
                        principalTable: "classes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "groups",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    semester_id = table.Column<int>(type: "integer", nullable: false),
                    section = table.Column<short>(type: "smallint", nullable: false),
                    number = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("group_pk", x => x.id);
                    table.ForeignKey(
                        name: "semester_fk",
                        column: x => x.semester_id,
                        principalTable: "semesters",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "semester_classes",
                columns: table => new
                {
                    class_id = table.Column<int>(type: "integer", nullable: false),
                    semester_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("semester_classes_pk", x => new { x.class_id, x.semester_id });
                    table.ForeignKey(
                        name: "class_fk",
                        column: x => x.class_id,
                        principalTable: "classes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "semester_fk",
                        column: x => x.semester_id,
                        principalTable: "semesters",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "semester_graders",
                columns: table => new
                {
                    grader_id = table.Column<int>(type: "integer", nullable: false),
                    semester_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("semester_graders_pk", x => new { x.grader_id, x.semester_id });
                    table.ForeignKey(
                        name: "grader_fk",
                        column: x => x.grader_id,
                        principalTable: "graders",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "semester_fk",
                        column: x => x.semester_id,
                        principalTable: "semesters",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "presentations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    group_id = table.Column<int>(type: "integer", nullable: false),
                    location = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("presentation_pk", x => x.id);
                    table.ForeignKey(
                        name: "group_fk",
                        column: x => x.group_id,
                        principalTable: "groups",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "rankings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    group_id = table.Column<int>(type: "integer", nullable: false),
                    judge_id = table.Column<int>(type: "integer", nullable: false),
                    points = table.Column<float>(type: "real", nullable: true),
                    ranking = table.Column<int>(type: "integer", nullable: true),
                    comments = table.Column<string>(type: "text", nullable: true),
                    nomination = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ranking_pk", x => x.id);
                    table.ForeignKey(
                        name: "group_fk",
                        column: x => x.group_id,
                        principalTable: "groups",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "judge_fk",
                        column: x => x.judge_id,
                        principalTable: "judges",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "student_groups",
                columns: table => new
                {
                    group_id = table.Column<int>(type: "integer", nullable: false),
                    student_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("student_groups_pk", x => new { x.group_id, x.student_id });
                    table.ForeignKey(
                        name: "group_fk",
                        column: x => x.group_id,
                        principalTable: "groups",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "student_fk",
                        column: x => x.student_id,
                        principalTable: "students",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "submissions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    group_id = table.Column<int>(type: "integer", nullable: false),
                    student_id = table.Column<int>(type: "integer", nullable: false),
                    GithubLink = table.Column<string>(type: "text", nullable: false),
                    YoutubeLink = table.Column<string>(type: "text", nullable: false),
                    UploadFile = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("submission_pk", x => x.id);
                    table.ForeignKey(
                        name: "group_fk",
                        column: x => x.group_id,
                        principalTable: "groups",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "student_fk",
                        column: x => x.student_id,
                        principalTable: "students",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "judge_presentations",
                columns: table => new
                {
                    judge_id = table.Column<int>(type: "integer", nullable: false),
                    presentation_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("judge_presentations_pk", x => new { x.judge_id, x.presentation_id });
                    table.ForeignKey(
                        name: "judge_fk",
                        column: x => x.judge_id,
                        principalTable: "judges",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "presentation_fk",
                        column: x => x.presentation_id,
                        principalTable: "presentations",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "grades",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    requirement_id = table.Column<int>(type: "integer", nullable: false),
                    grader_id = table.Column<int>(type: "integer", nullable: false),
                    group_id = table.Column<int>(type: "integer", nullable: false),
                    submission_id = table.Column<int>(type: "integer", nullable: false),
                    points = table.Column<float>(type: "real", nullable: true),
                    comments = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("grade_pk", x => x.id);
                    table.ForeignKey(
                        name: "grader_fk",
                        column: x => x.grader_id,
                        principalTable: "graders",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "group_fk",
                        column: x => x.group_id,
                        principalTable: "groups",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "requirement_fk",
                        column: x => x.requirement_id,
                        principalTable: "requirements",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "submission_fk",
                        column: x => x.submission_id,
                        principalTable: "submissions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "uniq_code",
                table: "classes",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_graders_class_id",
                table: "graders",
                column: "class_id");

            migrationBuilder.CreateIndex(
                name: "uniq_grader",
                table: "graders",
                columns: new[] { "f_name", "l_name", "net_id", "class_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_grades_grader_id",
                table: "grades",
                column: "grader_id");

            migrationBuilder.CreateIndex(
                name: "IX_grades_group_id",
                table: "grades",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_grades_submission_id",
                table: "grades",
                column: "submission_id");

            migrationBuilder.CreateIndex(
                name: "uniq_grade",
                table: "grades",
                columns: new[] { "requirement_id", "grader_id", "group_id", "submission_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uniq_group",
                table: "groups",
                columns: new[] { "semester_id", "section", "number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_judge_presentations_presentation_id",
                table: "judge_presentations",
                column: "presentation_id");

            migrationBuilder.CreateIndex(
                name: "uniq_judge",
                table: "judges",
                columns: new[] { "f_name", "l_name", "affiliation" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uniq_presentation",
                table: "presentations",
                columns: new[] { "group_id", "location", "start_date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_rankings_judge_id",
                table: "rankings",
                column: "judge_id");

            migrationBuilder.CreateIndex(
                name: "uniq_ranking",
                table: "rankings",
                columns: new[] { "group_id", "judge_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uniq_requirement",
                table: "requirements",
                columns: new[] { "class_id", "description" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_semester_classes_semester_id",
                table: "semester_classes",
                column: "semester_id");

            migrationBuilder.CreateIndex(
                name: "IX_semester_graders_semester_id",
                table: "semester_graders",
                column: "semester_id");

            migrationBuilder.CreateIndex(
                name: "uniq_term",
                table: "semesters",
                column: "term",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_student_groups_student_id",
                table: "student_groups",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "uniq_student",
                table: "students",
                columns: new[] { "f_name", "l_name", "net_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_submissions_student_id",
                table: "submissions",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "uniq_submission",
                table: "submissions",
                columns: new[] { "group_id", "student_id", "created_date" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "grades");

            migrationBuilder.DropTable(
                name: "judge_presentations");

            migrationBuilder.DropTable(
                name: "rankings");

            migrationBuilder.DropTable(
                name: "semester_classes");

            migrationBuilder.DropTable(
                name: "semester_graders");

            migrationBuilder.DropTable(
                name: "student_groups");

            migrationBuilder.DropTable(
                name: "requirements");

            migrationBuilder.DropTable(
                name: "submissions");

            migrationBuilder.DropTable(
                name: "presentations");

            migrationBuilder.DropTable(
                name: "judges");

            migrationBuilder.DropTable(
                name: "graders");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "groups");

            migrationBuilder.DropTable(
                name: "classes");

            migrationBuilder.DropTable(
                name: "semesters");
        }
    }
}
