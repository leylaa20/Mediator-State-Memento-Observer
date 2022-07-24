namespace MediatorPattern;

public interface FacebookMediator
{
    void SendMessage(string message, User user);
    void RegisterUser(User user);
}

public class ConcreteFacebookMediator : FacebookMediator
{
    private List<User> usersList = new List<User>();

    public void RegisterUser(User user) => usersList.Add(user);

    public void SendMessage(string message, User user)
    {
        foreach (var u in usersList)
            if (u != user) // mesaji gonderene oz atdigi mesaj gonderilmemelidir
                u.Receive(message);
    }
}


public abstract class User
{
    protected FacebookMediator mediator;
    protected string name;

    public User(FacebookMediator mediator, string name)
    {
        this.mediator = mediator;
        this.name = name;
    }

    public abstract void Send(string message);
    public abstract void Receive(string message);
}


public class ConcreteUser : User
{
    public ConcreteUser(FacebookMediator mediator, string name)
        : base(mediator, name) { }

    public override void Receive(string message) => Console.WriteLine(this.name + ": Received Message: " + message);

    public override void Send(string message)
    {
        Console.WriteLine(this.name + ": Sending Message = " + message + "\n");
        mediator.SendMessage(message, this);
    }
}


class Program
{
    static void Main(string[] args)
    {
        FacebookMediator facebookMediator = new ConcreteFacebookMediator();

        User Aris = new ConcreteUser(facebookMediator, "Aris");
        User Dave = new ConcreteUser(facebookMediator, "Dave");
        User Smith = new ConcreteUser(facebookMediator, "Smith");
        User Asel = new ConcreteUser(facebookMediator, "Asel");
        User John = new ConcreteUser(facebookMediator, "John");

        facebookMediator.RegisterUser(Aris);
        facebookMediator.RegisterUser(Dave);
        facebookMediator.RegisterUser(Smith);
        facebookMediator.RegisterUser(Asel);
        facebookMediator.RegisterUser(John);

        Dave.Send("Hello world!");
        Console.WriteLine();
        Asel.Send("Mediator Pattern");
    }
}