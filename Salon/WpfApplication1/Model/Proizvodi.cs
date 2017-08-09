namespace WpfApplication1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prodaja.Proizvodi")]
    public partial class Proizvodi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Proizvodi()
        {
            DetaljiPorudzbines = new HashSet<DetaljiPorudzbine>();
        }

        [Key]
        public int ProizvodID { get; set; }

        public int SifraProizvoda { get; set; }

        [Required]
        [StringLength(50)]
        public string Naziv { get; set; }

        [StringLength(50)]
        public string Proizvodjac { get; set; }

        [Column(TypeName = "money")]
        public decimal Cena { get; set; }

        [StringLength(30)]
        public string Kategorija { get; set; }

        public int Stanje { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetaljiPorudzbine> DetaljiPorudzbines { get; set; }
    }
}
