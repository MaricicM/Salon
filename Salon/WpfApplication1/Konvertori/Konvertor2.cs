using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfApplication1
{
    class Konvertor2 : IValueConverter
    {
        private ZakazivanjeDAL zDAL = new ZakazivanjeDAL();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            int a = (int)value;
            if (a == 0)
            {
                return null;
            }
            if (a < 0)
            {
                string dan = ((DateTime)((MainWindow)System.Windows.Application.Current.MainWindow).pickerPocetniDatum.SelectedDate + new TimeSpan(-a-1,0,0,0)).DayOfWeek.ToString().Trim();
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
            Usluge us = zDAL.VratiUslugu(a);
            string rezultat = us.Naziv + " " + us.Tip;
            return rezultat;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
