using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfApplication1
{
    class KonvertorBoja : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() == typeof(int))
            {
                int a = (int)value;
                if (a==2000)
                {
                    return Brushes.LightCyan;
                }
                if (a != 2000 && a>1500)
                {
                    return Brushes.LightCoral;
                }
                if (a == 999)
                {
                    return Brushes.LightPink;
                }
                if (a > 665 && a < 999)
                {
                    return Brushes.LightBlue;
                }
                if (a > 999 && a<1500) 
                {
                    return Brushes.LightGray;
                }
                return (a == 0) ? Brushes.LightGreen : Brushes.LightPink;
            }

            else
            {
                DateTime dt = (DateTime)value;
                return (dt.TimeOfDay == new TimeSpan(0, 0, 0)) ? Brushes.White : Brushes.Gray;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
