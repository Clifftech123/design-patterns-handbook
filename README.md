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

---

Structural Design Patterns
===========================

Simply put:

> Structural patterns are all about **how classes and objects are composed to form larger structures**. They use inheritance and composition to let you build flexible, efficient structures without having to rewrite everything from scratch.

Wikipedia describes them as:

> *"In software engineering, structural patterns are design patterns that ease the design by identifying a simple way to realize relationships among entities."*
>
> **Source:** [Wikipedia - Structural pattern](https://en.wikipedia.org/wiki/Structural_pattern)

- They describe how objects and classes are combined to form **larger, more complex structures** while keeping those structures flexible and efficient.
- They focus on **composition over inheritance** — how you connect things, not just what things are.

There are **7 structural design patterns**:

1. **Adapter**: Converts one interface into another that a client expects, letting incompatible interfaces work together.
2. **Bridge**: Decouples an abstraction from its implementation so the two can vary independently.
3. **Composite**: Composes objects into tree structures to represent part-whole hierarchies, letting clients treat individual objects and compositions uniformly.
4. **Decorator**: Attaches additional responsibilities to an object dynamically, as a flexible alternative to subclassing.
5. **Facade**: Provides a simplified, unified interface to a complex subsystem.
6. **Flyweight**: Uses sharing to efficiently support a large number of fine-grained objects.
7. **Proxy**: Provides a surrogate or placeholder for another object to control access to it.

* [Adapter](#adapter)
* [Bridge](#bridge)
* [Composite](#composite)
* [Decorator](#decorator)
* [Facade](#facade)
* [Flyweight](#flyweight)
* [Proxy](#proxy)


Adapter
-------------------

### Real World Example

> Think of a **language translator at a business meeting**. A British CEO needs to address a Japanese team. The CEO speaks only English. The team speaks only Japanese. A translator sits between them, converting every English sentence into Japanese and delivering it to the team. Both sides keep speaking their own language. Neither the CEO nor the team change anything about how they communicate. The translator makes them compatible.
>
> That is the Adapter. The client speaks one interface. The other side speaks a different one. The Adapter sits between them and makes both sides work together without either having to change.

### Problems it solves

- **The CEO cannot speak Japanese, and the team cannot speak English.** They are incompatible. The translator adapts one to the other without changing either side.
- **What if the CEO now needs to address a French team?** A French translator is brought in. The CEO's process does not change. Only the translator changes.
- **What if an existing class has a useful method but the wrong interface?** You wrap it in an adapter. The rest of the system talks to the adapter. The existing class stays untouched.

### In Simple Terms

> Wrap an existing class with a new interface so the client can use it without any changes to either side.

### Wikipedia describes it as:

> *"In software engineering, the adapter pattern is a software design pattern (also known as wrapper) that allows the interface of an existing class to be used as another interface. It is often used to make existing classes work with others without modifying their source code."*
>
> **Source:** [Wikipedia - Adapter pattern](https://en.wikipedia.org/wiki/Adapter_pattern)

### Programming Example

The CEO is the client. The Japanese team member is the adaptee — useful, but speaks the wrong interface. The Translator is the adapter.

```csharp
// What the CEO expects, someone who can receive a message in English
public interface IEnglishSpeaker
{
    void Speak(string message);
}
```

```csharp
// The Japanese team member, speaks only Japanese (the adaptee)
public class JapaneseTeamMember
{
    public void SpeakJapanese(string message)
    {
        Console.WriteLine($"Team member (Japanese): {message}");
    }
}
```

```csharp
// The Translator, adapts the Japanese speaker to the English interface
public class Translator : IEnglishSpeaker
{
    private readonly JapaneseTeamMember _teamMember;

    public Translator(JapaneseTeamMember teamMember)
    {
        _teamMember = teamMember;
    }

    public void Speak(string message)
    {
        string translated = TranslateToJapanese(message);
        _teamMember.SpeakJapanese(translated);
    }

    private string TranslateToJapanese(string english) => english switch
    {
        "Good morning, team."         => "おはようございます、チームの皆さん。",
        "Please review the proposal." => "提案書を確認してください。",
        _                             => $"[Japanese: {english}]"
    };
}
```

```csharp
// The CEO, only knows how to talk to an IEnglishSpeaker
public class CEO
{
    private readonly IEnglishSpeaker _speaker;

    public CEO(IEnglishSpeaker speaker)
    {
        _speaker = speaker;
    }

    public void Address(string message)
    {
        Console.WriteLine($"CEO (English): {message}");
        _speaker.Speak(message);
    }
}
```

Now let's see it in action:

```csharp
JapaneseTeamMember teamMember = new JapaneseTeamMember();
IEnglishSpeaker translator    = new Translator(teamMember);
CEO ceo = new CEO(translator);

ceo.Address("Good morning, team.");
ceo.Address("Please review the proposal.");
```

**Output:**
```
CEO (English): Good morning, team.
Team member (Japanese): おはようございます、チームの皆さん。
CEO (English): Please review the proposal.
Team member (Japanese): 提案書を確認してください。
```

> The CEO never knew about `JapaneseTeamMember`. The team never knew about the CEO's interface. The `Translator` made both sides work together without touching either. That is the Adapter.

### When to use it

- When you want to use an **existing class but its interface does not match** what your code expects
- When you want to create a **reusable class** that cooperates with classes that do not have compatible interfaces
- When you need to **integrate a third-party library or legacy code** without modifying it

---

Bridge
-------------------

### Real World Example

> Think of a **TV remote control and a television**. The remote is one thing. The TV is another. You can have a basic remote or a smart remote. You can have a Sony TV or a Samsung TV. Any remote works with any TV  you are not locked in. Buy a new Samsung TV, and your old remote still works. Buy a smart universal remote, and it works with every TV you own. Neither side needs to know the inner details of the other.
>
> That is the Bridge. The abstraction (remote) and the implementation (TV) are two separate hierarchies that can grow and change completely independently of each other.

### Problems it solves

- **What if every remote was hardwired to one specific TV brand?** You would need a SonyBasicRemote, a SamsungBasicRemote, a SonySmartRemote, a SamsungSmartRemote  one class for every combination. Adding one new TV brand would double your remote classes. The Bridge stops this explosion.
- **What if you want to add a new remote type without touching the TVs?** With Bridge, you just create a new remote class. The TVs are untouched.
- **What if you want to add a new TV brand without touching the remotes?** Same answer. You add a new TV class. Every existing remote already works with it.

### In Simple Terms

> Split a large class into two separate hierarchies  the abstraction and the implementation  so each can be changed and extended without affecting the other.

### Wikipedia describes it as:

> *"The bridge pattern is a design pattern used in software engineering that is meant to decouple an abstraction from its implementation so that the two can vary independently."*
>
> **Source:** [Wikipedia - Bridge pattern](https://en.wikipedia.org/wiki/Bridge_pattern)

### Programming Example

The remote control is the abstraction. The TV brand is the implementation. They are connected through a bridge  the `ITV` interface  but neither hierarchy depends on the other's details.

```csharp
// The implementation interface — what any TV must be able to do
public interface ITV
{
    void TurnOn();
    void TurnOff();
    void SetChannel(int channel);
    void SetVolume(int volume);
}
```

```csharp
// Concrete implementations — each brand handles things its own way
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
```

```csharp
// The abstraction — the remote holds a reference to whichever TV it controls
public abstract class RemoteControl
{
    protected ITV _tv;

    protected RemoteControl(ITV tv) { _tv = tv; }

    public abstract void TurnOn();
    public abstract void TurnOff();
    public abstract void SetChannel(int channel);
}
```

```csharp
// Refined abstraction — a basic remote, does exactly what the TV does
public class BasicRemote : RemoteControl
{
    public BasicRemote(ITV tv) : base(tv) { }

    public override void TurnOn()           => _tv.TurnOn();
    public override void TurnOff()          => _tv.TurnOff();
    public override void SetChannel(int ch) => _tv.SetChannel(ch);
}

// Refined abstraction — a smart remote, adds its own behaviour on top
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
```

Now let's see it in action:

```csharp
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

// Swap freely — smart remote now with Sony, no code changes needed
Console.WriteLine("\n--- Smart Remote + Sony TV ---");
SmartRemote smartSony = new SmartRemote(new SonyTV());
smartSony.TurnOn();
smartSony.SetChannel(3);
smartSony.TurnOff();
```

**Output:**
```
--- Basic Remote + Sony TV ---
Sony TV: Powering on. BRAVIA display ready.
Sony TV: Switching to channel 5.
Sony TV: Shutting down.

--- Smart Remote + Samsung TV ---
Smart Remote: Activating voice control.
Samsung TV: Turning on. Smart Hub loading.
Smart Remote: Looking up channel guide.
Samsung TV: Channel 10 selected.
Samsung TV: Volume at 20.
Smart Remote: Saving watch history.
Samsung TV: Powering off.

--- Smart Remote + Sony TV ---
Smart Remote: Activating voice control.
Sony TV: Powering on. BRAVIA display ready.
Smart Remote: Looking up channel guide.
Sony TV: Switching to channel 3.
Smart Remote: Saving watch history.
Sony TV: Shutting down.
```

> The same `SmartRemote` worked with both Sony and Samsung without any changes. Adding a new TV brand like LG means creating one new class — every existing remote works with it immediately. That is the Bridge.

### When to use it

- When you want to **avoid a permanent binding** between an abstraction and its implementation, so either can be swapped at runtime
- When both the abstraction and the implementation should be **independently extensible through subclassing**
- When changes to the implementation should have **no impact on the client code** — the client should not need to be recompiled

---

Composite
-------------------

### Real World Example

> Think of a **company organisation chart**. A company has a CEO. Under the CEO are department heads, each leading a department full of employees. Under some departments are even smaller sub-teams. Now imagine you want to know the total salary cost. You can ask a single employee  they tell you their salary. You can ask a whole department  it adds up every person inside it, including nested teams. You can ask the entire company  it rolls up every salary across every level. The same question, asked the same way, whether you are talking to one person or thousands.
>
> That is the Composite. Individual items and groups of items share the same interface. The caller never needs to know which one they are dealing with.

### Problems it solves

- **What if you had to write different code to handle a single employee versus a whole department?** You would end up with `if` checks everywhere just to figure out what you are talking to. Composite removes that entirely  one interface, always.
- **What if departments can contain other departments?** Composite handles any depth of nesting naturally. The caller just asks the top of the tree and the operation flows down automatically.
- **What if you want to add a new type of team or role?** You implement the same interface. Everything above it in the tree keeps working without any changes.

### In Simple Terms

> Compose objects into tree structures. Let individual objects and groups of objects be treated through the same interface, so the caller never has to care about the difference.

### Wikipedia describes it as:

> *"The composite pattern describes a group of objects that are treated the same way as a single instance of the same type of object. The intent of a composite is to compose objects into tree structures to represent part-whole hierarchies."*
>
> **Source:** [Wikipedia - Composite pattern](https://en.wikipedia.org/wiki/Composite_pattern)

### Programming Example

Every node in the tree  whether a single employee or an entire department  implements `IEmployee`. The caller treats them identically.

```csharp
// The component interface — every leaf and composite shares this contract
public interface IEmployee
{
    string Name    { get; }
    int    GetSalary();
    void   GetDetails(string indent = "");
}
```

```csharp
// The leaf — a single employee with no reports
public class Employee : IEmployee
{
    private readonly int _salary;

    public string Name { get; }

    public Employee(string name, int salary)
    {
        Name    = name;
        _salary = salary;
    }

    public int  GetSalary()                    => _salary;
    public void GetDetails(string indent = "") => Console.WriteLine($"{indent}- {Name} (£{_salary:N0})");
}
```

```csharp
// The composite — a department that holds employees or other departments
public class Department : IEmployee
{
    private readonly List<IEmployee> _members = new();

    public string Name { get; }

    public Department(string name) { Name = name; }

    public void Add(IEmployee employee)    => _members.Add(employee);
    public void Remove(IEmployee employee) => _members.Remove(employee);

    public int GetSalary() => _members.Sum(m => m.GetSalary());

    public void GetDetails(string indent = "")
    {
        Console.WriteLine($"{indent}[{Name}] Total: £{GetSalary():N0}");
        foreach (var member in _members)
            member.GetDetails(indent + "  ");
    }
}
```

Now let's see it in action:

```csharp
// Individual employees
var ceo        = new Employee("Alice (CEO)",        120_000);
var cto        = new Employee("Bob (CTO)",           95_000);
var dev1       = new Employee("Carol (Developer)",   65_000);
var dev2       = new Employee("David (Developer)",   62_000);
var cfo        = new Employee("Eve (CFO)",           90_000);
var accountant = new Employee("Frank (Accountant)",  55_000);

// Build the Engineering department
var engineering = new Department("Engineering");
engineering.Add(cto);
engineering.Add(dev1);
engineering.Add(dev2);

// Build the Finance department
var finance = new Department("Finance");
finance.Add(cfo);
finance.Add(accountant);

// Build the whole company
var company = new Department("Acme Corp");
company.Add(ceo);
company.Add(engineering);
company.Add(finance);

// Ask the whole company — one call, rolls up everything
Console.WriteLine("=== Full Company ===");
company.GetDetails();

// Ask just one department — same call, same interface
Console.WriteLine("\n=== Engineering Only ===");
engineering.GetDetails();

// Ask a single employee — same call, same interface
Console.WriteLine("\n=== Single Employee ===");
dev1.GetDetails();
```

**Output:**
```
=== Full Company ===
[Acme Corp] Total: £487,000
  - Alice (CEO) (£120,000)
  [Engineering] Total: £222,000
    - Bob (CTO) (£95,000)
    - Carol (Developer) (£65,000)
    - David (Developer) (£62,000)
  [Finance] Total: £145,000
    - Eve (CFO) (£90,000)
    - Frank (Accountant) (£55,000)

=== Engineering Only ===
[Engineering] Total: £222,000
  - Bob (CTO) (£95,000)
  - Carol (Developer) (£65,000)
  - David (Developer) (£62,000)

=== Single Employee ===
- Carol (Developer) (£65,000)
```

> `company.GetDetails()`, `engineering.GetDetails()`, `dev1.GetDetails()` — the same call on three different levels of the tree. The caller never checked what it was talking to. That is the Composite.

### When to use it

- When you need to represent **part-whole hierarchies** — trees where individual items and groups of items need to be used interchangeably
- When you want **client code to treat single objects and collections of objects uniformly**, without any special-casing
- When the **structure can be nested to any depth** and that depth should not affect how the caller interacts with it

---

Decorator
-------------------

### Real World Example

> Think of ordering a **coffee at a cafe**. You start with a plain espresso. Then you ask for milk. Then vanilla syrup. Then whipped cream on top. Each addition wraps around what was already there, adding its own cost and its own description. The espresso at the centre never changes. You are just layering on top of it, one addition at a time. You could add two shots of syrup. You could skip the milk entirely. Every combination is possible without creating a new type of coffee for each one.
>
> That is the Decorator. You start with a base object and wrap it in layers. Each layer adds its own behaviour and then delegates to whatever is underneath it.

### Problems it solves

- **What if you needed a class for every combination?** EspressoWithMilk, EspressoWithMilkAndVanilla, EspressoWithMilkAndVanillaAndCream  the list explodes. Decorator adds behaviour at runtime, so you never need those classes.
- **What if the base coffee should not change?** It does not. The espresso class stays untouched. The decorators wrap around it and extend it independently.
- **What if a new topping needs to be added?** You create one new decorator class. Every existing combination still works exactly as before.

### In Simple Terms

> Wrap an object in one or more layers, where each layer adds its own behaviour before or after delegating to the layer beneath it.

### Wikipedia describes it as:

> *"The decorator pattern is a design pattern that allows behaviour to be added to an individual object, dynamically, without affecting the behaviour of other instances of the same class."*
>
> **Source:** [Wikipedia - Decorator pattern](https://en.wikipedia.org/wiki/Decorator_pattern)

### Programming Example

The coffee is the component. Each topping is a decorator. Every decorator wraps the component and adds to its description and cost.

```csharp
// The component interface — every coffee, plain or decorated, shares this
public interface ICoffee
{
    string GetDescription();
    double GetCost();
}
```

```csharp
// The base component — a plain espresso
public class Espresso : ICoffee
{
    public string GetDescription() => "Espresso";
    public double GetCost()        => 1.00;
}
```

```csharp
// The base decorator — wraps any ICoffee and delegates to it
public abstract class CoffeeDecorator : ICoffee
{
    protected readonly ICoffee _coffee;

    protected CoffeeDecorator(ICoffee coffee) { _coffee = coffee; }

    public virtual string GetDescription() => _coffee.GetDescription();
    public virtual double GetCost()        => _coffee.GetCost();
}
```

```csharp
// Concrete decorators — each one adds its own layer
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
```

Now let's see it in action:

```csharp
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
```

**Output:**
```
Espresso => £1.00
Espresso, Milk => £1.30
Espresso, Milk, Vanilla Syrup => £1.80
Espresso, Milk, Vanilla Syrup, Whipped Cream => £2.55
```

> Each line is a new layer wrapped around the previous one. The espresso never changed. The cost and description grew with every wrapper. That is the Decorator.

### When to use it

- When you want to **add responsibilities to individual objects** without affecting other objects of the same class
- When **subclassing would lead to an explosion of classes** to cover every possible combination of behaviours
- When you need to be able to **stack behaviours in any order** at runtime, independently of each other

---

Facade
-------------------

### Real World Example

> Think of clicking **"Place Order"** on a shopping website. In that single click, several things happen behind the scenes: the system checks whether the item is in stock, your payment is charged, a shipping label is generated, and a confirmation email is sent to you. You do not see any of that. You click one button and get one result. The complexity of four separate systems is hidden behind a single, clean action.
>
> That is the Facade. One simple interface in front of many complex moving parts. The caller does not need to know what is happening behind the scenes.

### Problems it solves

- **What if the client had to call each subsystem directly?** Check inventory, then process payment, then generate a label, then send an email — in the right order, handling each failure separately. The Facade wraps all of that into one call.
- **What if one of the subsystems changes?** The Facade absorbs the change. The client code never needs to know. Only the Facade is updated.
- **What if different clients need the same flow?** They all call the same Facade method. The logic is in one place, not duplicated across every caller.

### In Simple Terms

> Provide a single, simple interface that hides the complexity of a set of subsystems behind it.

### Wikipedia describes it as:

> *"The facade pattern (also spelled façade) is a software-design pattern commonly used in object-oriented programming. Analogous to a facade in architecture, a facade is an object that serves as a front-facing interface masking more complex underlying or structural code."*
>
> **Source:** [Wikipedia - Facade pattern](https://en.wikipedia.org/wiki/Facade_pattern)

### Programming Example

Each subsystem does its own job. The `OrderFacade` is the single entry point that coordinates all of them. The client only ever talks to the facade.

```csharp
// Subsystem 1: checks whether the item is available
public class InventoryService
{
    public bool CheckStock(string item)
    {
        Console.WriteLine($"Inventory: Checking stock for {item}.");
        return true;
    }
}
```

```csharp
// Subsystem 2: handles the payment
public class PaymentService
{
    public bool ProcessPayment(string cardNumber, double amount)
    {
        Console.WriteLine($"Payment: Charging £{amount:F2} to card ending {cardNumber[^4..]}.");
        return true;
    }
}
```

```csharp
// Subsystem 3: generates a shipping label
public class ShippingService
{
    public string GenerateLabel(string item, string address)
    {
        Console.WriteLine($"Shipping: Generating label for {item} to {address}.");
        return "TRACK-29384";
    }
}
```

```csharp
// Subsystem 4: sends the confirmation email
public class EmailService
{
    public void SendConfirmation(string email, string trackingCode)
    {
        Console.WriteLine($"Email: Confirmation sent to {email}. Tracking code: {trackingCode}.");
    }
}
```

```csharp
// The Facade — one method, hides all four subsystems
public class OrderFacade
{
    private readonly InventoryService _inventory = new();
    private readonly PaymentService   _payment   = new();
    private readonly ShippingService  _shipping  = new();
    private readonly EmailService     _email     = new();

    public void PlaceOrder(string item, string cardNumber, double amount, string address, string email)
    {
        Console.WriteLine("=== Placing Order ===");

        if (!_inventory.CheckStock(item))
        {
            Console.WriteLine("Order failed: item out of stock.");
            return;
        }

        if (!_payment.ProcessPayment(cardNumber, amount))
        {
            Console.WriteLine("Order failed: payment declined.");
            return;
        }

        string trackingCode = _shipping.GenerateLabel(item, address);
        _email.SendConfirmation(email, trackingCode);

        Console.WriteLine($"\nOrder complete. Your tracking code is {trackingCode}.");
    }
}
```

Now let's see it in action:

```csharp
OrderFacade store = new OrderFacade();

store.PlaceOrder(
    item:       "Wireless Headphones",
    cardNumber: "4111111111111234",
    amount:     79.99,
    address:    "42 Maple Street, London",
    email:      "customer@email.com"
);
```

**Output:**
```
=== Placing Order ===
Inventory: Checking stock for Wireless Headphones.
Payment: Charging £79.99 to card ending 1234.
Shipping: Generating label for Wireless Headphones to 42 Maple Street, London.
Email: Confirmation sent to customer@email.com. Tracking code: TRACK-29384.

Order complete. Your tracking code is TRACK-29384.
```

> The client called one method. Four subsystems ran in the right order. None of that complexity was visible to the caller. That is the Facade.

### When to use it

- When you want to provide a **simple interface to a complex subsystem** so callers are not burdened by its internals
- When you want to **layer your system** so that high-level code talks to facades, not directly to low-level subsystems
- When you want a **single entry point** that coordinates a sequence of steps across multiple services

---

Flyweight
-------------------

### Real World Example

> Think of a **game that renders a forest**. The forest has ten thousand trees. Each tree has a type name, a colour, and a texture — but most of those trees are Oaks, and all Oaks look exactly the same. Creating ten thousand separate objects, each storing the same name, colour, and texture, wastes enormous amounts of memory. Instead, you create one shared Oak object that holds all that data. Every Oak tree in the forest points to that same shared object and only stores its own position on the map.
>
> That is the Flyweight. The data that is the same across many instances is shared. The data that is unique per instance is stored separately and passed in only when needed.

### Problems it solves

- **What if you created a full object for every single tree?** With ten thousand trees, you store the same name, colour, and texture ten thousand times. Flyweight stores that shared data once and reuses it everywhere.
- **What if a new tree type is introduced?** The factory creates one new shared object for it. Every tree of that type immediately uses it without any extra memory.
- **What if the forest needs to render each tree at its own position?** The position is unique per tree, so it is stored on the tree itself and passed to the shared object only at render time. The shared object never holds it.

### In Simple Terms

> Split an object's data into what is shared across many instances and what is unique per instance. Share the common part. Pass the unique part in only when needed.

### Wikipedia describes it as:

> *"A flyweight is an object that minimizes memory usage by sharing as much data as possible with other similar objects. It is a way to use objects in large numbers when a simple repeated representation would use an unacceptable amount of memory."*
>
> **Source:** [Wikipedia - Flyweight pattern](https://en.wikipedia.org/wiki/Flyweight_pattern)

### Programming Example

`TreeType` is the flyweight — it holds shared data. `Tree` holds only the unique position and a reference to a shared `TreeType`. The factory ensures each `TreeType` is created only once.

```csharp
// The flyweight — holds shared intrinsic state (same for all trees of this type)
public class TreeType
{
    public string Name    { get; }
    public string Colour  { get; }
    public string Texture { get; }

    public TreeType(string name, string colour, string texture)
    {
        Name    = name;
        Colour  = colour;
        Texture = texture;
    }

    public void Render(int x, int y)
    {
        Console.WriteLine($"Rendering {Name} tree ({Colour}, {Texture}) at ({x}, {y})");
    }
}
```

```csharp
// The flyweight factory — creates and caches tree types so they are never duplicated
public class TreeTypeFactory
{
    private readonly Dictionary<string, TreeType> _cache = new();

    public TreeType GetTreeType(string name, string colour, string texture)
    {
        string key = $"{name}_{colour}_{texture}";

        if (!_cache.ContainsKey(key))
        {
            Console.WriteLine($"Factory: Creating new TreeType for '{name}'.");
            _cache[key] = new TreeType(name, colour, texture);
        }

        return _cache[key];
    }

    public int TotalTypes => _cache.Count;
}
```

```csharp
// The context — holds unique extrinsic state (position) and a reference to a shared flyweight
public class Tree
{
    private readonly int      _x;
    private readonly int      _y;
    private readonly TreeType _type;

    public Tree(int x, int y, TreeType type)
    {
        _x    = x;
        _y    = y;
        _type = type;
    }

    public void Render() => _type.Render(_x, _y);
}
```

```csharp
// The forest — plants trees using shared flyweights
public class Forest
{
    private readonly List<Tree>      _trees   = new();
    private readonly TreeTypeFactory _factory = new();

    public void PlantTree(int x, int y, string name, string colour, string texture)
    {
        TreeType type = _factory.GetTreeType(name, colour, texture);
        _trees.Add(new Tree(x, y, type));
    }

    public void Render()
    {
        foreach (var tree in _trees)
            tree.Render();
    }

    public int TreeCount     => _trees.Count;
    public int TreeTypeCount => _factory.TotalTypes;
}
```

Now let's see it in action:

```csharp
Forest forest = new Forest();

// Plant 6 trees — but only 2 unique types
forest.PlantTree(1,  5,  "Oak",  "Dark Green",  "Rough bark");
forest.PlantTree(3,  12, "Oak",  "Dark Green",  "Rough bark");
forest.PlantTree(7,  2,  "Oak",  "Dark Green",  "Rough bark");
forest.PlantTree(10, 8,  "Pine", "Light Green", "Smooth bark");
forest.PlantTree(15, 3,  "Pine", "Light Green", "Smooth bark");
forest.PlantTree(20, 14, "Pine", "Light Green", "Smooth bark");

forest.Render();

Console.WriteLine($"\nTrees planted:              {forest.TreeCount}");
Console.WriteLine($"Unique tree types in memory: {forest.TreeTypeCount}");
```

**Output:**
```
Factory: Creating new TreeType for 'Oak'.
Factory: Creating new TreeType for 'Pine'.
Rendering Oak tree (Dark Green, Rough bark) at (1, 5)
Rendering Oak tree (Dark Green, Rough bark) at (3, 12)
Rendering Oak tree (Dark Green, Rough bark) at (7, 2)
Rendering Pine tree (Light Green, Smooth bark) at (10, 8)
Rendering Pine tree (Light Green, Smooth bark) at (15, 3)
Rendering Pine tree (Light Green, Smooth bark) at (20, 14)

Trees planted:              6
Unique tree types in memory: 2
```

> Six trees, but only two `TreeType` objects were ever created. Scale that to ten thousand trees and the factory still creates exactly two. The positions are unique per tree — the appearance is shared. That is the Flyweight.

### When to use it

- When your application needs to create a **very large number of similar objects** that would otherwise consume too much memory
- When most of the object's state can be made **shared across instances**, with only a small part being unique per instance
- When the **unique part of the state can be passed in externally** rather than stored inside every object

---

Proxy
-------------------

### Real World Example

> Think of a **security guard at the entrance of an office building**. You cannot walk straight into the building. You have to go through the guard first. The guard checks your name against the authorised list, logs your visit, and only then lets you through. If you are not on the list, you are turned away. The building itself never deals with any of that. It just lets people in. All the checking, logging, and decision-making happens at the guard — the proxy — before the building ever gets involved.
>
> That is the Proxy. It sits between the caller and the real object, controls what gets through, and can add behaviour like access checks or logging without the real object knowing anything about it.

### Problems it solves

- **What if anyone could walk straight into the building?** There would be no access control. The proxy intercepts every request and decides whether it should be allowed through.
- **What if you need to log every entry without changing the building?** The proxy handles it. The real building stays simple and focused on its own job.
- **What if the real object is expensive to create and you want to delay that?** The proxy can hold off creating it until someone actually passes the check and needs it.

### In Simple Terms

> Place an object in front of another object to control access to it. The caller thinks it is talking directly to the real object, but the proxy is handling it first.

### Wikipedia describes it as:

> *"A proxy, in its most general form, is a class functioning as an interface to something else. The proxy could interface to anything: a network connection, a large object in memory, a file, or some other resource that is expensive or impossible to duplicate."*
>
> **Source:** [Wikipedia - Proxy pattern](https://en.wikipedia.org/wiki/Proxy_pattern)

### Programming Example

The client talks to `IBuilding`. The `SecurityGuard` is the proxy — it implements the same interface, controls access, and only lets authorised visitors through to the `OfficeBuilding`.

```csharp
// The subject interface — the building and the proxy both implement this
public interface IBuilding
{
    void Enter(string visitorName);
}
```

```csharp
// The real subject — the actual building, just grants entry
public class OfficeBuilding : IBuilding
{
    public void Enter(string visitorName)
    {
        Console.WriteLine($"Building: {visitorName} has entered.");
    }
}
```

```csharp
// The proxy — the security guard controls who gets through
public class SecurityGuard : IBuilding
{
    private readonly OfficeBuilding _building          = new();
    private readonly List<string>   _authorisedVisitors = new() { "Alice", "Bob", "Carol" };

    public void Enter(string visitorName)
    {
        Console.WriteLine($"Guard: {visitorName} is requesting entry.");

        if (_authorisedVisitors.Contains(visitorName))
        {
            Console.WriteLine("Guard: ID verified. Access granted.");
            _building.Enter(visitorName);
        }
        else
        {
            Console.WriteLine($"Guard: {visitorName} is not on the list. Access denied.");
        }
    }
}
```

Now let's see it in action:

```csharp
IBuilding entrance = new SecurityGuard();

entrance.Enter("Alice");
Console.WriteLine();
entrance.Enter("David");
Console.WriteLine();
entrance.Enter("Bob");
```

**Output:**
```
Guard: Alice is requesting entry.
Guard: ID verified. Access granted.
Building: Alice has entered.

Guard: David is requesting entry.
Guard: David is not on the list. Access denied.

Guard: Bob is requesting entry.
Guard: ID verified. Access granted.
Building: Bob has entered.
```

> The client called `Enter()` on what it thought was the building. It was actually the security guard. The guard decided what happened. The building only ever saw the people who were allowed through. That is the Proxy.

### When to use it

- When you need **access control** — only letting certain callers through to the real object
- When you want to **add behaviour** such as logging, caching, or validation without changing the real object
- When the real object is **expensive to create** and you want to delay or guard that creation until it is truly needed

