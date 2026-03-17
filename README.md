<p align="center">
  <strong>Design Patterns Handbook</strong>
</p>

<p align="center">
  A practical guide to understanding software design patterns, for every developer, regardless of language. Examples are written in C#, but every concept here applies equally to Python, Java, TypeScript, Go, and beyond.
</p>

---

Introduction
============

Design patterns are **reusable solutions to common problems in software design**. Think of them as blueprints: not finished code, but proven templates you can adapt to solve a specific problem in your own codebase.

> *"In software engineering, a software design pattern is a general, reusable solution to a commonly occurring problem within a given context in software design. It is not a finished design that can be transformed directly into source or machine code. It is a description or template for how to solve a problem that can be used in many different situations."*
>
> **Source:** [Wikipedia - Software design pattern](https://en.wikipedia.org/wiki/Software_design_pattern)

### Things to keep in mind

- **Design patterns are not code.** They are a way of *thinking* about how to structure your code. They are a tool, not a silver bullet, for solving specific design problems.

- **The concepts are universal.** The examples here are written in C#, but the same patterns exist in every language. If you write Python, Java, Go, or TypeScript, you are already using some of these without knowing it.

- **There is no one-size-fits-all pattern.** Each pattern exists to address a particular kind of problem. Understanding *what problem a pattern solves* is more important than memorising the implementation.

> C# is used here as the teaching language. It is clear, readable, and widely understood. The goal of this handbook is for you to walk away understanding the pattern itself, not just the C# code.

---

Types of Design Patterns
-------------------------

* [Creational Design Patterns](#creational-design-patterns)
* [Structural Design Patterns](#structural-design-patterns)
* [Behavioral Design Patterns](#behavioral-design-patterns)

---

Creational Design Patterns
===========================

Simply put:

> Creational patterns are all about **how objects are created**. They can be divided into class-creation patterns, which use inheritance to decide which class to instantiate, and object-creation patterns, which use delegation to get the job done.

Wikipedia describes them as:

> *"In software engineering, creational patterns are design patterns that deal with object creation mechanisms, trying to create objects in a manner suitable to the situation. A creational pattern aims to separate a system from how its objects are created, composed, and represented. They increase the system's flexibility in terms of the what, who, how, and when of object creation."*
>
> **Source:** [Wikipedia - Creational pattern](https://en.wikipedia.org/wiki/Creational_pattern)

- They keep the details of object creation **hidden from the client code**, making the system easier to manage and maintain.
- They abstract away how objects are created, composed, and represented, so the rest of your code does not need to care.

There are **5 creational design patterns**:

1. **Singleton**: Ensures a class has only one instance and provides a global point of access to it.
2. **Factory Method**: Defines an interface for creating an object, but lets subclasses decide which class to instantiate.
3. **Abstract Factory**: Provides an interface for creating families of related or dependent objects without specifying their concrete classes.
4. **Builder**: Separates the construction of a complex object from its representation, so the same construction process can produce different results.
5. **Prototype**: Creates new objects by cloning an existing instance rather than building one from scratch.

* [Singleton](#singleton)
* [Factory Method](#factory-method)
* [Abstract Factory](#abstract-factory)
* [Builder](#builder)
* [Prototype](#prototype)


Singleton
-------------------

### Real World Example

> Think of the **conductor of an orchestra**. An orchestra has one conductor. Every musician on stage looks to that same conductor for direction: when to start, when to stop, how fast to play, how loud to go. The conductor is the single point of authority that all musicians connect to and take decisions from. You cannot have two conductors standing at the front giving different instructions. That would cause chaos. No matter which musician needs guidance, they all reach the same one person.

That is exactly how the Singleton works in code. One instance, shared by everyone who needs it, making decisions from one place.

### Problems it solves

- **What if two musicians get different conductors giving different instructions?** The performance falls apart. There must be one conductor that every musician looks to, without exception.
- **How does a musician find the conductor?** They do not go searching. There is one well-known place everyone looks, and the same conductor is always there.
- **What stops someone from appointing a second conductor?** The orchestra itself controls this. Once a conductor is on the podium, no second one can take it.

### In Simple Terms

> There is only one instance of the class, and every part of the system that needs it gets access to that exact same instance. Never a new one.

### Wikipedia describes it as:

> *"In object-oriented programming, the singleton pattern is a software design pattern that restricts the instantiation of a class to a singular instance. The pattern is useful when exactly one object is needed to coordinate actions across a system."*
>
> **Source:** [Wikipedia - Singleton pattern](https://en.wikipedia.org/wiki/Singleton_pattern)

### Programming Example

We model the analogy directly. The `OrchestraConductor` is the Singleton: one instance, shared by all musicians, making all decisions.

```csharp
public class OrchestraConductor
{
    // Step 1: Hold the one instance here
    private static OrchestraConductor _instance;

    // Step 2: Private constructor - nobody outside can do: new OrchestraConductor()
    private OrchestraConductor() { }

    // Step 3: The only way to get the conductor
    public static OrchestraConductor GetInstance()
    {
        if (_instance == null)
        {
            _instance = new OrchestraConductor();
        }

        return _instance;
    }

    // Decisions the conductor makes
    public void Start()                    => Console.WriteLine("Conductor: Begin playing.");
    public void Stop()                     => Console.WriteLine("Conductor: Stop playing.");
    public void SetTempo(string tempo)     => Console.WriteLine($"Conductor: Tempo is now {tempo}.");
}
```

Now let's see it in action:

```csharp
// Violinist asks for the conductor
OrchestraConductor violinist = OrchestraConductor.GetInstance();

// Pianist asks for the conductor
OrchestraConductor pianist = OrchestraConductor.GetInstance();

// Are they talking to the same conductor?
Console.WriteLine(object.ReferenceEquals(violinist, pianist)); // True

violinist.SetTempo("Allegro");
pianist.Start();
```

**Output:**
```
True
Conductor: Tempo is now Allegro.
Conductor: Begin playing.
```

> Both musicians got the **same conductor**. The constructor never ran twice. That is the Singleton.

### When to use it

- When you need **one shared resource** that the whole application talks to, such as a logger, a configuration manager, or a database connection pool
- When having more than one instance would cause **incorrect behaviour** or conflicting state
- When you want a **global point of access** to an object without passing it around everywhere


---

Factory Method
-------------------

### Real World Example

 > Think of a **recruitment agency**. A company calls the agency and says "we need a worker." The company does not go out and create the worker themselves. They just make the request. The agency decides which specific person to send: a developer, a designer, or a tester, depending on what the company needs. The company does not know or care exactly who is coming. They just know the person will be able to do the job.

That is the Factory Method. Your code asks for an object. The factory decides which specific type to create and hands it back. You work with it without needing to know exactly what it is under the hood.

### Problems it solves

- **The company should not need to know who they are getting.** They just need someone who can do the job. The agency handles the decision of who to send. The company never has to worry about the details.
- **What if the company needs a different type of worker tomorrow?** They call the same agency. The agency decides. The company's process does not change, only the agency's decision does.
- **What if a new type of worker needs to be introduced?** A new specialist agency is created to handle that. Everything else stays exactly the same.

### In Simple Terms

> Define an interface for creating an object, but let subclasses decide which class to instantiate. The factory method lets a class defer instantiation to subclasses.

### Wikipedia describes it as:

> *"In object-oriented programming, the factory method pattern is a design pattern that uses factory methods to deal with the problem of creating objects without having to specify their exact classes. Factory methods can be specified in an interface and implemented by subclasses, or implemented in a base class and optionally overridden by subclasses."*
>
> **Source:** [Wikipedia - Factory method pattern](https://en.wikipedia.org/wiki/Factory_method_pattern)

### Programming Example

The agency is the factory. The worker types are the products. The company is the client.

```csharp
// The worker interface - all workers can do a job
public interface IWorker
{
    void DoWork();
}
```

```csharp
// The concrete workers
public class Developer : IWorker
{
    public void DoWork() => Console.WriteLine("Developer: Writing code.");
}

public class Designer : IWorker
{
    public void DoWork() => Console.WriteLine("Designer: Creating designs.");
}
```

```csharp
// The base agency - declares the factory method
public abstract class RecruitmentAgency
{
    // This is the Factory Method - subclasses decide who to hire
    public abstract IWorker HireWorker();
}
```

```csharp
// Concrete agencies - each one decides which worker to send
public class TechAgency : RecruitmentAgency
{
    public override IWorker HireWorker() => new Developer();
}

public class DesignAgency : RecruitmentAgency
{
    public override IWorker HireWorker() => new Designer();
}
```

Now let's see it in action:

```csharp
// Company A needs a tech worker
RecruitmentAgency agency = new TechAgency();
IWorker worker = agency.HireWorker();
worker.DoWork();

// Company B needs a design worker
RecruitmentAgency agency2 = new DesignAgency();
IWorker worker2 = agency2.HireWorker();
worker2.DoWork();
```

**Output:**
```
Developer: Writing code.
Designer: Creating designs.
```

> The company never used `new Developer()` or `new Designer()` directly. The agency made that decision. That is the Factory Method.

---

Abstract Factory
-------------------

### Real World Example

> Think of a **furniture store that sells collections**. You walk in and choose a style: Modern or Victorian. Once you choose, everything you get comes from that same collection. The sofa, the chair, the coffee table all match. The store ensures you never walk out with a modern sofa paired with a Victorian chair. You do not pick individual pieces and hope they go together. The collection guarantees they will.
>
> That is the Abstract Factory. You choose a family, and the factory produces every object you need from that same family. Everything it gives you is guaranteed to work together.

### Problems it solves

- **What if a customer mixes furniture from different collections?** The room looks inconsistent. The store solves this by grouping everything into collections. You pick one collection and everything comes from it.
- **What if the store wants to introduce a new collection?** They create a new collection set. Every existing collection stays untouched. The customer's experience does not change, only the options grow.
- **What if different stores carry different collections?** Each store is its own factory. A customer walks into any store and follows the same process. The store handles which specific pieces to provide.

### In Simple Terms

> Provide an interface for creating families of related objects, without specifying their concrete classes.

### Wikipedia describes it as:

> *"The abstract factory pattern provides a way to create families of related objects without imposing their concrete classes, by encapsulating a group of individual factories that have a common theme without specifying their concrete classes."*
>
> **Source:** [Wikipedia - Abstract factory pattern](https://en.wikipedia.org/wiki/Abstract_factory_pattern)

### Programming Example

The furniture store is the abstract factory. Modern and Victorian are the concrete factories. Sofa and Chair are the products.

```csharp
// The product interfaces - every furniture type has a contract
public interface ISofa  { void Describe(); }
public interface IChair { void Describe(); }
```

```csharp
// Modern collection
public class ModernSofa : ISofa
{
    public void Describe() => Console.WriteLine("Sofa: Sleek modern design.");
}

public class ModernChair : IChair
{
    public void Describe() => Console.WriteLine("Chair: Minimalist modern style.");
}
```

```csharp
// Victorian collection
public class VictorianSofa : ISofa
{
    public void Describe() => Console.WriteLine("Sofa: Ornate Victorian design.");
}

public class VictorianChair : IChair
{
    public void Describe() => Console.WriteLine("Chair: Classic Victorian style.");
}
```

```csharp
// The abstract factory - every store can produce a sofa and a chair
public interface IFurnitureFactory
{
    ISofa  CreateSofa();
    IChair CreateChair();
}
```

```csharp
// Concrete factories - each one produces its own collection
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
```

Now let's see it in action:

```csharp
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
```

**Output:**
```
Sofa: Sleek modern design.
Chair: Minimalist modern style.
Sofa: Ornate Victorian design.
Chair: Classic Victorian style.
```

> Every piece came from the same collection. The client never used `new ModernSofa()` or `new VictorianChair()` directly. The factory kept the family together. That is the Abstract Factory.

### When to use it

- When your system needs to work with **multiple families of related objects** and you need to ensure they are always used together
- When you want to **swap out an entire family** of objects in one place without touching the rest of your code
- When you want to **enforce consistency** across related objects, so nothing from one family gets accidentally mixed with another

---

Builder
-------------------

### Real World Example

> Think of a **tailor making a suit**. Every customer that walks in goes through the same process: take measurements, choose the fabric, select the lining, pick the buttons, decide on the lapel style. The tailor follows those same steps for every order. But the finished suit is completely unique to each customer. A businessman walks out with a sharp formal suit. A wedding guest walks out with something entirely different. Same process, same tailor, different result every time.
>
> That is the Builder. The construction process stays the same. What changes are the choices made at each step.

### Problems it solves

- **What if a suit had to be assembled all at once with no steps?** You would have to know every detail upfront and get it all right in one go. The tailor breaks it down into steps so each decision is made clearly, one at a time.
- **What if two customers want completely different suits but go through the same tailor?** The tailor follows the same process for both. The steps do not change, only the choices within each step.
- **What if a new suit style needs to be introduced?** A new set of choices is defined for that style. The tailoring process itself stays untouched.

### In Simple Terms

> Separate the construction of a complex object from its representation, so that the same construction process can create different results.

### Wikipedia describes it as:

> *"The Builder pattern separates the construction of a complex object from its representation so that the same construction process can create different representations."*
>
> **Source:** [Wikipedia - Builder pattern](https://en.wikipedia.org/wiki/Builder_pattern)

### Programming Example

The tailor is the director. The suit is the product. The builder handles the step-by-step construction.

```csharp
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
```

```csharp
// The builder - defines the steps
public interface ISuitBuilder
{
    void SetFabric();
    void SetLining();
    void SetButtons();
    Suit GetSuit();
}
```

```csharp
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
```

```csharp
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
```

Now let's see it in action:

```csharp
Tailor tailor = new Tailor();

Suit businessSuit = tailor.MakeSuit(new BusinessSuitBuilder());
businessSuit.Describe();

Suit weddingSuit = tailor.MakeSuit(new WeddingSuitBuilder());
weddingSuit.Describe();
```

**Output:**
```
Suit: Dark wool fabric, Silk lining, Black horn buttons.
Suit: Ivory linen fabric, Satin lining, Pearl buttons.
```

> The same tailor, the same process, two completely different suits. That is the Builder.

### When to use it

- When an object has **many parts or configurations** and building it all at once would be confusing
- When you want the **same construction process** to produce different results depending on the choices made at each step
- When you want to keep the **construction logic separate** from the object itself, so each can change independently

---

Prototype
-------------------

### Real World Example

> Imagine you are building a drawing application. Users can create shapes — circles, rectangles, triangles — each with its own colour, size, and position. Now imagine the user wants ten red circles of the same size placed across the canvas. Creating each one from scratch means repeating the same setup ten times. What if the shape is complex, with many configured properties? That becomes expensive and repetitive.
>
> The Prototype pattern solves this by letting you take one fully configured shape and clone it. The clone starts as an exact copy. The user then moves it, recolours it, or resizes it independently. The original shape is never touched. This also means new shape types can be added at runtime without the application needing to know about them in advance.

### Problems it solves

- **Creating a new shape from scratch every time is expensive.** If a shape has many properties, setting them all up repeatedly wastes resources. Cloning an already configured object is far cheaper.
- **The application should not need to know the exact type of shape it is copying.** At runtime, shapes can be added or removed dynamically. The app just calls clone and gets back a ready object, whatever type it happens to be.
- **Modifying a copy should never affect the original.** Each cloned shape is fully independent. Changes to the copy stay with the copy.

### In Simple Terms

> Create new objects by copying an existing one. The copy starts identical to the original and can then be changed independently.

### Wikipedia describes it as:

> *"The Prototype pattern is used when the type of objects to create is determined by a prototypical instance, which is cloned to produce new objects."*
>
> **Source:** [Wikipedia - Prototype pattern](https://en.wikipedia.org/wiki/Prototype_pattern)

### Programming Example

Every shape knows how to clone itself. The application never calls `new Circle()` or `new Rectangle()` directly at runtime — it clones what already exists.

```csharp
// The prototype interface - every shape must be able to clone itself
public abstract class Shape
{
    public string Colour { get; set; }
    public int    Size   { get; set; }

    public abstract Shape Clone();
    public abstract void  Describe();
}
```

```csharp
// Concrete shapes
public class Circle : Shape
{
    public override Shape Clone()    => (Shape)this.MemberwiseClone();
    public override void  Describe() => Console.WriteLine($"Circle  | Colour: {Colour} | Size: {Size}");
}

public class Rectangle : Shape
{
    public override Shape Clone()    => (Shape)this.MemberwiseClone();
    public override void  Describe() => Console.WriteLine($"Rectangle | Colour: {Colour} | Size: {Size}");
}
```

Now let's see it in action:

```csharp
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
```

**Output:**
```
Circle  | Colour: Red  | Size: 50
Circle  | Colour: Red  | Size: 50
Circle  | Colour: Blue | Size: 50
```

> `clone2` changed to blue. The original stayed red. Each object is fully independent. That is the Prototype.

### When to use it

- When creating a new object from scratch is **expensive or complex** and an existing object already has everything configured
- When the application needs to create objects **at runtime without knowing their exact type** in advance
- When you need many variations of an object and want to **start from a known good state** rather than rebuild every time

