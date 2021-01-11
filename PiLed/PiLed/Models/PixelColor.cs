using System;

namespace PiLed.Models
{
    public class PixelColor
    {
        /// <summary>
        /// Value for Hue in an HSV color, ranging from 0 to 360
        /// </summary>
        public double Hue { get; set; }

        /// <summary>
        /// Value for Saturation in an HSV color, ranging from 0 to 1
        /// </summary>
        public double Saturation { get; set; }
        /// <summary>
        /// Value for Value in an HSV color, ranging from 0 to 1
        /// </summary>
        public double Value { get; set; }

        public PixelColor(double h, double s, double v)
        {
            Hue = h;
            Saturation = s;
            Value = v;
        }

        public (int red, int green, int blue) ConvertToRGBTuple()
        {
            int hi = Convert.ToInt32(Math.Floor(Hue / 60)) % 6;
            double f = Hue / 60 - Math.Floor(Hue / 60);

            var tempValue = Value * 255;
            int v = Convert.ToInt32(tempValue);
            int p = Convert.ToInt32(tempValue * (1 - Saturation));
            int q = Convert.ToInt32(tempValue * (1 - f * Saturation));
            int t = Convert.ToInt32(tempValue * (1 - (1 - f) * Saturation));

            if (hi == 0)
                return (v, t, p);
            else if (hi == 1)
                return (q, v, p);
            else if (hi == 2)
                return (p, v, t);
            else if (hi == 3)
                return (p, q, v);
            else if (hi == 4)
                return (t, p, v);
            else
                return (v, p, q);
        }
    }
}
