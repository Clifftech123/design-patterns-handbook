// Create one configured circle
Circle original = new Circle { Colour = "Red", Size = 50 };

// Clone it instead of building from scratch
Shape clone1 = original.Clone();
Shape clone2 = original.Clone();

// Modify the clones independently
clone2.Colour = "Blue";

original.Describe();
clone1.Describe();
clone2.Describe();


// The prototype base - every shape must be able to clone itself
public abstract class Shape
{
    public string Colour { get; set; }
    public int    Size   { get; set; }

    public abstract Shape Clone();
    public abstract void  Describe();
}

// Concrete shapes
public class Circle : Shape
{
    public override Shape Clone()    => (Shape)this.MemberwiseClone();
    public override void  Describe() => Console.WriteLine($"Circle    | Colour: {Colour} | Size: {Size}");
}

public class Rectangle : Shape
{
    public override Shape Clone()    => (Shape)this.MemberwiseClone();
    public override void  Describe() => Console.WriteLine($"Rectangle | Colour: {Colour} | Size: {Size}");
}
