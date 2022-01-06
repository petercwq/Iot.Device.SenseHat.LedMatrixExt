using Iot.Device.Common;
using Iot.Device.SenseHat;
using Iot.Device.SenseHat.LedMatrixExt;
using System.Drawing;

using SenseHat sh = new();

var letters = "@9876543210~!";
var message = " »»»»0123456789»»»»ABCDEFG»»»»ΔΘΠΣΦΨΩαβζ»»»» ";
var series = Enumerable.Range(0, 100).Select(x => (float)Math.Sin(x / 30f)).ToArray();
int speedInMs = 20;

// LedMatrix displaying
foreach (var rotation in Enum.GetValues<Rotation>())
{
    Console.WriteLine("Showing series values - forward - fill - {0}", rotation);
    sh.LedMatrix.ShowSeriesValues(series, Color.Blue, Color.Black, true, rotation, true, speedInMs);

    sh.LedMatrix.Clear();

    Console.WriteLine("Showing series values - backward - fill - {0}", rotation);
    sh.LedMatrix.ShowSeriesValues(series, Color.Blue, Color.Black, true, rotation, false, speedInMs);

    sh.LedMatrix.Clear();

    Console.WriteLine("Showing series values - forward - not fill - {0}", rotation);
    sh.LedMatrix.ShowSeriesValues(series, Color.Blue, Color.Black, false, rotation, true, speedInMs);

    sh.LedMatrix.Clear();

    Console.WriteLine("Showing series values - backward - not fill - {0}", rotation);
    sh.LedMatrix.ShowSeriesValues(series, Color.Blue, Color.Black, false, rotation, false, speedInMs);

    sh.LedMatrix.Clear();

    Console.WriteLine("Showing letters - {0}", rotation);
    int index = 0;
    foreach (var letter in letters)
    {
        if (index % 2 == 0)
            sh.LedMatrix.ShowLetter(letter, Color.Blue, Color.Black, rotation);
        else
            sh.LedMatrix.ShowMessage(letter.ToString(), foreColor: Color.Blue, textRotation: rotation);
        Thread.Sleep(200);
    }

    sh.LedMatrix.Clear();

    Console.WriteLine("Scrolling message to left- {0}", rotation);

    sh.LedMatrix.ShowMessage(message, Color.Blue, Color.Black, rotation, Direction.Left, speedInMs);

    sh.LedMatrix.Clear();

    Console.WriteLine("Scrolling messageto right- {0}", rotation);

    sh.LedMatrix.ShowMessage(message, Color.Red, Color.Black, rotation, Direction.Right, speedInMs);

    sh.LedMatrix.Clear();

    Console.WriteLine("Scrolling message to up- {0}", rotation);

    sh.LedMatrix.ShowMessage(message, Color.Green, Color.Black, rotation, Direction.Up, speedInMs);

    sh.LedMatrix.Clear();

    Console.WriteLine("Scrolling message to down- {0}", rotation);

    sh.LedMatrix.ShowMessage(message, Color.Yellow, Color.Black, rotation, Direction.Down, speedInMs);

    sh.LedMatrix.Clear();
}


int n = 0;
int x = 3, y = 3;
// set this to the current sea level pressure in the area for correct altitude readings
var defaultSeaLevelPressure = WeatherHelper.MeanSeaLevel;

while (true)
{
    Console.Clear();

    (int dx, int dy, bool holding) = JoystickState(sh);

    if (holding)
    {
        n++;
    }

    x = (x + 8 + dx) % 8;
    y = (y + 8 + dy) % 8;

    sh.Fill(n % 2 == 0 ? Color.DarkBlue : Color.DarkRed);
    sh.SetPixel(x, y, Color.Yellow);

    var tempValue = sh.Temperature;
    var temp2Value = sh.Temperature2;
    var preValue = sh.Pressure;
    var humValue = sh.Humidity;
    var accValue = sh.Acceleration;
    var angValue = sh.AngularRate;
    var magValue = sh.MagneticInduction;
    var altValue = WeatherHelper.CalculateAltitude(preValue, defaultSeaLevelPressure, tempValue);

    Console.WriteLine($"Temperature Sensor 1: {tempValue.DegreesCelsius:0.#}\u00B0C");
    Console.WriteLine($"Temperature Sensor 2: {temp2Value.DegreesCelsius:0.#}\u00B0C");
    Console.WriteLine($"Pressure: {preValue.Hectopascals:0.##} hPa");
    Console.WriteLine($"Altitude: {altValue.Meters:0.##} m");
    Console.WriteLine($"Acceleration: {sh.Acceleration} g");
    Console.WriteLine($"Angular rate: {sh.AngularRate} DPS");
    Console.WriteLine($"Magnetic induction: {sh.MagneticInduction} gauss");
    Console.WriteLine($"Relative humidity: {humValue.Percent:0.#}%");
    Console.WriteLine($"Heat index: {WeatherHelper.CalculateHeatIndex(tempValue, humValue).DegreesCelsius:0.#}\u00B0C");
    Console.WriteLine($"Dew point: {WeatherHelper.CalculateDewPoint(tempValue, humValue).DegreesCelsius:0.#}\u00B0C");

    Thread.Sleep(1000);
}

(int, int, bool) JoystickState(SenseHat sh)
{
    sh.ReadJoystickState();

    int dx = 0;
    int dy = 0;

    if (sh.HoldingUp)
    {
        dy--; // y goes down
    }

    if (sh.HoldingDown)
    {
        dy++;
    }

    if (sh.HoldingLeft)
    {
        dx--;
    }

    if (sh.HoldingRight)
    {
        dx++;
    }

    return (dx, dy, sh.HoldingButton);
}
