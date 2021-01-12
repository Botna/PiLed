using PiLed.Devices.Implementations;
using System.Threading;

namespace PiLed.Display
{
    public class RainbowColorDisplay
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

        public RainbowColorDisplay(IPixelDevice device, RainbowType type = RainbowType.Led)
        {
            _pixelDevice = device;
            _type = type;
        }

        public void Start(CancellationToken token = default)
        {
            
        }
    }
}