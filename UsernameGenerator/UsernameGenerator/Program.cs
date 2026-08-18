
var firstname = ""; // Preferably be "string?"
while (string.IsNullOrEmpty(firstname))
{
    Console.Write("Enter your first name: ");
    firstname = Console.ReadLine();
}

string? lastname = "";
while (string.IsNullOrEmpty(lastname))
{
    Console.Write("Enter your last name: ");
    lastname = Console.ReadLine();
}

Random random  = new Random();
// var random  = new Random();
// Random random  = new();

string randomDigits = random.Next(0, 1000).ToString("D3");
string username = $"{firstname[0]}{lastname[..2]}{randomDigits}".ToLower();

Console.WriteLine($"Your username is: {username}");
