using Xunit;

namespace Iot.Device.SenseHat.LedMatrixExt.Tests;

public class MiscExtensionTests
{
    [Theory]
    [InlineData("123456", "654321")]
    public void Reverse(string input, string expect)
    {
        Assert.Equal(expect, input.Reverse());
    }

    [Theory]
    [InlineData(255, 255)]
    [InlineData(0, 0)]
    [InlineData(0b10101010, 0b01010101)]
    public void ReverseBits(byte input, byte expect)
    {
        Assert.Equal(expect, input.ReverseBits());
    }

    [Theory]
    [InlineData(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 },
        new byte[] { 0, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15, 4, 8, 12, 16 }, 4, 1)]
    public void Transpose(byte[] input, byte[] expect, int edgeLen, int startIndex)
    {
        input.Transpose(edgeLen, startIndex);
        Assert.Equal(expect, input);
    }

    [Theory]
    [InlineData(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 },
        new byte[] { 0, 13, 14, 15, 16, 9, 10, 11, 12, 5, 6, 7, 8, 1, 2, 3, 4 }, 4, 1)]
    public void FlipUpDown(byte[] input, byte[] expect, int edgeLen, int startIndex)
    {
        input.FlipUpDown(edgeLen, startIndex);
        Assert.Equal(expect, input);
    }

    [Theory]
    [InlineData(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 },
new byte[] { 0, 4, 3, 2, 1, 8, 7, 6, 5, 12, 11, 10, 9, 16, 15, 14, 13 }, 4, 1)]
    public void FlipLeftRight(byte[] input, byte[] expect, int edgeLen, int startIndex)
    {
        input.FlipLeftRight(edgeLen, startIndex);
        Assert.Equal(expect, input);
    }

    [Theory]
    [InlineData(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 },
new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, 4, 1, Rotation.Rotate0)]
    [InlineData(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 },
new byte[] { 0, 13, 9, 5, 1, 14, 10, 6, 2, 15, 11, 7, 3, 16, 12, 8, 4 }, 4, 1, Rotation.Rotate90)]
    [InlineData(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 },
new byte[] { 0, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 }, 4, 1, Rotation.Rotate180)]
    [InlineData(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 },
new byte[] { 0, 4, 8, 12, 16, 3, 7, 11, 15, 2, 6, 10, 14, 1, 5, 9, 13 }, 4, 1, Rotation.Rotate270)]
    public void Rotate(byte[] input, byte[] expect, int edgeLen, int startIndex, Rotation rotation)
    {
        input.Rotate(edgeLen, startIndex, rotation);
        Assert.Equal(expect, input);
    }
}
