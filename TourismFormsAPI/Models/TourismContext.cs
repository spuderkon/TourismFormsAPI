using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TourismFormsAPI.Models;

public partial class TourismContext : DbContext
{
    public TourismContext()
    {
    }

    public TourismContext(DbContextOptions<TourismContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Criteria> Criteria { get; set; }

    public virtual DbSet<FillMethod> FillMethods { get; set; }

    public virtual DbSet<Form> Forms { get; set; }

    public virtual DbSet<Measure> Measures { get; set; }

    public virtual DbSet<Municipality> Municipalities { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Survey> Surveys { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Name=ConnectionStrings:Tourism",
                builder => builder.EnableRetryOnFailure()
            );

        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.ToTable("Answer");

            entity.Property(e => e.Text).HasMaxLength(50);

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Answer_Question");

            entity.HasOne(d => d.Survey).WithMany(p => p.Answers)
                .HasForeignKey(d => d.SurveyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Answer_Survey");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("City");

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Municipality).WithMany(p => p.Cities)
                .HasForeignKey(d => d.MunicipalityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_City_Municipality");
        });

        modelBuilder.Entity<Criteria>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Form).WithMany(p => p.Criterias)
                .HasForeignKey(d => d.FormId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Criteria_Form");
        });

        modelBuilder.Entity<FillMethod>(entity =>
        {
            entity.ToTable("FillMethod");
        });

        modelBuilder.Entity<Form>(entity =>
        {
            entity.ToTable("Form");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Measure>(entity =>
        {
            entity.ToTable("Measure");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Municipality>(entity =>
        {
            entity.ToTable("Municipality");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Login).HasMaxLength(50);

            entity.HasOne(d => d.Region).WithMany(p => p.Municipalities)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Municipality_Region");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.ToTable("Question");

            entity.Property(e => e.Formula).HasMaxLength(150);

            entity.HasOne(d => d.Criteria).WithMany(p => p.Questions)
                .HasForeignKey(d => d.CriteriaId)
                .HasConstraintName("FK_Question_Criteria");

            entity.HasOne(d => d.FillMethod).WithMany(p => p.Questions)
                .HasForeignKey(d => d.FillMethodId)
                .HasConstraintName("FK_Question_FillMethod");

            entity.HasOne(d => d.Measure).WithMany(p => p.Questions)
                .HasForeignKey(d => d.MeasureId)
                .HasConstraintName("FK_Question_Measure");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.ToTable("Region");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Survey>(entity =>
        {
            entity.ToTable("Survey");

            entity.Property(e => e.AppointmentDate).HasColumnType("datetime");
            entity.Property(e => e.CityName).HasMaxLength(50);
            entity.Property(e => e.CompletionDate).HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.MunicipalityName).HasMaxLength(100);
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.City).WithMany(p => p.Surveys)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Survey_City");

            entity.HasOne(d => d.Form).WithMany(p => p.Surveys)
                .HasForeignKey(d => d.FormId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Survey_Form");

            entity.HasOne(d => d.Municipality).WithMany(p => p.Surveys)
                .HasForeignKey(d => d.MunicipalityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Survey_Municipality");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
