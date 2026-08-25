var random = new Random();
var continueLoop = "y";

while (continueLoop == "y")
{
    var number1 = random.Next(0, 11);
    var number2 = random.Next(0, 51);
    
    Console.Write($"What is {number1} + {number2}? ");
    var userAnswer = int.Parse(Console.ReadLine());
    
    Console.WriteLine(userAnswer == number1 + number2 ? "Correct!" : "Wrong!");
    
    Console.Write("Press Y to start again, or any other button to Quit: ");
    continueLoop = Console.ReadLine()?.ToLower();
}