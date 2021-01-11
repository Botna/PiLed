namespace PiLed.Devices.Config
{
    public class PixelConfig
    {
        /// <summary>
        /// Time in miliseconds between subsequent flushes
        /// </summary>
        public int FlushRate { get; set; } = 10;

        /// <summary>
        /// Number of LEDs in the strand.
        /// </summary>
        public int NumLeds { get; set; }
    }
}
