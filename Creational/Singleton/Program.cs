// Violinist asks for the conductor
OrchestraConductor violinist = OrchestraConductor.GetInstance();

// Pianist asks for the conductor
OrchestraConductor pianist = OrchestraConductor.GetInstance();

// Are they talking to the same conductor?
Console.WriteLine(object.ReferenceEquals(violinist, pianist));


violinist.SetTempo("Allegro");
pianist.Start();


// The conductor makes decisions by stop var 
  OrchestraConductor.GetInstance().Stop();


public class OrchestraConductor
{
    // Step 1: Hold the one instance here
    private static OrchestraConductor _instance;

    // Step 2: Private constructor — nobody outside can do: new OrchestraConductor()
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
    public void Start() => Console.WriteLine("Conductor: Begin playing.");
    public void Stop() => Console.WriteLine("Conductor: Stop playing.");
    public void SetTempo(string tempo) => Console.WriteLine($"Conductor: Tempo is now {tempo}.");
}
