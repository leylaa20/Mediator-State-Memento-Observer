namespace ObserverPattern;

public class Application
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Application(int id, string name)
    {
        Id = id;
        Name = name;
    }
}


public class ApplicationsHandler : IObservable<Application>
{
    private readonly List<IObserver<Application>> _observers;
    public List<Application> Applications { get; set; }

    public ApplicationsHandler()
    {
        _observers = new(); //notification gonderilecek olanlarin listi
        Applications = new();
    }

    public IDisposable Subscribe(IObserver<Application> observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);

            foreach (var item in Applications)
                observer.OnNext(item);
        }

        return new Unsubscriber(_observers, observer);
    }

    public void AddApplication(Application app)
    {
        Applications.Add(app);

        foreach (var observer in _observers)
            observer.OnNext(app);
    }

    public void CloseApplications()
    {
        foreach (var observer in _observers)
            observer.OnCompleted();

        _observers.Clear();
    }
}

public class Unsubscriber : IDisposable
{
    private readonly List<IObserver<Application>> _observers;
    private readonly IObserver<Application> _observer;

    public Unsubscriber(List<IObserver<Application>> observers, IObserver<Application> observer)
    {
        _observers = observers;
        _observer = observer;
    }

    public void Dispose()
    {
        if (_observers.Contains(_observer))
            _observers.Remove(_observer);
    }
}

public class HRSpecialist : IObserver<Application>
{
    public string Name { get; set; }
    public List<Application> Applications { get; set; }
    private IDisposable _cancellation;

    public HRSpecialist(string name)
    {
        Name = name;
        Applications = new();
    }

    public void ListApplications()
    {
        if (Applications.Any())
            foreach (var app in Applications)
                Console.WriteLine($"{Name} {app.Name} has just applied for job no {app.Id}");
        else
            Console.WriteLine($"{Name} No applications yet.");
    }


    public virtual void Subscribe(ApplicationsHandler provider) => _cancellation = provider.Subscribe(this);

    public virtual void Unsubscribe()
    {
        _cancellation.Dispose();
        Applications.Clear();
    }

    // yeni notification- larin olmayacagini bildirir
    public void OnCompleted() => Console.WriteLine($"{Name} We are not accepting any more applications");

    public void OnError(Exception error) { }

    // notification qebul edir
    public void OnNext(Application value) => Applications.Add(value);

}


class Program
{
    static void Main(string[] args)
    {
        var observer1 = new HRSpecialist("Bill");
        var observer2 = new HRSpecialist("John");
        var provider = new ApplicationsHandler();

        observer1.Subscribe(provider);
        observer2.Subscribe(provider);

        provider.AddApplication(new(1, "Jesus"));
        provider.AddApplication(new(2, "Dave"));

        observer1.ListApplications();
        observer2.ListApplications();
        observer1.Unsubscribe();

        Console.WriteLine();
        Console.WriteLine($"{observer1.Name} unsubscribed");
        Console.WriteLine();

        provider.AddApplication(new(3, "Sofia"));
        observer1.ListApplications();
        observer2.ListApplications();

        Console.WriteLine();
        provider.CloseApplications();
    }
}