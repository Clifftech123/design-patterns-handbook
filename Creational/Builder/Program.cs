Tailor tailor = new Tailor();

Suit businessSuit = tailor.MakeSuit(new BusinessSuitBuilder());
businessSuit.Describe();

Suit weddingSuit = tailor.MakeSuit(new WeddingSuitBuilder());
weddingSuit.Describe();


// The product
public class Suit
{
    public string Fabric  { get; set; }
    public string Lining  { get; set; }
    public string Buttons { get; set; }

    public void Describe()
    {
        Console.WriteLine($"Suit: {Fabric} fabric, {Lining} lining, {Buttons} buttons.");
    }
}

// The builder - defines the steps
public interface ISuitBuilder
{
    void SetFabric();
    void SetLining();
    void SetButtons();
    Suit GetSuit();
}

// Business suit builder
public class BusinessSuitBuilder : ISuitBuilder
{
    private Suit _suit = new Suit();

    public void SetFabric()  => _suit.Fabric  = "Dark wool";
    public void SetLining()  => _suit.Lining  = "Silk";
    public void SetButtons() => _suit.Buttons = "Black horn";
    public Suit GetSuit()    => _suit;
}

// Wedding suit builder
public class WeddingSuitBuilder : ISuitBuilder
{
    private Suit _suit = new Suit();

    public void SetFabric()  => _suit.Fabric  = "Ivory linen";
    public void SetLining()  => _suit.Lining  = "Satin";
    public void SetButtons() => _suit.Buttons = "Pearl";
    public Suit GetSuit()    => _suit;
}

// The tailor - the director who runs the process
public class Tailor
{
    public Suit MakeSuit(ISuitBuilder builder)
    {
        builder.SetFabric();
        builder.SetLining();
        builder.SetButtons();
        return builder.GetSuit();
    }
}
