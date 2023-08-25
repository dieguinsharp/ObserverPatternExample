var promotion = new PromotionNotifyer();

var customer1 = new Customer("Diego");
var customer2 = new Customer("Luffy");
var customer3 = new Customer("Devedor");

promotion.Subscribe(customer1);
promotion.Subscribe(customer2);
promotion.Subscribe(customer3);

Console.WriteLine("Promotion of day:");
Console.WriteLine();

promotion.SetNewPromotion(new PromotionItem("GPU 3090", 3000, 2500));
promotion.SetNewPromotion(new PromotionItem("GPU 4090", 4000, 3500));

promotion.UnSubscribe(customer3);

Console.WriteLine("Promotion of day:");
Console.WriteLine();

promotion.SetNewPromotion(new PromotionItem("GPU 3090", 2000, 1500));
promotion.SetNewPromotion(new PromotionItem("GPU 4090", 3000, 2500));

public interface IObserver
{
    void Update(PromotionItem subject);
}

public interface ISubject
{
    void Subscribe(IObserver observer);
    void UnSubscribe(IObserver observer);
    void Notify(PromotionItem item);
}

public class PromotionNotifyer : ISubject
{
    private readonly List<IObserver> _observers = new List<IObserver>();

    public void Notify(PromotionItem item)
    {
        foreach(var observer in _observers)
            observer.Update(item);
    }

    public void Subscribe(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void UnSubscribe(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void SetNewPromotion(PromotionItem item)
    {
        this.Notify(item);
    }
}

public class Customer : IObserver
{
    private readonly string _name;
    public Customer(string name)
    {
        _name = name;
    }
    public void Update(PromotionItem item)
    {
        Console.WriteLine(string.Concat("New Promotion for you, ", _name, "!"));
        Console.WriteLine(item.ToString());

        Console.WriteLine();
    }
}

public class PromotionItem
{
    public string Name { get; set; }
    public double NewPrice { get; set; }
    public double OldPrice { get; set; }

    public PromotionItem(string name, double oldPrice, double newPrice)
    {
        Name = name;
        NewPrice = newPrice;
        OldPrice = oldPrice;
    }

    public override string ToString()
    {
        return string.Concat(Name, ": ", OldPrice, "$", " for ", NewPrice, "$");
    }
}