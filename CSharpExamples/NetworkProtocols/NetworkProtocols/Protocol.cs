namespace NetworkProtocols;

public class Protocol
{
    public virtual void Transmit()
    {
        Console.WriteLine("Transmitting data...");
    }
}

internal class Tcp : Protocol
{
    public override void Transmit()
    {
        Console.WriteLine("Reliable packet delivery.");
    }
}

internal class Udp : Protocol
{
    public override void Transmit()
    {
        Console.WriteLine("Sending datagram");
    }
}

internal class Http : Protocol
{
    public override void Transmit()
    {
        Console.WriteLine("GET /index.html");
    }
}

internal class Https : Protocol
{
    public override void Transmit()
    {
        Console.WriteLine("Encrypted request");
    }
}