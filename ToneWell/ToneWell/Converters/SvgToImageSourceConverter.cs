using FFImageLoading.Svg.Forms;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace ToneWell.Converters
{
    public class SvgToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = parameter as string;
            if (!string.IsNullOrEmpty(str))
            {
                var on = SvgImageSource.FromFile(str, vectorWidth: 36, vectorHeight: 36);
                return on;
            }
            else return null; //Icon="{Binding ., Converter={x:Static converter:Converters.SvgToImageSourceConverter}, ConverterParameter='settings.svg'}"
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
