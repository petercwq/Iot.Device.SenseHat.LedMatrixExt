using System.Drawing;

namespace Iot.Device.SenseHat.LedMatrixExt;

/// <summary>
/// Extension class to <see cref="SenseHatLedMatrix"/>.
/// </summary>
public static class LedMatrixExtension
{
    private const int NumberOfPixelsPerLine = SenseHatLedMatrix.NumberOfPixelsPerRow;
    private const int NumberOfPixelsPerFrame = SenseHatLedMatrix.NumberOfPixels;

    /// <summary>
    /// Show a message to led matrix.
    /// </summary>
    /// <param name="ledMatrix">Led matrix to display message.</param>
    /// <param name="message">Message to be displayed.</param>
    /// <param name="speedInMs">Message scrolling speed in milliseconds.</param>
    /// <param name="foreColor">Foreground color, or text color. White if not specified.</param>
    /// <param name="backColor">Background color. Black if not specified.</param>
    /// <param name="textRotation">Text rotaion in clockwise.</param>
    /// <param name="scrollDirection">Scrolling direction if message has more than one letter.</param>
    public static void ShowMessage(this SenseHatLedMatrix ledMatrix, string message, Color? foreColor = null, Color? backColor = null, Rotation textRotation = Rotation.Rotate0, Direction scrollDirection = Direction.Left, int speedInMs = 90)
    {
        if (string.IsNullOrWhiteSpace(message))
            return; // nothing to display

        var isReversed = scrollDirection == Direction.Right || scrollDirection == Direction.Down;
        var msg = isReversed ? message.Reverse() : message;
        var pixels = Font8x8.GetPixels(msg!, foreColor ?? Color.White, backColor ?? Color.Black, textRotation);
        var rowByRow = scrollDirection == Direction.Up || scrollDirection == Direction.Down;

        if (!rowByRow)
        {
            // colors are stored row by row in array from Font8x8.GetPixels()
            // transpose for scrolling col by col
            for (int i = 0; i < msg.Length; i++)
            {
                pixels.Transpose(NumberOfPixelsPerLine, i * NumberOfPixelsPerFrame);
            }
        }

        // shift pixelsPerStep pixels per frame to scroll
        var steps = (pixels.Length - NumberOfPixelsPerFrame) / NumberOfPixelsPerLine;
        var step = 0;
        do
        {
            var start = isReversed ? pixels.Length - NumberOfPixelsPerFrame - step * NumberOfPixelsPerLine : step * NumberOfPixelsPerLine;
            var frame = pixels.Skip(start).Take(NumberOfPixelsPerFrame).ToArray();
            if (!rowByRow)
            {
                // transpose back for displaying row by row
                frame.Transpose(NumberOfPixelsPerLine);
            }
            ledMatrix.Write(frame);
            if (++step > steps)
                break;
            Thread.Sleep(speedInMs);
        } while (true);
    }

    /// <summary>
    /// Show a letter to led matrix. 
    /// </summary>
    /// <param name="ledMatrix">Led matrix to display letter.</param>
    /// <param name="letter">Letter to be displayed.</param>
    /// <param name="foreColor">Foreground color, or text color. White if not specified.</param>
    /// <param name="backColor">Background color. Black if not specified.</param>
    /// <param name="rotation">Text rotaion in clockwise.</param>
    public static void ShowLetter(this SenseHatLedMatrix ledMatrix, char letter, Color? foreColor = null, Color? backColor = null, Rotation rotation = Rotation.Rotate0)
    {
        var frame = Font8x8.GetPixels(letter.ToString(), foreColor ?? Color.White, backColor ?? Color.Black, rotation);
        ledMatrix.Write(frame);
    }

    /// <summary>
    /// Clear led matrix by filling it with Black.
    /// </summary>
    /// <param name="ledMatrix">Led matrix to clear.</param>
    /// <param name="backColor">Background color.</param>
    public static void Clear(this SenseHatLedMatrix ledMatrix, Color? backColor = null)
    {
        ledMatrix.Fill(backColor ?? Color.Black);
    }

    /// <summary>
    /// Show a series of values to led matrix.
    /// </summary>
    /// <param name="ledMatrix">Led matrix to display values.</param>
    /// <param name="values">Values to be displayed.</param>
    /// <param name="foreColor">Foreground color, or text color. White if not specified.</param>
    /// <param name="backColor">Background color. Black if not specified.</param>
    /// <param name="rotation">Series rotation in clockwise.</param>
    /// <param name="forward">Indicates if series values moving forward.</param>
    /// <param name="speedInMs">Speed of scrolling.</param>
    /// <param name="fill">Indicates if fill area under line.</param>
    public static void ShowSeriesValues(this SenseHatLedMatrix ledMatrix, float[] values, Color? foreColor = null, Color? backColor = null, bool fill = false, Rotation rotation = Rotation.Rotate0, bool forward = true, int speedInMs = 90)
    {
        if (values == null || values.Length == 0)
            return; // nothing to display

        // normalize values
        var max = values.Max();
        var min = values.Min();
        var normal = NumberOfPixelsPerLine / (max - min);
        var normalised = values.Select(i => (byte)Math.Round(normal * (i - min))).ToArray();

        var fColor = foreColor ?? Color.White;
        var bColor = backColor ?? Color.Black;
        var frame = new Color[NumberOfPixelsPerFrame];
        frame.Populate(fColor);

        var steps = values.Length;
        var step = 0;
        var value = 0;

        do
        {
            var start = forward ? step : steps - NumberOfPixelsPerLine - step;
            for (int col = 0; col < NumberOfPixelsPerLine; col++)
            {
                var index = start + col;
                value = index > -1 && index < normalised.Length ? normalised[index] : 0;
                for (int row = 0; row < NumberOfPixelsPerLine; row++)
                {
                    var light = fill ? value >= NumberOfPixelsPerLine - row : value == NumberOfPixelsPerLine - row;
                    frame[NumberOfPixelsPerLine * row + col] = light ? fColor : bColor;
                }
            }

            frame.Rotate(NumberOfPixelsPerLine, 0, rotation);
            ledMatrix.Write(frame);
            if (++step > steps)
                break;
            Thread.Sleep(speedInMs);
        } while (true);
    }
}
