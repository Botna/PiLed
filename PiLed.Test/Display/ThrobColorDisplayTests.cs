using Moq;
using NUnit.Framework;
using PiLed.Devices.Config;
using PiLed.Devices.Implementations;
using PiLed.Display;
using PiLed.Models;
using System.Threading;
using System.Threading.Tasks;

namespace PiLed.Test.Display
{
    public class ThrobColorDisplayTests
    {
        Mock<IPixelDevice> _pixelDeviceMock;
        PixelConfig _config;
        [SetUp]
        public void Setup()
        {
            _pixelDeviceMock = new Mock<IPixelDevice>();
            _config = new PixelConfig()
            {
                FlushRate = 10,
                NumLeds = 1
            };
        }

        [Test]
        public void ThrobColorDisplayTests_Happy()
        {
            var color = new PixelColor(0, 1, 1);

            _pixelDeviceMock.SetupGet(x => x._config).Returns(_config);
            _pixelDeviceMock.Setup(x => x.FlushColorToLeds(It.IsAny<PixelBuffer>()));

            var display = new ThrobColorDisplay(_pixelDeviceMock.Object, color);

            var cs = new CancellationTokenSource();
            Task.Run(()=> display.Start(cs.Token));

            //Not long enough for the full throb effect, but long enough for multiple Flushes.
            Thread.Sleep(5);
            cs.Cancel();
            _pixelDeviceMock.VerifyAll();
            _pixelDeviceMock.Verify(x => x.FlushColorToLeds(It.Is<PixelBuffer>(x => x.Color.Value == 1)), times: Times.AtLeastOnce);
            _pixelDeviceMock.Verify(x => x.FlushColorToLeds(It.Is<PixelBuffer>(x => x.Color.Value == 0)), times: Times.AtLeastOnce);
            _pixelDeviceMock.Verify(x => x.FlushColorToLeds(It.Is<PixelBuffer>(x => x.Color.Saturation != 1)), times: Times.Never);
        }


        [Test]
        public void ThrobColorDisplayTests_HappyWithMultipleColors()
        {
            var colors = new PixelColor[3];
            colors[0] = new PixelColor(0, 1, 1);
            colors[1] = new PixelColor(120, 1, 1);
            colors[2] = new PixelColor(240, 1, 1);

            _pixelDeviceMock.SetupGet(x => x._config).Returns(_config);
            _pixelDeviceMock.Setup(x => x.FlushColorToLeds(It.IsAny<PixelBuffer>()));

            var display = new ThrobColorDisplay(_pixelDeviceMock.Object, colors);

            var cs = new CancellationTokenSource();
            Task.Run(() => display.Start(cs.Token));

            //Not long enough for the full throb effect, but long enough for multiple Flushes.
            Thread.Sleep(5);
            cs.Cancel();
            _pixelDeviceMock.VerifyAll();
            _pixelDeviceMock.Verify(x => x.FlushColorToLeds(It.Is<PixelBuffer>(x => x.Color.Value == 1 && x.Color.Hue == 0)), times: Times.AtLeastOnce);
            _pixelDeviceMock.Verify(x => x.FlushColorToLeds(It.Is<PixelBuffer>(x => x.Color.Value == 1 && x.Color.Hue == 120)), times: Times.AtLeastOnce);
            _pixelDeviceMock.Verify(x => x.FlushColorToLeds(It.Is<PixelBuffer>(x => x.Color.Value == 1 && x.Color.Hue == 240)), times: Times.AtLeastOnce);
            _pixelDeviceMock.Verify(x => x.FlushColorToLeds(It.Is<PixelBuffer>(x => x.Color.Value == 0)), times: Times.AtLeastOnce);
            _pixelDeviceMock.Verify(x => x.FlushColorToLeds(It.Is<PixelBuffer>(x => x.Color.Saturation != 1)), times: Times.Never);
        }
    }
}
