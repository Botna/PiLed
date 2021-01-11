using PiLed.Devices.Config;
using PiLed.Devices.SPI;
using PiLed.Models;
using System.Device.Spi;

namespace PiLed.Devices.Implementations
{
    public interface IPixelDevice
    {
        ISpiHandler _spi { get; set; }
        SpiConnectionSettings _spiConnectionSettings { get;}
        PixelConfig _config { get; set; }

        void FlushColorToLeds(params PixelBuffer[] buffer);

        void ClearLeds();
    }
}
