namespace WpfApplication1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prodaja.Porudzbine")]
    public partial class Porudzbine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Porudzbine()
        {
            DetaljiPorudzbines = new HashSet<DetaljiPorudzbine>();
        }

        [Key]
        public int PorudzbinaID { get; set; }

        public DateTime DatumPorudzbine { get; set; }

        [Column(TypeName = "money")]
        public decimal Total { get; set; }

        public int? KorisnikID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetaljiPorudzbine> DetaljiPorudzbines { get; set; }

        public virtual Korisnici Korisnici { get; set; }
    }
}
