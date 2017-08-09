namespace WpfApplication1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Zakazivanje.Radnici")]
    public partial class Radnici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Radnici()
        {
            Terminis = new HashSet<Termini>();
            Terminis1 = new HashSet<Termini>();
        }

        [Key]
        public int RadnikID { get; set; }

        [Required]
        [StringLength(30)]
        public string Ime { get; set; }

        [Required]
        [StringLength(30)]
        public string Prezime { get; set; }

        public int Pozicija { get; set; }

        [StringLength(30)]
        public string Telefon { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DatumZaposlenja { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Termini> Terminis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Termini> Terminis1 { get; set; }
        public override string ToString()
        {
            return Ime + " " + Prezime;
        }
    }
}
