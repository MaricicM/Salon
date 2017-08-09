using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfApplication1
{
    class Konvertor1 : IValueConverter
    {
        private ZakazivanjeDAL zDAL = new ZakazivanjeDAL();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                int a = (int)value;
                if (a < 0)
                {
                    DateTime rez = (DateTime)((MainWindow)System.Windows.Application.Current.MainWindow).pickerPocetniDatum.SelectedDate + new TimeSpan(-a - 1, 0, 0, 0);
                    return rez.ToShortDateString();
                }
                string rezultat = null;
                if (a != 0)
                {
                    Korisnici kor = zDAL.VratiKorisnika(a);
                    rezultat = kor.Ime + " " + kor.Prezime;
                }                
                return rezultat;
            }
            return null;            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
