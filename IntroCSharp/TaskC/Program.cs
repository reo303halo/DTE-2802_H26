namespace TaskC;

internal class Program
{
    private static void Main()
    {
        Console.Write("Enter the temperature value: ");
        var tempInput = Console.ReadLine();
        var inputTemp = Convert.ToDouble(tempInput);

        // Get the target scale from the user
        Console.Write("Enter the current scale (C for Celsius, F for Fahrenheit): ");
        var scale = Console.ReadLine()?.ToUpper(); // Converts input to uppercase to handle 'c' or 'f'

        Console.WriteLine();
        
        switch (scale)
        {
            case "C":
            {
                var fahrenheit = Converter.ToFahrenheit(inputTemp);
                Console.WriteLine("{0}°C is equal to {1:F1}°F", inputTemp, fahrenheit);
                break;
            }
            case "F":
            {
                var celsius = Converter.ToCelsius(inputTemp);
                Console.WriteLine("{0}°F is equal to {1:F1}°C", inputTemp, celsius);
                break;
            }
            default:
                Console.WriteLine("Invalid scale entered. Please use 'C' or 'F'.");
                break;
        }
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
}