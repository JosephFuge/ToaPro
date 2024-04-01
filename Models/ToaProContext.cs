using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ToaPro.Models;

public partial class ToaProContext : IdentityDbContext<ToaProUser, IdentityRole, string>
{
    public ToaProContext()
    {
    }

    public ToaProContext(DbContextOptions<ToaProContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Grader> Graders { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Judge> Judges { get; set; }

    public virtual DbSet<Presentation> Presentations { get; set; }

    public virtual DbSet<Ranking> Rankings { get; set; }

    public virtual DbSet<Requirement> Requirements { get; set; }

    public virtual DbSet<Semester> Semesters { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Submission> Submissions { get; set; }
    public virtual DbSet<JudgeAvailability> JudgeAvailabilities { get; set; }
    public virtual DbSet<Student> StudentAvailabilities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=ToaPro;Username=postgres;Password=postgres;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("class_pk");

            entity.ToTable("classes");

            entity.HasIndex(e => e.Code, "uniq_code").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(5)
                .HasColumnName("code");
            entity.Property(e => e.Description).HasColumnName("description");

            entity.HasMany(d => d.Semesters).WithMany(p => p.Classes)
                .UsingEntity<Dictionary<string, object>>(
                    "SemesterClass",
                    r => r.HasOne<Semester>().WithMany()
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("semester_fk"),
                    l => l.HasOne<Class>().WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("class_fk"),
                    j =>
                    {
                        j.HasKey("ClassId", "SemesterId").HasName("semester_classes_pk");
                        j.ToTable("semester_classes");
                        j.HasIndex(new[] { "SemesterId" }, "IX_semester_classes_semester_id");
                        j.IndexerProperty<int>("ClassId").HasColumnName("class_id");
                        j.IndexerProperty<int>("SemesterId").HasColumnName("semester_id");
                    });
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("grade_pk");

            entity.ToTable("grades");

            entity.HasIndex(e => e.GraderId, "IX_grades_grader_id");

            entity.HasIndex(e => e.GroupId, "IX_grades_group_id");

            entity.HasIndex(e => e.SubmissionId, "IX_grades_submission_id");

            entity.HasIndex(e => new { e.RequirementId, e.GraderId, e.GroupId, e.SubmissionId }, "uniq_grade").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Comments).HasColumnName("comments");
            entity.Property(e => e.GraderId).HasColumnName("grader_id");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.Points).HasColumnName("points");
            entity.Property(e => e.RequirementId).HasColumnName("requirement_id");
            entity.Property(e => e.SubmissionId).HasColumnName("submission_id");

            entity.HasOne(d => d.Grader).WithMany(p => p.Grades)
                .HasForeignKey(d => d.GraderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("grader_fk");

            entity.HasOne(d => d.Group).WithMany(p => p.Grades)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("group_fk");

            entity.HasOne(d => d.Requirement).WithMany(p => p.Grades)
                .HasForeignKey(d => d.RequirementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("requirement_fk");

            entity.HasOne(d => d.Submission).WithMany(p => p.Grades)
                .HasForeignKey(d => d.SubmissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("submission_fk");
        });

        modelBuilder.Entity<Grader>(entity =>
        {
           /* entity.HasKey(e => e.Id).HasName("grader_pk");

            entity.ToTable("graders");

            entity.HasIndex(e => e.ClassId, "IX_graders_class_id");

            entity.HasIndex(e => new { e.ClassId }, "uniq_grader").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.IsProfessor)
                .HasDefaultValue(false)Learn about building Web apps with ASP.NET Core.


                .HasColumnName("is_professor");

            entity.HasOne(d => d.Class).WithMany(p => p.Graders)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("class_fk");

            entity.HasMany(d => d.Semesters).WithMany(p => p.Graders)
                .UsingEntity<Dictionary<string, object>>(
                    "SemesterGrader",
                    r => r.HasOne<Semester>().WithMany()
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("semester_fk"),
                    l => l.HasOne<Grader>().WithMany()
                        .HasForeignKey("GraderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("grader_fk"),
                    j =>
                    {
                        j.HasKey("GraderId", "SemesterId").HasName("semester_graders_pk");
                        j.ToTable("semester_graders");
                        j.HasIndex(new[] { "SemesterId" }, "IX_semester_graders_semester_id");
                        j.IndexerProperty<int>("GraderId").HasColumnName("grader_id");
                        j.IndexerProperty<int>("SemesterId").HasColumnName("semester_id");
                    }); */
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("group_pk");

            entity.ToTable("groups");

            entity.HasIndex(e => new { e.SemesterId, e.Section, e.Number }, "uniq_group").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.Section).HasColumnName("section");
            entity.Property(e => e.SemesterId).HasColumnName("semester_id");

            entity.HasOne(d => d.Semester).WithMany(p => p.Groups)
                .HasForeignKey(d => d.SemesterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("semester_fk");

            //entity.HasMany(d => d.Students).WithOne(p => p.Group)
            //    .UsingEntity<Dictionary<string, object>>(
            //        "StudentGroup",
            //        r => r.HasOne<Student>().WithMany()
            //            .HasForeignKey("StudentId")
            //            .OnDelete(DeleteBehavior.ClientSetNull)
            //            .HasConstraintName("student_fk"),
            //        l => l.HasOne<Group>().WithMany()
            //            .HasForeignKey("GroupId")
            //            .OnDelete(DeleteBehavior.ClientSetNull)
            //            .HasConstraintName("group_fk"),
            //        j =>
            //        {
            //            j.HasKey("GroupId", "StudentId").HasName("student_groups_pk");
            //            j.ToTable("student_groups");
            //            j.HasIndex(new[] { "StudentId" }, "IX_student_groups_student_id");
            //            j.IndexerProperty<int>("GroupId").HasColumnName("group_id");
            //            j.IndexerProperty<int>("StudentId").HasColumnName("student_id");
            //        });
        });

        modelBuilder.Entity<Judge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("judge_pk");

            entity.ToTable("judges");

            entity.HasIndex(e => new { e.FName, e.LName, e.Affiliation }, "uniq_judge").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Affiliation)
                .HasMaxLength(35)
                .HasColumnName("affiliation");
            entity.Property(e => e.FName)
                .HasMaxLength(35)
                .HasColumnName("f_name");
            entity.Property(e => e.LName)
                .HasMaxLength(35)
                .HasColumnName("l_name");

            entity.HasMany(d => d.Presentations).WithMany(p => p.Judges)
                .UsingEntity<Dictionary<string, object>>(
                    "JudgePresentation",
                    r => r.HasOne<Presentation>().WithMany()
                        .HasForeignKey("PresentationId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("presentation_fk"),
                    l => l.HasOne<Judge>().WithMany()
                        .HasForeignKey("JudgeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("judge_fk"),
                    j =>
                    {
                        j.HasKey("JudgeId", "PresentationId").HasName("judge_presentations_pk");
                        j.ToTable("judge_presentations");
                        j.HasIndex(new[] { "PresentationId" }, "IX_judge_presentations_presentation_id");
                        j.IndexerProperty<int>("JudgeId").HasColumnName("judge_id");
                        j.IndexerProperty<int>("PresentationId").HasColumnName("presentation_id");
                    });
        });

        modelBuilder.Entity<Presentation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("presentation_pk");

            entity.ToTable("presentations");

            entity.HasIndex(e => new { e.GroupId, e.Location, e.StartDate }, "uniq_presentation").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.Location)
                .HasMaxLength(15)
                .HasColumnName("location");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.Group).WithMany(p => p.Presentations)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("group_fk");
        });

        modelBuilder.Entity<Ranking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ranking_pk");

            entity.ToTable("rankings");

            entity.HasIndex(e => e.JudgeId, "IX_rankings_judge_id");

            entity.HasIndex(e => new { e.GroupId, e.JudgeId }, "uniq_ranking").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CommunicationComments).HasColumnName("communication_comments");
            entity.Property(e => e.TechnologyComments).HasColumnName("technology_comments");
            entity.Property(e => e.OverallComments).HasColumnName("overall_comments");
            entity.Property(e => e.CommunicationPoints).HasColumnName("communication_points");
            entity.Property(e => e.TechnologyPoints).HasColumnName("technology_points");
            entity.Property(e => e.OverallPoints).HasColumnName("overall_points");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.JudgeId).HasColumnName("judge_id");
            entity.Property(e => e.Nomination).HasColumnName("nomination");
            entity.Property(e => e.TeamRanking).HasColumnName("team_ranking");

            entity.HasOne(d => d.Group).WithMany(p => p.Rankings)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("group_fk");

            entity.HasOne(d => d.Judge).WithMany(p => p.Rankings)
                .HasForeignKey(d => d.JudgeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("judge_fk");
        });

        modelBuilder.Entity<Requirement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("requirement_pk");

            entity.ToTable("requirements");

            entity.HasIndex(e => new { e.ClassId, e.Description }, "uniq_requirement").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Points).HasColumnName("points");

            entity.HasOne(d => d.Class).WithMany(p => p.Requirements)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("class_fk");
        });

        modelBuilder.Entity<Semester>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("semester_pk");

            entity.ToTable("semesters");

            entity.HasIndex(e => e.Term, "uniq_term").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Term)
                .HasMaxLength(6)
                .HasColumnName("term");
            entity.Property(e => e.Year).HasColumnName("year");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("student_pk");

            entity.ToTable("students");

            entity.Property(e => e.StudentId)
                .HasColumnName("id");
        });

        modelBuilder.Entity<Submission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("submission_pk");

            entity.ToTable("submissions");

            entity.HasIndex(e => e.StudentId, "IX_submissions_student_id");

            entity.HasIndex(e => new { e.GroupId, e.StudentId, e.CreatedDate }, "uniq_submission").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Group).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("group_fk");

            entity.HasOne(d => d.Student).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


}
