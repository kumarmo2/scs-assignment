namespace InventoryManagement;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var store = Store.CreateStore(10);

        store.AddSupply(1, 20);
        store.AddDemand(1, 30);

        store.AddSupply(3, 4000);

        store.AddDemand(4, 200);

        Console.WriteLine(store.GetInventory(2));
        Console.WriteLine(store.GetInventory(3));
        Console.WriteLine(store.GetInventory(4));


        store.AddDemand(0, 2500);
        // store.AddSupply(0, 20);
        // store.AddDemand(3, 25);
        // store.AddDemand(2, 30);
        Console.WriteLine(store.GetInventory(1));
        Console.WriteLine(store.GetInventory(2));
        Console.WriteLine(store.GetInventory(3));
        Console.WriteLine(store.GetInventory(4));
        Console.WriteLine(store.GetInventory(6));
        Console.WriteLine(store.GetInventory(7));
        Console.WriteLine(store.GetInventory(9));
    }
}
