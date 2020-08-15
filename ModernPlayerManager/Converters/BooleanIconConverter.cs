using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ModernPlayerManager.Converters
{
    public class BooleanIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return ((bool) value) ? "\uF13E" : "\uF13D";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            return value as string == "\uF13E" ? true : false;
        }
    }
}
