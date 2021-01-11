using PiLed.Devices.Implementations;
using PiLed.Models;
using System.Threading;

namespace PiLed.Display
{
    public class SolidColorDisplay : IPixelDisplay
    {
        public IPixelDevice _pixelDevice { get; set; }
        private readonly PixelColor _color;

        public SolidColorDisplay(IPixelDevice device, PixelColor color)
        {
            _pixelDevice = device;
            _color = color;
        }

        public void Start(CancellationToken token)
        {
            var numLeds = _pixelDevice._config.NumLeds;

            var buffer = new PixelBuffer();
            buffer.PixelIndices = new int[numLeds];
            for (int i = 0; i < numLeds; i++)
            {
                buffer.PixelIndices[i] = i;
            }

            buffer.Color = _color;

            ((IPixelDisplay)this).Flush(buffer);

            while (!token.IsCancellationRequested)
            { }
            _pixelDevice.ClearLeds();
        }
    }
}
