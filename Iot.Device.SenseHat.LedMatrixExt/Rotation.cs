namespace Iot.Device.SenseHat.LedMatrixExt;

/// <summary>
/// Specifies the rotation to apply to an object, such as text, image.
/// </summary>
public enum Rotation : byte
{
    /// <summary>
    /// Not rotated. This is the default value.
    /// </summary>
    Rotate0,

    /// <summary>
    /// Rotate clockwise by 90 degrees.
    /// </summary>
    Rotate90,

    /// <summary>
    /// Rotate clockwise by 180 degrees.
    /// </summary>
    Rotate180,

    /// <summary>
    /// Rotate clockwise by 270 degrees.
    /// </summary>
    Rotate270
}
