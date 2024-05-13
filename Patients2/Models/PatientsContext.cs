using Microsoft.EntityFrameworkCore;

namespace Patients2.Models;

public partial class PatientsContext : DbContext
{
    public PatientsContext()
    {
        Database.EnsureCreated();
    }

    public PatientsContext(DbContextOptions<PatientsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Artery> Arteries { get; set; }

    public virtual DbSet<Availability> Availabilities { get; set; }

    public virtual DbSet<BasicInputCalculation> BasicInputCalculations { get; set; }

    public virtual DbSet<BasicInputPatient> BasicInputPatients { get; set; }

    public virtual DbSet<Complaint> Complaints { get; set; }

    public virtual DbSet<Diagnosis> Diagnoses { get; set; }

    public virtual DbSet<ImtMean> ImtMeans { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Sex> Sexes { get; set; }

    public virtual DbSet<SocialStatus> SocialStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=patients.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artery>(entity =>
        {
            entity.ToTable("arteries");

            entity.HasIndex(e => e.Id, "IX_arteries_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Artery1).HasColumnName("artery");
        });

        modelBuilder.Entity<BasicInputCalculation>(entity =>
        {
            entity.ToTable("basic_input_calculations");

            entity.HasIndex(e => e.Id, "IX_basic_input_calculations_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Avad)
                .HasColumnType("INTEGER")
                .HasColumnName("avad");
            entity.Property(e => e.Imt)
                .HasColumnType("INTEGER")
                .HasColumnName("imt");
            entity.Property(e => e.ImtMean).HasColumnName("imt_mean");
            entity.Property(e => e.Patient).HasColumnName("patient");

            entity.HasOne(d => d.PatientNavigation).WithMany(p => p.BasicInputCalculations).HasForeignKey(d => d.Patient);

            entity.HasOne(d => d.ImtMeanNavigation).WithMany(p => p.BasicInputCalculations).HasForeignKey(d => d.ImtMean);
        });

        modelBuilder.Entity<BasicInputPatient>(entity =>
        {
            entity.ToTable("basic_input_patient");

            entity.HasIndex(e => e.Id, "IX_basic_input_patient_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AffectedArtery).HasColumnName("affected_artery");
            entity.Property(e => e.Athero).HasColumnName("athero");
            entity.Property(e => e.Complaints).HasColumnName("complaints");
            entity.Property(e => e.Css).HasColumnName("css");
            entity.Property(e => e.Dad).HasColumnName("dad");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.MainDiagnosis).HasColumnName("main_diagnosis");
            entity.Property(e => e.Patient).HasColumnName("patient");
            entity.Property(e => e.Pd).HasColumnName("pd");
            entity.Property(e => e.Sad).HasColumnName("sad");
            entity.Property(e => e.Weight)
                .HasColumnType("INTEGER")
                .HasColumnName("weight");

            entity.HasOne(d => d.AffectedArteryNavigation).WithMany(p => p.BasicInputPatients).HasForeignKey(d => d.AffectedArtery);

            entity.HasOne(d => d.ComplaintsNavigation).WithMany(p => p.BasicInputPatients).HasForeignKey(d => d.Complaints);

            entity.HasOne(d => d.MainDiagnosisNavigation).WithMany(p => p.BasicInputPatients).HasForeignKey(d => d.MainDiagnosis);

            entity.HasOne(d => d.PatientNavigation).WithMany(p => p.BasicInputPatients).HasForeignKey(d => d.Patient);

            entity.HasOne(d => d.AvailabilityNavigation).WithMany(p => p.BasicInputPatients).HasForeignKey(d => d.Athero);
        });

        modelBuilder.Entity<Complaint>(entity =>
        {
            entity.ToTable("complaints");

            entity.HasIndex(e => e.Id, "IX_complaints_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Complaint1).HasColumnName("complaint");
        });

        modelBuilder.Entity<Diagnosis>(entity =>
        {
            entity.ToTable("diagnoses");

            entity.HasIndex(e => e.Id, "IX_diagnoses_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Diagnosis1).HasColumnName("diagnosis");
        });

        modelBuilder.Entity<ImtMean>(entity =>
        {
            entity.ToTable("imt_means");

            entity.HasIndex(e => e.Id, "IX_imt_means_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Mean).HasColumnName("mean");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("locations");

            entity.HasIndex(e => e.Id, "IX_locations_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Location1).HasColumnName("location");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("patients");

            entity.HasIndex(e => e.Id, "IX_patients_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Fullname).HasColumnName("fullname");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.MedicalHistory).HasColumnName("medical_history");
            entity.Property(e => e.Sex).HasColumnName("sex");
            entity.Property(e => e.SocialStatus).HasColumnName("social_status");

            entity.HasOne(d => d.LocationNavigation).WithMany(p => p.Patients).HasForeignKey(d => d.Location);

            entity.HasOne(d => d.SexNavigation).WithMany(p => p.Patients).HasForeignKey(d => d.Sex);

            entity.HasOne(d => d.SocialStatusNavigation).WithMany(p => p.Patients).HasForeignKey(d => d.SocialStatus);
        });

        modelBuilder.Entity<Sex>(entity =>
        {
            entity.ToTable("sex");

            entity.HasIndex(e => e.Id, "IX_sex_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Sex1).HasColumnName("sex");
        });

        modelBuilder.Entity<Availability>(entity =>
        {
            entity.ToTable("availability");

            entity.HasIndex(e => e.Id, "IX_availability_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Availability1).HasColumnName("availability");
        });

        modelBuilder.Entity<SocialStatus>(entity =>
        {
            entity.ToTable("social_statuses");

            entity.HasIndex(e => e.Id, "IX_social_statuses_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
