// Company A needs a tech worker
RecruitmentAgency agency = new TechAgency();
IWorker worker = agency.HireWorker();
worker.DoWork();

// Company B needs a design worker
RecruitmentAgency agency2 = new DesignAgency();
IWorker worker2 = agency2.HireWorker();
worker2.DoWork();


// The worker interface — all workers can do a job
public interface IWorker
{
    void DoWork();
}

// The concrete workers
public class Developer : IWorker
{
    public void DoWork() => Console.WriteLine("Developer: Writing code.");
}

public class Designer : IWorker
{
    public void DoWork() => Console.WriteLine("Designer: Creating designs.");
}

// The base agency — declares the factory method
public abstract class RecruitmentAgency
{
    // This is the Factory Method — subclasses decide who to hire
    public abstract IWorker HireWorker();
}

// Concrete agencies — each one decides which worker to send
public class TechAgency : RecruitmentAgency
{
    public override IWorker HireWorker() => new Developer();
}

public class DesignAgency : RecruitmentAgency
{
    public override IWorker HireWorker() => new Designer();
}
