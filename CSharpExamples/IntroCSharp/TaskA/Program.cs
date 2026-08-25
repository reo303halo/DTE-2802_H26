// 1. Read the length from the user
Console.Write("Enter the length of the rectangle: ");
var lengthInput = Console.ReadLine();
var length = Convert.ToDouble(lengthInput);

// 2. Read the width from the user
Console.Write("Enter the width of the rectangle: ");
var widthInput = Console.ReadLine();
var width = Convert.ToDouble(widthInput);

// 3. Calculate area and perimeter
var area = length * width;
var perimeter = 2 * (length + width);

// Blank line for clean formatting
Console.WriteLine();

// 4. Print the results using placeholders
Console.WriteLine("With a length of {0} and a width of {1}:", length, width);
Console.WriteLine("Area: {0}", area);
Console.WriteLine("Perimeter: {0}", perimeter);

// Keeps the console window open until a key is pressed (optional)
Console.ReadLine();