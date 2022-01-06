using Xunit;

namespace Iot.Device.SenseHat.LedMatrixExt.Tests;

public class Font8x8Tests
{
    [Theory]
    [InlineData('}', new byte[] { 0xE0, 0x30, 0x30, 0x1C, 0x30, 0x30, 0xE0, 0x00 })]
    [InlineData('?', new byte[] { 0x78, 0xCC, 0x0C, 0x18, 0x30, 0x00, 0x30, 0x00 })]
    [InlineData('众', new byte[] { 0x78, 0xCC, 0x0C, 0x18, 0x30, 0x00, 0x30, 0x00 })]
    public void GetGlyph(char input, byte[] expect)
    {
        Assert.Equal(expect, Font8x8.GetGlyph(input));
    }


    //[Fact]
    //public void UpdateCode()
    //{
    //    var text = File.ReadAllText(@"C:\Users\pchen\Downloads\temp_font_code.txt");
    //    var new_text = Regex.Replace(text, "0x([0-9A-F]{2})", m => "0x" + byte.Parse(m.Groups[1].Value, System.Globalization.NumberStyles.HexNumber).ReverseBits().ToString("X2"));
    //    File.WriteAllText(@"C:\Users\pchen\Downloads\temp_font_code_new.txt", new_text);
    //}

    [Theory]
    [InlineData('}', new byte[] { 0xE0, 0x30, 0x30, 0x1C, 0x30, 0x30, 0xE0, 0x00 })]
    [InlineData('?', new byte[] { 0x78, 0xCC, 0x0C, 0x18, 0x30, 0x00, 0x30, 0x00 })]
    [InlineData('中', new byte[] { 0xFF, 0xCC, 0x0C, 0x18, 0xFF, 0x00, 0x30, 0x00 })]
    public void AddGlyph(char input, byte[] expect)
    {
        Font8x8.AddGlyph(input, expect);
        Assert.Equal(expect, Font8x8.GetGlyph(input));
    }

    [Theory]
    [InlineData("}", Rotation.Rotate0, new byte[] {
        1, 1, 1, 0, 0, 0, 0, 0,
        0, 0, 1, 1, 0, 0, 0, 0,
        0, 0, 1, 1, 0, 0, 0, 0,
        0, 0, 0, 1, 1, 1, 0, 0,
        0, 0, 1, 1, 0, 0, 0, 0,
        0, 0, 1, 1, 0, 0, 0, 0,
        1, 1, 1, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0 })]
    [InlineData("}", Rotation.Rotate90, new byte[] {
        0, 1, 0, 0, 0, 0, 0, 1,
        0, 1, 0, 0, 0, 0, 0, 1,
        0, 1, 1, 1, 0, 1, 1, 1,
        0, 0, 1, 1, 1, 1, 1, 0,
        0, 0, 0, 0, 1, 0, 0, 0,
        0, 0, 0, 0, 1, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0 })]
    [InlineData("}", Rotation.Rotate180, new byte[] {
        0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 1, 1, 1,
        0, 0, 0, 0, 1, 1, 0, 0,
        0, 0, 0, 0, 1, 1, 0, 0,
        0, 0, 1, 1, 1, 0, 0, 0,
        0, 0, 0, 0, 1, 1, 0, 0,
        0, 0, 0, 0, 1, 1, 0, 0,
        0, 0, 0, 0, 0, 1, 1, 1 })]
    [InlineData("}", Rotation.Rotate270, new byte[] {
        0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 1, 0, 0, 0, 0,
        0, 0, 0, 1, 0, 0, 0, 0,
        0, 1, 1, 1, 1, 1, 0, 0,
        1, 1, 1, 0, 1, 1, 1, 0,
        1, 0, 0, 0, 0, 0, 1, 0,
        1, 0, 0, 0, 0, 0, 1, 0 })]
    public void GetPixels(string message, Rotation rotation, byte[] expect)
    {
        Assert.Equal(expect, Font8x8.GetPixels(message, (byte)1, (byte)0, rotation));
    }
}
