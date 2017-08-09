using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class Artikal
    {
        public int ArtikalID { get; set; }
        public int SifraProizvoda { get; set; }
        public string Naziv { get; set; }
        public string Proizvodjac { get; set; }
        public decimal Cena { get; set; }
        public decimal Ukupno { get; set; }
        public int Kolicina { get; set; }

    }
}
