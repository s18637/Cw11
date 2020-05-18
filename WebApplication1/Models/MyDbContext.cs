using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class MyDbContext : DbContext
    {
        public DbSet<Medicament> Medicament { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<PrescriptionMedicament> prescriptionMedicament { get; set; }

        public MyDbContext(DbContextOptions options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medicament>(e =>
            {
                e.HasKey(k => k.IdMedicament).HasName("Medicament_Key");
                e.Property(p => p.Name).HasMaxLength(100).IsRequired();
                e.Property(p => p.Description).HasMaxLength(100).IsRequired();
                e.Property(p => p.Type).HasMaxLength(100).IsRequired();

            });
            modelBuilder.Entity<Doctor>(e =>
            {
                e.HasKey(k => k.IdDoctor).HasName("Doctor_Key");
                e.Property(p => p.FirstName).HasMaxLength(100).IsRequired();
                e.Property(p => p.LastName).HasMaxLength(100).IsRequired();
                e.Property(p => p.Email).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<Patient>(e =>
            {
                e.HasKey(k => k.IdPatient).HasName("Patient_key");
                e.Property(p => p.FirstName).HasMaxLength(100).IsRequired();
                e.Property(p => p.LastName).HasMaxLength(100).IsRequired();
                e.Property(p => p.BirthDate).IsRequired();

            });
            modelBuilder.Entity<Prescription>(e =>
            {
                e.HasKey(k => k.IdPrescription).HasName("Prescription_Key");
                e.Property(p => p.DueDate).IsRequired();
                e.Property(p => p.Date).IsRequired();

                e.HasOne(d => d.Doctor)
                .WithMany(d => d.Prescriptions)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Doctor_Prescription");

                e.HasOne(d => d.Patient)
                .WithMany(d => d.Prescription)
                .HasForeignKey(d => d.IdPatient)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Patient_Prescription");
                
            });
            modelBuilder.Entity<PrescriptionMedicament>(e =>
            {
                e.HasKey(k => k.IdMedicament).HasName("Medicament_FKey");
                e.HasKey(k => k.IdPrescription).HasName("Prescription_FKey");
                e.Property(p => p.Details).HasMaxLength(100).IsRequired();

                e.ToTable("Prescription_Medicament");

                e.HasOne(d => d.Medicament)
                .WithMany(d => d.PrescriptionMedicament)
                .HasForeignKey(d => d.IdMedicament)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Medicament_Prescription");

                e.HasOne(d => d.Prescription)
                .WithMany(d => d.PrescriptionMedicament)
                .HasForeignKey(d => d.IdPrescription)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Prescription_Medicaments");
            });
        }
    }
}
