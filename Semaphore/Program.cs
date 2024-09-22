namespace Semaphore;



public class Semaphore
{
    private object _mutex = new();
    private int _availableTasks;
    private readonly int _capacity;

    public Semaphore(int capacity)
    {
        if (capacity < 1)
        {
            throw new ArgumentException($"capacity must be greater than 0", nameof(capacity));
        }
        _availableTasks = capacity;
        _capacity = capacity;
    }


    public void Wait()
    {
        lock (_mutex)
        {
            if (_availableTasks == 0)
            {
                Console.WriteLine(".... waiting ");
                Monitor.Wait(_mutex);
                Console.WriteLine(".... released ");
            }
            _availableTasks--;
        }
    }

    public void Signal()
    {
        lock (_mutex)
        {
            if (_availableTasks == _capacity)
            {
                // noop
                return;
            }
            Monitor.Pulse(_mutex);
            _availableTasks++;
        }
    }


}



class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");


        var semaphore = new Semaphore(3);
        var tasks = new List<Task>();
        var count = 0;

        for (var i = 0; i < 10; i++)
        {
            var x = i;

            tasks.Add(Task.Run(() =>
            {
                try
                {
                    semaphore.Wait();
                    Console.WriteLine($"delaying x: {x}");
                    count++;
                    Thread.Sleep(2000);
                    Console.WriteLine($"released delaying, x: {x}");
                }
                finally
                {
                    semaphore.Signal();
                }


            }));
        }
        Task.WaitAll(tasks.ToArray());
        Console.WriteLine($"count: {count}");
    }
}
