using PiLed.Devices.Config;
using PiLed.Devices.SPI;
using PiLed.Models;
using System;
using System.Diagnostics;

namespace PiLed.Devices.Implementations
{
    public class WS2801PixelDevice : IPixelDevice
    {
        public ISpiHandler _spi { get; set; }
        public PixelConfig _config { get; set; }

        private readonly Stopwatch _stopwatch;

        public WS2801PixelDevice(PixelConfig config, ISpiHandler handler)
        {
            _spi = handler;
            _config = config;
            _stopwatch = Stopwatch.StartNew();
        }

        public void FlushColorToLeds(params PixelBuffer[] buffers)
        {
            var payloadBytes = new byte[_config.NumLeds * 3];
            foreach (var buffer in buffers)
            {
                foreach (var index in buffer.PixelIndices)
                {
                    try
                    {
                        (var r, var g, var b) = buffer.Color.ConvertToRGBTuple();

                        payloadBytes[index * 3] = (byte)r;
                        payloadBytes[(index * 3) + 1] = (byte)g;
                        payloadBytes[(index * 3) + 2] = (byte)b;
                    }
                    catch (Exception ex)
                    {
                        Console.Write("Exception caught: " + ex.ToString());
                    }
                }
            }

            while (_stopwatch.ElapsedMilliseconds < _config.FlushRate) { }
            _spi.FlushBytesToSpi(payloadBytes);
            _stopwatch.Restart();
        }

        public void ClearLeds()
        {
            var payloadBytes = new byte[_config.NumLeds * 3];

            for(int i = 0; i< payloadBytes.Length; i++)
            {
                payloadBytes[i] = 0;
            }

            while (_stopwatch.ElapsedMilliseconds < _config.FlushRate) { }
            _spi.FlushBytesToSpi(payloadBytes);
            _stopwatch.Restart();
        }
    }
}
