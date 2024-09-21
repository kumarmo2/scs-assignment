namespace InventoryManagement;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var store = Store.CreateStore(10);
        Console.WriteLine(store._tree is null);
        Console.WriteLine(store._tree.GetHeight());
        store.PrintInorderTree();
        // Console.WriteLine(store.Height);
    }
}
