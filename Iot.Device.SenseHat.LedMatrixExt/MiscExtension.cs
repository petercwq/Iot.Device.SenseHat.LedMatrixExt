namespace Iot.Device.SenseHat.LedMatrixExt;

/// <summary>
/// Misc extensions.
/// </summary>
public static class MiscExtension
{
    /// <summary>
    /// Reverse a <see cref="string"/>.
    /// </summary>
    /// <param name="value">String to be reversed.</param>
    /// <returns>Reversed string.</returns>
    public static string Reverse(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        char[] charArray = value.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    /// <summary>
    /// Reverse bits of a <see cref="byte"/>.
    /// </summary>
    /// <param name="value">Byte to be reversed.</param>
    /// <returns>Reversed byte.</returns>
    public static byte ReverseBits(this byte value)
    {
        return (byte)((value * 0x80200802ul & 0x0884422110ul) * 0x0101010101ul >> 32);
    }

    /// <summary>
    /// Populate arrary with the same value.
    /// </summary>
    /// <typeparam name="T">Type of value.</typeparam>
    /// <param name="array">Array to be populated.</param>
    /// <param name="value">Value to populate.</param>
    public static void Populate<T>(this T[] array, T value)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = value;
        }
    }

    /// <summary>
    /// Transpose a n x n matrix represented by a one-dimension array.
    /// </summary>
    /// <typeparam name="T">Type of array elements.</typeparam>
    /// <param name="array">The array which carries the matrix to be transposed.</param>
    /// <param name="n">Width and height of the matrix.</param>
    /// <param name="startIndex">The start index of the matrix in the array.</param>
    public static void Transpose<T>(this T[] array, int n, int startIndex = 0)
    {
        int i, j;
        for (i = 1; i < n; i++)
        {
            for (j = 0; j < i; j++)
            {
                Swap(ref array[startIndex + j * n + i], ref array[startIndex + i * n + j]);
            }
        }
    }

    /// <summary>
    /// Flip up-down a n x n matrix represented by a one-dimension array.
    /// </summary>
    /// <typeparam name="T">Type of array elements.</typeparam>
    /// <param name="array">The array which carries the matrix to be flipped.</param>
    /// <param name="n">Width and height of the matrix.</param>
    /// <param name="startIndex">The start index of the matrix in the array.</param>
    public static void FlipUpDown<T>(this T[] array, int n, int startIndex = 0)
    {
        int i, j;
        for (i = 0; i < n; i++)
        {
            for (j = 0; j < n / 2; j++)
            {
                Swap(ref array[startIndex + j * n + i], ref array[startIndex + (n - 1 - j) * n + i]);
            }
        }
    }

    /// <summary>
    /// Flip left-right a n x n matrix represented by a one-dimension array.
    /// </summary>
    /// <typeparam name="T">Type of array elements.</typeparam>
    /// <param name="array">The array which carries the matrix to be flipped.</param>
    /// <param name="n">Width and height of the matrix.</param>
    /// <param name="startIndex">The start index of the matrix in the array.</param>
    public static void FlipLeftRight<T>(this T[] array, int n, int startIndex = 0)
    {
        int i, j;
        for (i = 0; i < n / 2; i++)
        {
            for (j = 0; j < n; j++)
            {
                Swap(ref array[startIndex + j * n + i], ref array[startIndex + j * n + (n - 1 - i)]);
            }
        }
    }

    /// <summary>
    /// Rotate a n x n matrix represented by a one-dimension array.
    /// </summary>
    /// <typeparam name="T">Type of array elements.</typeparam>
    /// <param name="array">The array which carries the matrix to be flipped.</param>
    /// <param name="n">Width and height of the matrix.</param>
    /// <param name="startIndex">The start index of the matrix in the array.</param>
    /// <param name="rotation">Rotation in clockwise.</param>
    public static void Rotate<T>(this T[] array, int n, int startIndex = 0, Rotation rotation = Rotation.Rotate0)
    {
        switch (rotation)
        {
            case Rotation.Rotate0:
                break;
            case Rotation.Rotate90:
                Rotate90(array, n, startIndex);
                break;
            case Rotation.Rotate180:
                Rotate180(array, n, startIndex);
                break;
            case Rotation.Rotate270:
                Rotate270(array, n, startIndex);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Swap two values.
    /// </summary>
    private static void Swap<T>(ref T x, ref T y)
    {
        T t = y;
        y = x;
        x = t;
    }

    /// <summary>
    /// Rotate matrix 90 degrees in clockwise.
    /// </summary>
    private static void Rotate90<T>(T[] array, int edgeLen, int startIndex = 0)
    {
        //REVERSE every col 
        array.FlipUpDown(edgeLen, startIndex);
        // Performing Transpose
        array.Transpose(edgeLen, startIndex);
    }

    /// <summary>
    /// Rotate matrix 180 degrees in clockwise.
    /// </summary>
    private static void Rotate180<T>(T[] array, int edgeLen, int startIndex = 0)
    {
        Array.Reverse(array, startIndex, edgeLen * edgeLen);
    }

    /// <summary>
    /// Rotate matrix 270 degrees in clockwise.
    /// </summary>
    private static void Rotate270<T>(T[] array, int edgeLen, int startIndex = 0)
    {
        //REVERSE every row 
        array.FlipLeftRight(edgeLen, startIndex);
        // Performing Transpose
        array.Transpose(edgeLen, startIndex);
    }
}
