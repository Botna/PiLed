using PiLed.Devices.Config;
using PiLed.Devices.SPI;
using PiLed.Models;

namespace PiLed.Devices.Implementations
{
    public interface IPixelDevice
    {
        ISpiHandler _spi { get; set; }
        PixelConfig _config { get; set; }

        void FlushColorToLeds(params PixelBuffer[] buffer);
    }
}
