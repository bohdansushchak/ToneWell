using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Xamarin.Forms;

namespace ToneWell.Converters
{
    public class GetImageFromTrackConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var filePath = value as string;

                try
                {
                    TagLib.File tagFile = TagLib.File.Create(filePath); //can throw exception: mpeg header not found

                    //TODO: потрібно доробити щоб закривати стрим

                    var mStream = new MemoryStream();
                    var firstPicture = tagFile.Tag.Pictures.FirstOrDefault();
                    if (firstPicture != null)
                    {
                        byte[] pData = firstPicture.Data.Data;
                        mStream.Write(pData, 0, System.Convert.ToInt32(pData.Length));

                        System.Diagnostics.Debug.WriteLine("pData.Length: " + pData.Length);
                        System.Diagnostics.Debug.WriteLine("mStream.Length: " + mStream.Length);

                        var source = StreamImageSource.FromStream(() => mStream);

                        return source;
                    }
                }
                catch(Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }

            return parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
