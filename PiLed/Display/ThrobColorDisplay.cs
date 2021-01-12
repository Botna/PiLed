using PiLed.Devices.Implementations;
using PiLed.Models;
using System.Threading;

namespace PiLed.Display
{
    public class ThrobColorDisplay : IPixelDisplay
    {
        public IPixelDevice _pixelDevice { get; set; }
        private readonly PixelColor[] _colors;

        public ThrobColorDisplay(IPixelDevice device, params PixelColor[] colors)
        {
            _pixelDevice = device;
            _colors = colors;
        }

        public void Start(CancellationToken token = default)
        {
            var numLeds = _pixelDevice._config.NumLeds;


            var iterator = 99;
            var decrease = true;
            var iterationValue = 1;

            var colorIndex = 0;
            var color = _colors[colorIndex];
            while(!token.IsCancellationRequested)
            {
                var sat = color.Saturation;
                var val = color.Value;

                if(iterator % 100 == 0)
                {
                    if(iterator == 0)
                    {
                        decrease = false;
                    }
                    else
                    {
                        decrease = true;
                        colorIndex = (colorIndex + 1) % _colors.Length;
                        color = _colors[colorIndex];
                    }
                }

                iterator = decrease
                    ? iterator - iterationValue
                    : iterator + iterationValue;

                sat = sat - (iterator / 100);
                val = val - (iterator / 100);

                var tempColor = new PixelColor(color.Hue, sat, val);
                var buffer = new PixelBuffer();
                buffer.PixelIndices = new int[numLeds];
                for (int i = 0; i < numLeds; i++)
                {
                    buffer.PixelIndices[i] = i;
                }

                buffer.Color = tempColor;

                ((IPixelDisplay)this).Flush(buffer);
            }
        }
    }
}
