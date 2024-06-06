using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Natigtak.Models;

public partial class Natiga2023Context : DbContext
{
    public Natiga2023Context()
    {
    }

    public Natiga2023Context(DbContextOptions<Natiga2023Context> options)
        : base(options)
    {
    }

    public virtual DbSet<StageNewSearch> StageNewSearches { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StageNewSearch>(entity =>
        {
            entity.HasKey(e => e.SeatingNo).HasName("PK_Table");

            entity.ToTable("Stage_New_Search");

            entity.HasIndex(e => e.TotalDegree, "DegreeIX");

            entity.HasIndex(e => e.StudentCase, "StuCaseIX");

            entity.Property(e => e.SeatingNo).HasColumnName("seating_no");
            entity.Property(e => e.ArabicName)
                .HasMaxLength(255)
                .HasColumnName("arabic_name");
            entity.Property(e => e.CFlage).HasColumnName("c_flage");
            entity.Property(e => e.StudentCase).HasColumnName("student_case");
            entity.Property(e => e.StudentCaseDesc)
                .HasMaxLength(255)
                .HasColumnName("student_case_desc");
            entity.Property(e => e.TotalDegree).HasColumnName("total_degree");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
