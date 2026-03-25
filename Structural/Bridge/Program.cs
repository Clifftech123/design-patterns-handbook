// Basic remote paired with a Sony TV
Console.WriteLine("--- Basic Remote + Sony TV ---");
RemoteControl basicSony = new BasicRemote(new SonyTV());
basicSony.TurnOn();
basicSony.SetChannel(5);
basicSony.TurnOff();

// Smart remote paired with a Samsung TV
Console.WriteLine("\n--- Smart Remote + Samsung TV ---");
SmartRemote smartSamsung = new SmartRemote(new SamsungTV());
smartSamsung.TurnOn();
smartSamsung.SetChannel(10);
smartSamsung.SetVolume(20);
smartSamsung.TurnOff();

// Swap freely, smart remote now with Sony, no code changes needed
Console.WriteLine("\n--- Smart Remote + Sony TV ---");
SmartRemote smartSony = new SmartRemote(new SonyTV());
smartSony.TurnOn();
smartSony.SetChannel(3);
smartSony.TurnOff();


// The implementation interface, what any TV must be able to do
public interface ITV
{
    void TurnOn();
    void TurnOff();
    void SetChannel(int channel);
    void SetVolume(int volume);
}

// Concrete implementations, each brand handles things its own way
public class SonyTV : ITV
{
    public void TurnOn()           => Console.WriteLine("Sony TV: Powering on. BRAVIA display ready.");
    public void TurnOff()          => Console.WriteLine("Sony TV: Shutting down.");
    public void SetChannel(int ch) => Console.WriteLine($"Sony TV: Switching to channel {ch}.");
    public void SetVolume(int vol) => Console.WriteLine($"Sony TV: Volume set to {vol}.");
}

public class SamsungTV : ITV
{
    public void TurnOn()           => Console.WriteLine("Samsung TV: Turning on. Smart Hub loading.");
    public void TurnOff()          => Console.WriteLine("Samsung TV: Powering off.");
    public void SetChannel(int ch) => Console.WriteLine($"Samsung TV: Channel {ch} selected.");
    public void SetVolume(int vol) => Console.WriteLine($"Samsung TV: Volume at {vol}.");
}

// The abstraction, the remote holds a reference to whichever TV it controls
public abstract class RemoteControl
{
    protected ITV _tv;

    protected RemoteControl(ITV tv) { _tv = tv; }

    public abstract void TurnOn();
    public abstract void TurnOff();
    public abstract void SetChannel(int channel);
}

// Refined abstraction, a basic remote that does exactly what the TV does
public class BasicRemote : RemoteControl
{
    public BasicRemote(ITV tv) : base(tv) { }

    public override void TurnOn()           => _tv.TurnOn();
    public override void TurnOff()          => _tv.TurnOff();
    public override void SetChannel(int ch) => _tv.SetChannel(ch);
}

// Refined abstraction, a smart remote that adds its own behaviour on top
public class SmartRemote : RemoteControl
{
    public SmartRemote(ITV tv) : base(tv) { }

    public override void TurnOn()
    {
        Console.WriteLine("Smart Remote: Activating voice control.");
        _tv.TurnOn();
    }

    public override void TurnOff()
    {
        Console.WriteLine("Smart Remote: Saving watch history.");
        _tv.TurnOff();
    }

    public override void SetChannel(int ch)
    {
        Console.WriteLine("Smart Remote: Looking up channel guide.");
        _tv.SetChannel(ch);
    }

    public void SetVolume(int vol) => _tv.SetVolume(vol);
}
