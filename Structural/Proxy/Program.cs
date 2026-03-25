IBuilding entrance = new SecurityGuard();

entrance.Enter("Alice");
Console.WriteLine();
entrance.Enter("David");
Console.WriteLine();
entrance.Enter("Bob");


// The subject interface
public interface IBuilding
{
    void Enter(string visitorName);
}

// The real subject, the actual building
public class OfficeBuilding : IBuilding
{
    public void Enter(string visitorName)
    {
        Console.WriteLine($"Building: {visitorName} has entered.");
    }
}

// The proxy, the security guard controls who gets through
public class SecurityGuard : IBuilding
{
    private readonly OfficeBuilding _building           = new();
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
