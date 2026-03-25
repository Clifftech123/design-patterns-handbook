Forest forest = new Forest();

// Plant 6 trees — but only 2 unique types
forest.PlantTree(1,  5,  "Oak",  "Dark Green",  "Rough bark");
forest.PlantTree(3,  12, "Oak",  "Dark Green",  "Rough bark");
forest.PlantTree(7,  2,  "Oak",  "Dark Green",  "Rough bark");
forest.PlantTree(10, 8,  "Pine", "Light Green", "Smooth bark");
forest.PlantTree(15, 3,  "Pine", "Light Green", "Smooth bark");
forest.PlantTree(20, 14, "Pine", "Light Green", "Smooth bark");

forest.Render();

Console.WriteLine($"\nTrees planted:               {forest.TreeCount}");
Console.WriteLine($"Unique tree types in memory: {forest.TreeTypeCount}");


// The flyweight, holds shared intrinsic state
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

// The flyweight factory, creates and caches tree types so they are never duplicated
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

// The context, holds unique position and a reference to a shared flyweight
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

// The forest, plants trees using shared flyweights
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
