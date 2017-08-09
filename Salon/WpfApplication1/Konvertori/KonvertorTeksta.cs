using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfApplication1
{
    class KonvertorTeksta : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //ZakazivanjeDAL zDAL = new ZakazivanjeDAL();
            MainWindow mw = (MainWindow)System.Windows.Application.Current.MainWindow;
            int a = (int)value;
            if (a == 0)
            {
                return null;
            }
            if (a > 1009 && a < 1500) 
            {
                string dan = ((DateTime)mw.pickerPocetniDatum.SelectedDate + new TimeSpan(a-1010, 0, 0, 0)).DayOfWeek.ToString().Trim();
                if (dan == "Monday")
                {
                    return "Ponedeljak";
                }
                if (dan == "Tuesday")
                {
                    return "Utorak";
                }
                if (dan == "Wednesday")
                {
                    return "Sreda";
                }
                if (dan == "Thursday")
                {
                    return "Cetvrtak";
                }
                if (dan == "Friday")
                {
                    return "Petak";
                }
                if (dan == "Saturday")
                {
                    return "Subota";
                }
                if (dan == "Sunday")
                {
                    return "Nedelja";
                }
            }
            if (a > 999 && a < 1500) 
            {
                DateTime rez = (DateTime)mw.pickerPocetniDatum.SelectedDate + new TimeSpan(a-1000, 0, 0, 0);
                return rez.ToShortDateString();
            }
            if (a == 999 || a == 2999)  
            {
                return null;
            }
            if (a == 666) 
            {
                return "Bojana";
            }
            if (a == 777) 
            {
                return "Jovana";
            }
            if (a == 888) 
            {
                return "Ivana";
            }
            if (a > 0 && a < 1500) 
            {
                Usluge us = mw.pretragaUsluga.First(u=> u.UslugaID == a);
                return us.Naziv + " " + us.Tip;
            }
            if (a > 2000)
            {
                Usluge us = mw.pretragaUsluga.First(u => u.UslugaID == a - 2000);
                return us.Naziv + " " + us.Tip;
            }
            if (a < 0) 
            {
                Korisnici kor = mw.pretragaKorisnika.First(k => k.KorisnikID == -a);
                return kor.Ime + " " + kor.Prezime;
            }
            if (a > 1500 && a < 2000)
            {
                Korisnici kor = mw.pretragaKorisnika.First(k => k.KorisnikID == -a + 2000);
                return kor.Ime + " " + kor.Prezime;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
