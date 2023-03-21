Console.OutputEncoding = System.Text.Encoding.Default;
#region checking argument
if (args.Length != 1)
{
    Console.WriteLine("Wrong number of argument!. Try again :)");
    return;
}
if (!(args[0].Length == 15 || args[0].Length == 19))
{
    Console.WriteLine("Invalid length of arguments! Try again :)");
    return;
}

if (args[0].Length == 15)
{
    if (args[0][3] != '-' || args[0][7] != '-' || args[0][11] != '-')
    {
        Console.WriteLine("Invalid input format! Please use hyphen '-' to separate colors. Try again :)");
        return;
    }
}
else 
{
    if (args[0][3] != '-' || args[0][7] != '-' || args[0][11] != '-' || args[0][15] != '-')
    {
        Console.WriteLine("Missing hyphen! Try again :)");
        return;
    }
}
#endregion
const string INVALID_COLOR_CODE = "Invalid color code! Try again :)";

if (args[0].Length < 16)
{
    if (TryDecode4ColorBands(
        args[0].Substring(0, 3),
        args[0].Substring(4, 3),
        args[0].Substring(8, 3),
        args[0].Substring(12, 3),
        out double resistorValue,
        out double tolerance
    ))
    {
        Console.WriteLine($"The resistor value is {resistorValue:n}Ω and the Tolerance is ± {tolerance}%");
    }
    else
    {
        Console.WriteLine(INVALID_COLOR_CODE);
    }
}
else
{
    if(TryDecode5ColorBands(
        args[0].Substring(0, 3),
        args[0].Substring(4, 3),
        args[0].Substring(8, 3),
        args[0].Substring(12, 3),
        args[0].Substring(16, 3),
        out double resistorValue,
        out double tolerance
    ))
    {
         Console.WriteLine($"The resistor value is {resistorValue:n}Ω and the Tolerance is ± {tolerance}%");
    }
    else
    {
        Console.WriteLine(INVALID_COLOR_CODE);
    }
}

bool TryConvertColorToDigit(string color, out int colorDigit)
{
    colorDigit = color switch{
        "Bla" => 0,
        "Bro" => 1,
        "Red" => 2,
        "Ora" => 3,
        "Yel" => 4,
        "Gre" => 5,
        "Blu" => 6,
        "Vio" => 7,
        "Gra" => 5,
        "Whi" => 9,
        _ => -1
    };
    if (colorDigit == -1)
    {
        return false;
    }
    return true;
}

bool TryGetMultiplierFromColor(string color, out double multiplier)
{
    switch (color)
    {
        case "Gol": multiplier = 0.1; break;
        case "Sil": multiplier = 0.01; break;
        default: 
        int digit;
        if (!TryConvertColorToDigit(color, out digit))
        {
            multiplier = -1;
            return false;
        }
        multiplier = Math.Pow(10, digit);
        break;
    }
    return true;
}

bool TryGetToleranceFromColor(string color, out double tolerance)
{
    tolerance = color switch{
        "Bro" => 1,
        "Red" => 2,
        "Gre" => 0.5,
        "Blu" => 0.25,
        "Vio" => 0.1,
        "Gra" => 0.05,
        "Gol" => 5,
        "Sil" => 10,
        _ => -1
    };
    
    if (tolerance == -1)
    {
        return false;
    }
    return true;
}

bool TryDecode4ColorBands(string col1, string col2, string col3, string colTol,
    out double resistorValue, out double tolerance)
{
    tolerance = -1; resistorValue = -1;

    if (!TryConvertColorToDigit(col1, out int digit1)) { return false; }
    if (!TryConvertColorToDigit(col2, out int digit2)) { return false; }
    if (!TryGetMultiplierFromColor(col3, out double multiplier)) { return false; }
    if (!TryGetToleranceFromColor(colTol, out tolerance)) { return false; }

    resistorValue = (digit1 * 10 + digit2) * multiplier;
    return true;
}

bool TryDecode5ColorBands(string col1, string col2, string col3, string col4, string colTol, 
    out double resistorValue, out double tolerance)
{
    tolerance = -1; resistorValue = -1;

    if (!TryConvertColorToDigit(col1, out int digit1)) { return false; }
    if (!TryConvertColorToDigit(col2, out int digit2)) { return false; }
    if (!TryConvertColorToDigit(col3, out int digit3)) { return false; }
    if (!TryGetMultiplierFromColor(col4, out double multiplier)) { return false; }
    if (!TryGetToleranceFromColor(colTol, out tolerance)) { return false; }

    resistorValue = (digit1 * 100 + digit2 * 10 + digit3) * multiplier;
    return true;

}
