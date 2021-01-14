using PiLed.Devices.Implementations;
using PiLed.Models;
using System.Collections.Generic;
using System.Threading;

namespace PiLed.Display
{
    public class RainbowColorDisplay : IPixelDisplay
    {
        private readonly RainbowType _type;
        public enum RainbowType
        {
            //All Leds will be the same color and cycle through the rainbow.
            Led,
            //A single LED will represent a single color, with the entirity of the list of LED's cycling through the color spectrum.
            ColorWheel
        }

        public IPixelDevice _pixelDevice { get; set; }
        public int _numLeds { get; set; }

        public RainbowColorDisplay(IPixelDevice device, RainbowType type = RainbowType.Led)
        {
            _pixelDevice = device;
            _numLeds = device._config.NumLeds;
            _type = type;
        }

        public void Start(CancellationToken token = default)
        {
            switch (_type)
            {
                case RainbowType.Led: BuildLedBasedRainbowDisplay(token); break;
                case RainbowType.ColorWheel: BuildColorWheelBasedRainbowDisplay(token); break;
                default: break;
            }
        }

        private void BuildLedBasedRainbowDisplay(CancellationToken token)
        {
            var numLeds = _pixelDevice._config.NumLeds;
            var buffer = new PixelBuffer();

            buffer.PixelIndices = new int[numLeds];
            for (int i = 0; i < numLeds; i++)
            {
                buffer.PixelIndices[i] = i;
            }

            var iteration = 0;
            while (!token.IsCancellationRequested)
            {
                iteration = iteration % 3600;
                buffer.Color = new PixelColor(iteration / 10, 1, 1);
                ((IPixelDisplay)this).Flush(buffer);
                iteration++;
            }
            _pixelDevice.ClearLeds();
        }

        private void BuildColorWheelBasedRainbowDisplay(CancellationToken token)
        {
            var iteration = 0;
            while (!token.IsCancellationRequested)
            {
                iteration = iteration % 360;

                List<PixelBuffer> buffers = new List<PixelBuffer>();
                for (int i = 0; i < _numLeds; i++)
                {
                    var buffer = new PixelBuffer();
                    buffer.PixelIndices = new int[1] { i };

                    var hueValue = (i * (360 / _numLeds) + iteration) % 360;
                    buffer.Color = new PixelColor(hueValue, 1, 1);
                    buffers.Add(buffer);
                }
                iteration++;
                ((IPixelDisplay)this).Flush(buffers.ToArray());
            }
            _pixelDevice.ClearLeds();
        }
    }
}