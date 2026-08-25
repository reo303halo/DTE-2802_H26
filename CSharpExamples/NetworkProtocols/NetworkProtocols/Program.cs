namespace NetworkProtocols;

internal abstract class Program
{
    private static void Main()
    {
        // Array of the base type
        var protocols = new Protocol[]
        {
            new(), // new Protocol(),
            new Tcp(),
            new Udp(),
            new Http(),
            new Https()
        };
        
        // Polymorphism
        foreach (var protocol in protocols)
        {
            protocol.Transmit();
        }
    }
}