# PiLed

[![Build Status](https://dev.azure.com/drewzisa/drewzisa/_apis/build/status/PiLed/Botna.PiLed%20Publish?branchName=main)](https://dev.azure.com/drewzisa/drewzisa/_build/latest?definitionId=1&branchName=main)

Dotnetcore library for operating Pixel based LED's on a raspberry pi, or other linux based devices, via the SPI

### Installing PiLed

To install, simply add the nuget library via `dotnet add package` or Visual studio
- `dotnet add package PiLed`

### Using PiLed

This library allows the user to create a variety of different Led based devices, and then pass them into various included DisplayModes.
Presently, the only LED strand that is supported as WS2801 based Leds (you can purchase these from adafruit [here](https://www.adafruit.com/product/322 "here") )

A wiring example for the WS2801 LED Strand can be found here - https://learn.adafruit.com/light-painting-with-raspberry-pi/hardware

#### Create a Device
The device will have several pieces of default configuration, that are changeable based on your hardware.  They can be changed via the `PixelConfig`
- `var ws2801Device = new WS2801PixelDevice(new PixelConfig(){ NumLeds = 25});`

#### Create a Display mode
All display modes that take a color do so in [HSV values](https://www.tydac.ch/color/ "HSV values")
- `var display = new SolidColorDisplay(device: ws2801Device, color: new PixelColor(h:0, s: 1, v:1); // This will display all LED's as Red`

#### Start the Display
- `display.Start();`

### Creating custom Displays
With access to the underlying device as shown above, you can create your own set of `PixelBuffers` and flush color(s) to the attached Led's. This
allows for more direct control over the LED's as opposed to relying on the pre-built display modes.
```csharp
            var numLeds = 3;
            var ws2801Device = new WS2801PixelDevice(new PiLed.Devices.Config.PixelConfig() { NumLeds = numLeds });

            var greenBuffer = new PixelBuffer();
            greenBuffer.PixelIndices = new int[2] { 0, 2 }; //Represents this buffers color will apply to the LEDS [0] and [2], or the 1st and 3rd led
            greenBuffer.Color = new PixelColor(120, 0, 0);

            var blueBuffer = new PixelBuffer();
            blueBuffer.PixelIndices = new int[1] { 1 }; //Rerepents this buffers color will apply to LED [1] of the strand. or the 2nd led.
            blueBuffer.Color = new PixelColor(240, 0, 0);

            ws2801Device.FlushColorToLeds(greenBuffer, blueBuffer);
```
