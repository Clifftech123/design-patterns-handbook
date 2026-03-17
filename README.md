<p align="center">
  <strong>Design Patterns in C#</strong> — A complete breakdown of all classic design patterns, implemented and explained in C#. While the code examples use C#, the concepts are universal and apply to any programming language.
</p>

---

Introduction
============

Design patterns are **reusable solutions to common problems in software design**. Think of them as blueprints — not finished code, but proven templates you can adapt to solve a specific problem in your own codebase.

> *"In software engineering, a software design pattern is a general, reusable solution to a commonly occurring problem within a given context in software design. It is not a finished design that can be transformed directly into source or machine code. It is a description or template for how to solve a problem that can be used in many different situations."*
>
> **Source:** [Wikipedia - Software design pattern](https://en.wikipedia.org/wiki/Software_design_pattern)

### Things to keep in mind

- **Design patterns are not code.** They are a way of *thinking* about how to structure your code. They are a tool — not a silver bullet — for solving specific design problems.

- **They are not tied to any language or library.** The examples here are in C#, but the same patterns can be implemented in Python, Java, Go, TypeScript, or any other language.

- **There is no one-size-fits-all pattern.** Each pattern exists to address a particular kind of problem. Understanding *what problem a pattern solves* is more important than memorising the implementation.

> Although this repository implements all patterns in C#, the underlying concepts are completely language-agnostic. If you program in another language, you can follow along just fine — only the syntax will differ.

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

> Creational patterns are all about **how objects are created**. They can be divided into class-creation patterns — which use inheritance to decide which class to instantiate — and object-creation patterns, which use delegation to get the job done.

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

* [Factory Method](#factory-method)
* [Abstract Factory](#abstract-factory)
* [Builder](#builder)
* [Prototype](#prototype)
* [Singleton](#singleton)
