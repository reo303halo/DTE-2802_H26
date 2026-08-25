namespace TaskC;

/*
Notice that I didn't create separate methods for every possible conversion, such as KelvinToFahrenheit().
This keeps the Converter class relatively simple. With three scales, you only need the basic conversion methods:
    Celsius -> Fahrenheit
    Fahrenheit -> Celsius
    Celsius -> Kelvin
    Kelvin -> Celsius

Although we kept the Converter class simple, the Main method in the Program class is a bit more complex. 
It handles user input and output, as well as the logic for determining which conversion to perform based on the user's input.

It is up to you to decide how to structure your code. You could create separate methods for each conversion, 
or you could keep it simple like this. 
The important thing is that the code is clear and easy to understand.
*/
internal class Program
{
    private static void Main()
    {
        Console.Write("Enter the temperature value: ");
        var tempInput = Console.ReadLine();
        var inputTemp = Convert.ToDouble(tempInput);

        Console.Write("Enter the current scale (C for Celsius, F for Fahrenheit, K for Kelvin): ");
        var fromScale = Console.ReadLine()?.ToUpper();

        Console.Write("Enter the target scale (C for Celsius, F for Fahrenheit, K for Kelvin): ");
        var toScale = Console.ReadLine()?.ToUpper();

        Console.WriteLine();

        double result;

        switch (fromScale)
        {
            case "C":
                switch (toScale)
                {
                    case "F":
                        result = Converter.ToFahrenheit(inputTemp);
                        break;

                    case "K":
                        result = Converter.ToKelvin(inputTemp);
                        break;

                    case "C":
                        result = inputTemp;
                        break;

                    default:
                        Console.WriteLine("Invalid target scale.");
                        return;
                }
                break;

            case "F":
                switch (toScale)
                {
                    case "C":
                        result = Converter.ToCelsius(inputTemp);
                        break;

                    case "K":
                        result = Converter.ToKelvin(Converter.ToCelsius(inputTemp));
                        break;

                    case "F":
                        result = inputTemp;
                        break;

                    default:
                        Console.WriteLine("Invalid target scale.");
                        return;
                }
                break;

            case "K":
                switch (toScale)
                {
                    case "C":
                        result = Converter.ToCelsiusFromKelvin(inputTemp);
                        break;

                    case "F":
                        result = Converter.ToFahrenheit(
                            Converter.ToCelsiusFromKelvin(inputTemp));
                        break;

                    case "K":
                        result = inputTemp;
                        break;

                    default:
                        Console.WriteLine("Invalid target scale.");
                        return;
                }
                break;

            default:
                Console.WriteLine("Invalid current scale.");
                return;
        }

        Console.WriteLine(
            $"{inputTemp:F1}°{fromScale} is equal to {result:F1}°{toScale}");
    }
}

// Separate class containing static methods for conversion
public static class Converter
{
    public static double ToCelsius(double fahrenheit)
    {
        return (fahrenheit - 32) * 5.0 / 9.0;
    }

    public static double ToFahrenheit(double celsius)
    {
        return (celsius * 9.0 / 5.0) + 32;
    }

    public static double ToKelvin(double celsius)
    {
        return celsius + 273.15;
    }

    public static double ToCelsiusFromKelvin(double kelvin)
    {
        return kelvin - 273.15;
    }
}