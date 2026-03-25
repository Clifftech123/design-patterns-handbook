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

// Ask the whole company, one call rolls up everything
Console.WriteLine("=== Full Company ===");
company.GetDetails();

// Ask just one department, same call, same interface
Console.WriteLine("\n=== Engineering Only ===");
engineering.GetDetails();

// Ask a single employee, same call, same interface
Console.WriteLine("\n=== Single Employee ===");
dev1.GetDetails();


// The component interface, every leaf and composite shares this contract
public interface IEmployee
{
    string Name    { get; }
    int    GetSalary();
    void   GetDetails(string indent = "");
}

// The leaf, a single employee with no reports
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

// The composite, a department that holds employees or other departments
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
