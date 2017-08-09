namespace WpfApplication1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prodaja.DetaljiPorudzbine")]
    public partial class DetaljiPorudzbine
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PorudzbinaID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProizvodID { get; set; }

        public int Kolicina { get; set; }

        public virtual Porudzbine Porudzbine { get; set; }

        public virtual Proizvodi Proizvodi { get; set; }
    }
}
