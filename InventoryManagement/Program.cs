namespace InventoryManagement;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var store = Store.CreateStore(10);
        store.AddSupply(2, 50);
        store.AddDemand(8, 25);
        store.AddDemand(4, 10);
        Console.WriteLine(store.GetInventory(3));
        Console.WriteLine(store.GetInventory(4));
        Console.WriteLine(store.GetInventory(8));
        Console.WriteLine(store.GetInventory(6));
        Console.WriteLine(store.GetInventory(1));
    }
}
