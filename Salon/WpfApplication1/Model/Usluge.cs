namespace WpfApplication1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Zakazivanje.Usluge")]
    public partial class Usluge
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usluge()
        {
            Terminis = new HashSet<Termini>();
        }

        [Key]
        public int UslugaID { get; set; }

        public int SifraUsluge { get; set; }

        [Required]
        [StringLength(100)]
        public string Naziv { get; set; }

        [StringLength(30)]
        public string Tip { get; set; }

        public int Trajanje { get; set; }

        [StringLength(300)]
        public string Opis { get; set; }

        [Column(TypeName = "money")]
        public decimal Cena { get; set; }

        [StringLength(500)]
        public string Primedbe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Termini> Terminis { get; set; }
        public override string ToString()
        {
            return Naziv + " " + Tip;
        }
    }
}
