// 1. Ask the user for weight in kilegrams
Console.Write("Enter your weight in kg (e.g., 75: ");
var weightInput = Console.ReadLine();
var weight = Convert.ToDouble(weightInput);

// 2. Ask the user for height in meters
Console.Write("Enter your height in cm (e.g., 1,75): ");
var heightInput = Console.ReadLine();
var height = Convert.ToDouble(heightInput);

var bmi = weight / (height * height);

Console.WriteLine();

Console.WriteLine("Your BMI is: {0:F1}", bmi);

// Interpret the BMI using a switch case
/*
switch (bmi)
{
    case < 18.5:
        Console.WriteLine("Interpretation: Underweight");
        break;
    
    case < 25:
        Console.WriteLine("Interpretation: Normal weight");
        break;
    
    case < 30:
        Console.WriteLine("Interpretation: Overweight");
        break;
    
    default:
        Console.WriteLine("Interpretation: Obesity");
        break;
}
*/

var interpretation = bmi switch
{
    < 18.5 => "Underweight",
    < 25   => "Normal weight",
    < 30   => "Overweight",
    _      => "Obesity"
};
Console.WriteLine($"Interpretation: {interpretation}");
