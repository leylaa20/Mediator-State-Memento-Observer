namespace StatePattern;

interface IConnection
{
    public void Open();
    public void Close();
    public void Log();
    public void Update();
}

class Accounting : IConnection
{
    public void Open() => Console.WriteLine("\nOpen database for Accounting");
    public void Close() => Console.WriteLine("Close the database");
    public void Log() => Console.WriteLine("Log activities");
    public void Update() => Console.WriteLine("Accounting has been updated");
}

class Sales : IConnection
{
    public void Open() => Console.WriteLine("\nOpen database for Sales");
    public void Close() => Console.WriteLine("Close the database");
    public void Log() => Console.WriteLine("Log activities");
    public void Update() => Console.WriteLine("Sales has been updated");
}

class Management : IConnection
{
    public void Open() => Console.WriteLine("\nOpen database for Management");
    public void Close() => Console.WriteLine("Close the database");
    public void Log() => Console.WriteLine("Log activities");
    public void Update() => Console.WriteLine("Management has been updated");
}

class Controller
{
    public static Accounting? account;
    public static Sales? sales;
    public static Management? management;

    private static IConnection? connection;

    public Controller()
    {
        account = new Accounting();
        sales = new Sales();
        management = new Management();
    }

    public void SetAccountingConnection() => connection = account;
    public void SetSalesConnection() => connection = sales;
    public void SetManagementConnection() => connection = management;

    public void Open() => connection?.Open();
    public void Close() => connection?.Close();
    public void Log() => connection?.Log();
    public void Update() => connection?.Update();
}

class Program
{
    static void Main(string[] args)
    {
        Controller controller = new Controller();
        int choice;

        while (true)
        {
            Console.Write("\n1. Management\n2. Sales\n3. Accounting\n4. Exit\nEnter choice: ");
            choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 1)
                controller.SetManagementConnection();

            if (choice == 2)
                controller.SetSalesConnection();

            if (choice == 3)
                controller.SetAccountingConnection();

            if (choice == 4)
                break;

            controller.Open();
            controller.Log();
            controller.Close();
            controller.Update();
        }
    }
}