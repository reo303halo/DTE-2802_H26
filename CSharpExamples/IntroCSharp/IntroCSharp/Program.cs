// This is a single line comment

/*
* This is 
* multiple lines
* of comments
*/

using System.Globalization;

Console.WriteLine("Hello, World!");

int integer = 42; // Integer

bool boolean = true; // Boolean: true or false

char a = 'A'; 
// Represents a single Unicode character.
// Memory usage: Fixed (16 bits / 2 bytes).
// Used for individual characters, such as letters, digits, or symbols.

string greeting = "Hello, World!";
// Represents a sequence of characters.
// Memory usage: Variable (depends on the length of the string).
// Used for text, sentences, or any combination of characters.


Console.WriteLine($"a: {a}, greeting: {greeting}"); // String Interpolation: recommended for better readability
Console.WriteLine("a:" + a + ", greeting: " + greeting); // String Concatenation: historically better performance


float myFloat = 12.3123123123123123123f;
// Represents a 32-bit floating-point number.
// Used for scientific data or where precision is not critical.
// Approximate range: ±1.5e-45 to ±3.4e38.

double myDouble = 12.3123123123123123123;
// Represents a 64-bit floating-point number.
// Offers higher precision than float.
// Widely used for general-purpose floating-point calculations.

decimal myDecimal =  12.3123123123123123123m;
// Represents a 128-bit floating-point number.
// Ideal for financial calculations or exact precision.
// Range: ±1.0e-28 to ±7.9e28 with up to 28 decimal places.

Console.WriteLine($"myFloat: {myFloat}\nmyDouble: {myDouble}\nmyDecimal: {myDecimal}");

var decimalString = myDecimal.ToString(CultureInfo.InvariantCulture); // Use InvariantCulture for consistent formatting.
Console.WriteLine($"Length of myDecimal {decimalString[2..].Length}"); 
// Calculates the length of the substring of decimalString starting from the third character (index 2) to the end.


// Using var:
var aFloat = 12.3f; // float
var aDouble = 12.3; // double
var aDecimal = 12.3m; // decimal