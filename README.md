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

Think of the **conductor of an orchestra**. An orchestra has one conductor. Every musician on stage looks to that same conductor for direction: when to start, when to stop, how fast to play, how loud to go. The conductor is the single point of authority that all musicians connect to and take decisions from. You cannot have two conductors standing at the front giving different instructions. That would cause chaos. No matter which musician needs guidance, they all reach the same one person.

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

### When NOT to use it

- When it makes your code hard to test because of hidden shared state
- When it is simply being used as a convenient global variable. That is not what it is for.

---

Factory Method
-------------------

### Real World Example

Think of a **recruitment agency**. A company calls the agency and says "we need a worker." The company does not go out and create the worker themselves. They just make the request. The agency decides which specific person to send: a developer, a designer, or a tester, depending on what the company needs. The company does not know or care exactly who is coming. They just know the person will be able to do the job.

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

### When to use it

- When your code should **not care about the exact type** of object it is working with, only that it fulfils a certain contract
- When you want to **add new types** without changing the code that uses them. Just add a new subclass.
- When the responsibility for **deciding what to create** belongs to a specific part of your system, not the caller

### When NOT to use it

- When there is only ever one type of object to create. A factory adds unnecessary complexity in that case.
- When the creation logic is simple enough that a direct `new` call is perfectly clear
