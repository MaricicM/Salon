namespace WpfApplication1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Zakazivanje.Korisnici")]
    public partial class Korisnici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Korisnici()
        {
            Porudzbines = new HashSet<Porudzbine>();
            Terminis = new HashSet<Termini>();
        }

        [Key]
        public int KorisnikID { get; set; }

        [Required]
        [StringLength(30)]
        public string Ime { get; set; }

        [StringLength(30)]
        public string Prezime { get; set; }

        public int Pol { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DatumRodjenja { get; set; }

        [StringLength(30)]
        public string Telefon { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public DateTime DatumOtvaranjaDosijea { get; set; }

        public DateTime? DatumPoslednjeIntervencije { get; set; }

        [StringLength(500)]
        public string PoznatiAlergeni { get; set; }

        [StringLength(500)]
        public string Zabeleske { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Porudzbine> Porudzbines { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Termini> Terminis { get; set; }

        public override string ToString()
        {
            return Ime + " " + Prezime;
        }
    }
}
