using PiLed.Devices.Implementations;
using PiLed.Display;
using PiLed.Models;

namespace PiLed
{
    internal class ThrobDisplay : IPixelDisplay
    {
        private IPixelDevice device;
        private PixelColor pixelColor;

        public ThrobDisplay(IPixelDevice device, PixelColor pixelColor)
        {
            this.device = device;
            this.pixelColor = pixelColor;
        }
    }
}