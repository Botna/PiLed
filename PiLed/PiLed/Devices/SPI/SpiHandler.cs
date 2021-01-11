using System.Device.Spi;

namespace PiLed.Devices.SPI
{
    public interface ISpiHandler
    {
        void FlushBytesToSpi(byte[] data);
    }
    public class SpiHandler : ISpiHandler
    {
        private readonly SpiConnectionSettings  _spiConnectionSettings;
        public SpiHandler(SpiConnectionSettings spiConnectionSettings)
        {
            _spiConnectionSettings = spiConnectionSettings;
        }

        public void FlushBytesToSpi(byte[] data)
        {
            var OurSpiPort = SpiDevice.Create(_spiConnectionSettings);
            OurSpiPort.Write(data);
            OurSpiPort.Dispose();
        }
    }
}
