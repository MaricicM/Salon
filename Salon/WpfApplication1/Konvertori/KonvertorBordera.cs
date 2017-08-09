using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WpfApplication1
{
    class KonvertorBordera : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int a = (int)value;
            return (a == 0) ? new Thickness(1, 1, 1, 1) : (a > 0 && a < 665) ? new Thickness(1, 1, 1, 0) : new Thickness(1, 0, 1, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
