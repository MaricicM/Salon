namespace WpfApplication1
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model2")
        {
        }

        public virtual DbSet<DetaljiPorudzbine> DetaljiPorudzbines { get; set; }
        public virtual DbSet<Porudzbine> Porudzbines { get; set; }
        public virtual DbSet<Proizvodi> Proizvodis { get; set; }
        public virtual DbSet<Korisnici> Korisnicis { get; set; }
        public virtual DbSet<Radnici> Radnicis { get; set; }
        public virtual DbSet<Termini> Terminis { get; set; }
        public virtual DbSet<Usluge> Usluges { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Porudzbine>()
                .Property(e => e.Total)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Porudzbine>()
                .HasMany(e => e.DetaljiPorudzbines)
                .WithRequired(e => e.Porudzbine)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Proizvodi>()
                .Property(e => e.Cena)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Proizvodi>()
                .HasMany(e => e.DetaljiPorudzbines)
                .WithRequired(e => e.Proizvodi)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Korisnici>()
                .HasMany(e => e.Terminis)
                .WithRequired(e => e.Korisnici)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Radnici>()
                .HasMany(e => e.Terminis)
                .WithOptional(e => e.Radnici)
                .HasForeignKey(e => e.AdministratorID);

            modelBuilder.Entity<Radnici>()
                .HasMany(e => e.Terminis1)
                .WithRequired(e => e.Radnici1)
                .HasForeignKey(e => e.RadnikID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usluge>()
                .Property(e => e.Cena)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Usluge>()
                .HasMany(e => e.Terminis)
                .WithRequired(e => e.Usluge)
                .WillCascadeOnDelete(false);
        }
    }
}
