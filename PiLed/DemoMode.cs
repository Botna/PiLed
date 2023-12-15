using PiLed.Devices.Implementations;
using PiLed.Display;
using PiLed.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using static PiLed.Display.RainbowColorDisplay;

namespace PiLed
{
    public class DemoMode
    {
        private readonly List<IPixelDisplay> _displays;
        private int _demoDisplayTime = 30000;

        public DemoMode(IPixelDevice device)
        {
            _displays = new List<IPixelDisplay>
            {
                new ThrobColorDisplay(device, new PixelColor(0, 1, 1)),
                new RainbowColorDisplay(device, RainbowType.Led),
                new ThrobColorDisplay(device, new PixelColor(120, 1, 1)),
                new RainbowColorDisplay(device, RainbowType.ColorWheel),
                new ThrobColorDisplay(device, new PixelColor(240, 1, 1)),
                new RainbowColorDisplay(device, RainbowType.Led)
            };
        }

        public void Start(CancellationToken demoToken = default)
        {
            var iterator = 0;

            while (!demoToken.IsCancellationRequested)
            {
                var tokenSource = new CancellationTokenSource();
                var token = tokenSource.Token;
                var task = Task.Run(() => _displays[iterator].Start(token), token);

                Stopwatch sw = new Stopwatch();
                sw.Start();
                while(sw.ElapsedMilliseconds < _demoDisplayTime && !demoToken.IsCancellationRequested){}
                sw.Stop();
                tokenSource.Cancel();
                iterator = (iterator + 1) % _displays.Count;
            }
        }
    }
}
