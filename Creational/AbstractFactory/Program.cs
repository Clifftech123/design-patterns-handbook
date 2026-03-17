// Customer orders a Modern collection
IFurnitureFactory factory = new ModernFurnitureFactory();
ISofa  sofa  = factory.CreateSofa();
IChair chair = factory.CreateChair();
sofa.Describe();
chair.Describe();

// Customer orders a Victorian collection
IFurnitureFactory factory2 = new VictorianFurnitureFactory();
ISofa  sofa2  = factory2.CreateSofa();
IChair chair2 = factory2.CreateChair();
sofa2.Describe();
chair2.Describe();


// The product interfaces
public interface ISofa  { void Describe(); }
public interface IChair { void Describe(); }

// Modern collection
public class ModernSofa : ISofa
{
    public void Describe() => Console.WriteLine("Sofa: Sleek modern design.");
}

public class ModernChair : IChair
{
    public void Describe() => Console.WriteLine("Chair: Minimalist modern style.");
}

// Victorian collection
public class VictorianSofa : ISofa
{
    public void Describe() => Console.WriteLine("Sofa: Ornate Victorian design.");
}

public class VictorianChair : IChair
{
    public void Describe() => Console.WriteLine("Chair: Classic Victorian style.");
}

// The abstract factory
public interface IFurnitureFactory
{
    ISofa  CreateSofa();
    IChair CreateChair();
}

// Concrete factories
public class ModernFurnitureFactory : IFurnitureFactory
{
    public ISofa  CreateSofa()  => new ModernSofa();
    public IChair CreateChair() => new ModernChair();
}

public class VictorianFurnitureFactory : IFurnitureFactory
{
    public ISofa  CreateSofa()  => new VictorianSofa();
    public IChair CreateChair() => new VictorianChair();
}
