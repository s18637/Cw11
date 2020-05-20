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
                IEnumerable<Medicament> medicaments = new List<Medicament>
                {
                    new Medicament { Name = "Paracetamol", Description = "Przeciwbolowy lek", Type = "Przeciwbolwe", IdMedicament = 1},
                    new Medicament { Name = "Wiagra", Description = "Lek dla mezczyzn z problemami erekcji", Type = "Pobudzjący", IdMedicament = 2},
                    new Medicament { Name = "Marihuana", Description = "Lecznicza Mariuhuana, niska zawartość THC", Type = "Przeciwbolowe, odurzajace", IdMedicament = 3}
                };
                e.HasData(medicaments);

            });
            modelBuilder.Entity<Doctor>(e =>
            {
                e.HasKey(k => k.IdDoctor).HasName("Doctor_Key");
                e.Property(p => p.FirstName).HasMaxLength(100).IsRequired();
                e.Property(p => p.LastName).HasMaxLength(100).IsRequired();
                e.Property(p => p.Email).HasMaxLength(100).IsRequired();

                IEnumerable<Doctor> doctors = new List<Doctor>
                {
                    new Doctor { FirstName = "Andrzej", LastName = "Jezrdna", Email = "andrzej@wp.pl", IdDoctor=1},
                    new Doctor { FirstName = "Filip", LastName = "Pilif", Email = "dilip@wp.pl", IdDoctor = 2 },
                    new Doctor { FirstName = "Katarzyna", LastName = "Anyzratak", Email = "kasia@wp.pl", IdDoctor = 3}
                };

                e.HasData(doctors);
            });

            modelBuilder.Entity<Patient>(e =>
            {
                e.HasKey(k => k.IdPatient).HasName("Patient_key");
                e.Property(p => p.FirstName).HasMaxLength(100).IsRequired();
                e.Property(p => p.LastName).HasMaxLength(100).IsRequired();
                e.Property(p => p.BirthDate).IsRequired();

                IEnumerable<Patient> patients = new List<Patient>
                {
                    new Patient { FirstName = "Konrad", LastName = "Darnok", BirthDate = DateTime.Parse("20.08.1995"), IdPatient=1},
                    new Patient { FirstName = "Malgorzata", LastName = "Atazroglam", BirthDate = DateTime.Parse("06.02.1982"), IdPatient=2},
                    new Patient { FirstName = "Dawid", LastName = "Diwad", BirthDate = DateTime.Parse("01.01.1963"), IdPatient =3}
                };
                e.HasData(patients);

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

                IEnumerable<Prescription> prescriptions = new List<Prescription>
                {
                    new Prescription { DueDate = DateTime.Parse("03.08.2020"), Date = DateTime.Parse("03.02.2020"), IdDoctor=2, IdPatient = 2, IdPrescription=1 },
                    new Prescription { DueDate = DateTime.Parse("04.06.2020"), Date = DateTime.Parse("02.01.2020"), IdDoctor = 1, IdPatient=1, IdPrescription =2},
                    new Prescription { DueDate = DateTime.Parse("10.09.2020"), Date = DateTime.Parse("05.03.2020"), IdDoctor = 3, IdPatient=3, IdPrescription =3},
                    new Prescription { DueDate = DateTime.Parse("15.06.2020"), Date = DateTime.Parse("05.12.2019"), IdDoctor =2, IdPatient = 1, IdPrescription =4}
                };
                e.HasData(prescriptions);

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

                IEnumerable<PrescriptionMedicament> prescriptionMedicaments = new List<PrescriptionMedicament> 
                {
                    new PrescriptionMedicament { Details="aaaa", Dose= 2, IdMedicament = 1, IdPrescription = 2},
                    new PrescriptionMedicament { Details ="bbbb",Dose=3, IdMedicament=2, IdPrescription = 1 },
                    new PrescriptionMedicament { Details =  "cccc",Dose =1, IdMedicament =3, IdPrescription=3}
                };
                e.HasData(prescriptionMedicaments);
            });
        }
    }
}
