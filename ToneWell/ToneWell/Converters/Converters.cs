using System;
using System.Collections.Generic;
using System.Text;

namespace ToneWell.Converters
{
    public static class Converters
    {
        public static GetImageFromTrackConverter GetImageFromTrackConverter { get; private set; } = new GetImageFromTrackConverter();

        public static SvgToImageSourceConverter SvgToImageSourceConverter { get; private set; } = new SvgToImageSourceConverter();
    }
}
