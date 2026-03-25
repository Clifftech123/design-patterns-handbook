// A plain espresso
ICoffee order = new Espresso();
Console.WriteLine($"{order.GetDescription()} => £{order.GetCost():F2}");

// Wrap it with milk
order = new Milk(order);
Console.WriteLine($"{order.GetDescription()} => £{order.GetCost():F2}");

// Wrap it with vanilla syrup on top
order = new VanillaSyrup(order);
Console.WriteLine($"{order.GetDescription()} => £{order.GetCost():F2}");

// Wrap it with whipped cream on top of that
order = new WhippedCream(order);
Console.WriteLine($"{order.GetDescription()} => £{order.GetCost():F2}");


// The component interface
public interface ICoffee
{
    string GetDescription();
    double GetCost();
}

// The base component
public class Espresso : ICoffee
{
    public string GetDescription() => "Espresso";
    public double GetCost()        => 1.00;
}

// The base decorator
public abstract class CoffeeDecorator : ICoffee
{
    protected readonly ICoffee _coffee;

    protected CoffeeDecorator(ICoffee coffee) { _coffee = coffee; }

    public virtual string GetDescription() => _coffee.GetDescription();
    public virtual double GetCost()        => _coffee.GetCost();
}

// Concrete decorators
public class Milk : CoffeeDecorator
{
    public Milk(ICoffee coffee) : base(coffee) { }

    public override string GetDescription() => _coffee.GetDescription() + ", Milk";
    public override double GetCost()        => _coffee.GetCost() + 0.30;
}

public class VanillaSyrup : CoffeeDecorator
{
    public VanillaSyrup(ICoffee coffee) : base(coffee) { }

    public override string GetDescription() => _coffee.GetDescription() + ", Vanilla Syrup";
    public override double GetCost()        => _coffee.GetCost() + 0.50;
}

public class WhippedCream : CoffeeDecorator
{
    public WhippedCream(ICoffee coffee) : base(coffee) { }

    public override string GetDescription() => _coffee.GetDescription() + ", Whipped Cream";
    public override double GetCost()        => _coffee.GetCost() + 0.75;
}
