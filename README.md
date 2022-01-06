# Iot.Device.SenseHat.LedMatrixExt
Extension methods of SenseHatLedMatrix to show unicode letter, message, or series values on Raspberry Pi Sense HAT running on Linux.

## Getting started
- Create a new console app (e.g. `dotnet new console -o SenseHat.Sample`).
- Install `Iot.Device.SenseHat.LedMatrixExt` from NuGet (e.g. navigate inside the project directory and run `dotnet add package Iot.Device.SenseHat.LedMatrixExt`).

- Explore the sample app [SenseHat.QuickStart](./SenseHat.QuickStart/Program.cs) on how to build and run the app.

## Extension methods of SenseHatLedMatrix

- Clear LED Matrix
```csharp
ledMatrix.Clear(Color? backColor = null)
```
- Show a single letter (Unicode supported)
```csharp
ledMatrix.ShowLetter(char letter, Color? foreColor = null, Color? backColor = null, Rotation rotation = Rotation.Rotate0)
```
- Show a message (Unicode supported)

```csharp
ledMatrix.ShowMessage(string message, Color? foreColor = null, Color? backColor = null, Rotation textRotation = Rotation.Rotate0, Direction scrollDirection = Direction.Left, int speedInMs = 90)
```

- Show a series of values (Unicode supported)

```csharp
ledMatrix.ShowSeriesValues(float[] values, Color? foreColor = null, Color? backColor = null, bool fill = false, Rotation rotation = Rotation.Rotate0, bool forward = true, int speedInMs = 90)
```

## Unicode 8 x 8 Font

### Built-in unicode characters
- U+0000 - U+007F (basic latin)
- U+00A0 - U+00FF (extended latin)
- U+0390 - U+03C9 (greek characters)
- U+3040 - U+309F (Hiragana)
- U+2500 - U+257F (box drawing)
- U+2580 - U+259F (block elements)
- U+E541 - U+E55A (standard galactic alphabet)

### Add custom characters

See section [`Encoding`](#Encoding) for `glyph`.
```csharp
Font8x8.AddGlyph(char letter, byte[] glyph)
```

### Encoding
Every character in the font is encoded row-wise in 8 bytes.

The most significant bit of each byte corresponds to the first pixel in a row. 

The character 'A' (0x41 / 65) is encoded as 
{ 0x30, 0x78, 0xCC, 0xCC, 0xFC, 0xCC, 0xCC, 0x00 }

    0x30 => 0011 0000 => ..XX....
    0x78 => 0111 1000 => .XXXX...
    0xCC => 1100 1100 => XX..XX..
    0xCC => 1100 1100 => XX..XX..
    0xFC => 1111 1100 => XXXXXX..
    0xCC => 1100 1100 => XX..XX..
    0xCC => 1100 1100 => XX..XX..
    0x00 => 0000 0000 => ........

To access the nth pixel in a row, right-shift by 7-n.

                         . . X X . . . .
                         | | | | | | | |
    (0x30 >> 7) & 1 == 0-+ | | | | | | |
    (0x30 >> 6) & 1 == 0---+ | | | | | |
    (0x30 >> 5) & 1 == 1-----+ | | | | |
    (0x30 >> 4) & 1 == 1-------+ | | | |
    (0x30 >> 3) & 1 == 0---------+ | | |
    (0x30 >> 2) & 1 == 0-----------+ | |
    (0x30 >> 1) & 1 == 0-------------+ |
    (0x30 >> 0) & 1 == 0---------------+

## Acknowledgements

Font8x8: https://github.com/dhepper/font8x8