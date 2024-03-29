using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ToaPro.Models;

public partial class WorkingdatabaseContext : DbContext
{
    public WorkingdatabaseContext()
    {
    }

    public WorkingdatabaseContext(DbContextOptions<WorkingdatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Evaluation> Evaluations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=workingdatabase.sqlite");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Evaluation>(entity =>
        {
            entity.HasKey(e => e.SubmissionId);

            entity.ToTable("evaluations");

            entity.Property(e => e.SubmissionId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
