using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace SmartNote
{
    public sealed class PriorityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return null;

            //if (parameter == null)
            //    return value;

            string ret;
            if ((int)value == 0)
            {
                ret = "Magas";
            }
            else if ((int)value == 1)
            {
                ret = "Közepes";
            }
            else
            {
                ret = "Alacsony";
            }

            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            string language)
        {
            throw new NotImplementedException();
        }
    }
}
