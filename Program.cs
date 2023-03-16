Console.OutputEncoding = System.Text.Encoding.Default;
if (args.Length < 1)
{
    Console.WriteLine("Missing arguments. Try again :)");
    return;
}

double resistorValue = 0, Tolerance = 0;

GetColors(out string col1, out string col2, out string col3, out string col4, out string colTol);
if (args[0].Length < 16)
{
    Decode4ColorBands(col1, col2, col3, colTol, out resistorValue, out Tolerance);
}
else
{
    Decode5ColorBands(col1, col2, col3, col4, colTol, out resistorValue, out Tolerance);
}
Console.WriteLine($"The resistor value is {resistorValue:n}Ω and the Tolerance is ± {Tolerance}%");

void GetColors(out string col1, out string col2, out string col3, out string col4, out string colTol)
{
    string colorBands = args[0];
    col1 = colorBands.Substring(0, 3);
    col2 = colorBands.Substring(4, 3);
    col3 = colorBands.Substring(8, 3);
    col4 = colorBands.Substring(12, 3);
    colTol = colorBands.Substring(colorBands.Length - 3);
}

int ConvertColorToDigit(string color)
{
    return color switch
    {
        "Bla" => 0,
        "Bro" => 1,
        "Red" => 2,
        "Ora" => 3,
        "Yel" => 4,
        "Gre" => 5,
        "Blu" => 6,
        "Vio" => 7,
        "Gra" => 8,
        "Whi" => 9,
        _ => -1
    };
}

double GetMultiplierFromColor(string color)
{
    return color switch
    {
        "Bla" => 1,
        "Bro" => 10,
        "Red" => 100,
        "Ora" => 1_000,
        "Yel" => 10_000,
        "Gre" => 100_000,
        "Blu" => 1_000_000,
        "Vio" => 10_000_000,
        "Gra" => 100_000_000,
        "Whi" => 1_000_000_000,
        "Gol" => 0.1,
        "Sil" => 0.01,
        _ => -1
    };
}

double GetToleranceFromColor(string color)
{
    return color switch
    {
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
}

void Decode4ColorBands(string col1, string col2, string col3, string colTol, out double resistorValue, out double Tolerance)
{
    int valCol1 = ConvertColorToDigit(col1);
    int valCol2 = ConvertColorToDigit(col2);
    double valCol3 = GetMultiplierFromColor(col3);
    Tolerance = GetToleranceFromColor(colTol);

    double factor1 = double.Parse(valCol1.ToString() + valCol2.ToString());
    resistorValue = factor1 * valCol3;
}

void Decode5ColorBands(string col1, string col2, string col3, string col4, string colTol, out double resistorValue, out double Tolerance)
{
    int valCol1 = ConvertColorToDigit(col1);
    int valCol2 = ConvertColorToDigit(col2);
    int valCol3 = ConvertColorToDigit(col3);
    double valCol4 = GetMultiplierFromColor(col4);
    Tolerance = GetToleranceFromColor(colTol);

    double factor1 = double.Parse(valCol1.ToString() + valCol2.ToString() + valCol3.ToString());
    resistorValue = factor1 * valCol4;
}
