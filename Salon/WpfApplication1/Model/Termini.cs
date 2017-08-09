namespace WpfApplication1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Zakazivanje.Termini")]
    public partial class Termini
    {
        [Key]
        public int TerminID { get; set; }

        public DateTime DatumVreme { get; set; }

        public int KorisnikID { get; set; }

        public int RadnikID { get; set; }

        public int? AdministratorID { get; set; }

        public int UslugaID { get; set; }

        public virtual Korisnici Korisnici { get; set; }

        public virtual Radnici Radnici { get; set; }

        public virtual Radnici Radnici1 { get; set; }

        public virtual Usluge Usluge { get; set; }
    }
}
