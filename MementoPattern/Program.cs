namespace MementoPattern;

public class LEDTV
{
    public string Size { get; set; }
    public string Price { get; set; }
    public bool USBSupport { get; set; }

    public LEDTV(string Size, string Price, bool USBSupport)
    {
        this.Size = Size;
        this.Price = Price;
        this.USBSupport = USBSupport;
    }

    public string GetDetails() => "LED TV Size = " + Size + ", Price = " + Price + ", USBSupport = " + USBSupport;
}

public class Memento
{
    public LEDTV ledTV { get; set; }
    public Memento(LEDTV ledTV)
    {
        this.ledTV = ledTV;
    }

    public string GetDetails() => "Memento LED TV: " + ledTV.GetDetails();
}

public class Caretaker
{
    private List<Memento> ledTvList = new List<Memento>();

    public void AddMemento(Memento m)
    {
        ledTvList.Add(m);
        Console.WriteLine(m.GetDetails());
    }

    public Memento GetMemento(int index) => ledTvList[index];
}

public class Originator
{
    public LEDTV? ledTV;

    public Memento CreateMemento() => new Memento(ledTV);

    public void SetMemento(Memento memento) => ledTV = memento.ledTV;

    public string GetDetails() => "Originator ledTV: " + ledTV.GetDetails();
}


class Program
{
    static void Main(string[] args)
    {
        Originator originator = new Originator();
        Caretaker caretaker = new Caretaker();

        originator.ledTV = new LEDTV("42 inch", "60000Rs", false);
        caretaker.AddMemento(originator.CreateMemento());

        originator.ledTV = new LEDTV("46 inch", "80000Rs", true);
        caretaker.AddMemento(originator.CreateMemento());

        originator.ledTV = new LEDTV("50 inch", "100000Rs", true);

        Console.WriteLine("\nOrignator current state : " + originator.GetDetails());
        Console.WriteLine("\nOriginator restoring");

        originator.ledTV = caretaker.GetMemento(0).ledTV;

        Console.WriteLine("\nOrignator current state : " + originator.GetDetails());
    }
}