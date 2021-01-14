namespace PiLed.Models
{
    public class PixelBuffer
    {
        /// <summary>
        /// Indices of the pixel strand that this particular coloration value will apply to
        /// </summary>
        public int[] PixelIndices { get; set; }

        /// <summary>
        /// HSV values of the color to apply
        /// </summary>
        public PixelColor Color { get; set; }
    }
}
