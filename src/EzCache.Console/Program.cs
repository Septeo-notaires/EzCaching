using EzCache.Cache;

public class Program
{
    private static readonly LruCache cache = new LruCache(10);
    public static void Main()
    {

        while (true) {
            Console.Clear();
            Console.WriteLine("Caching front");
            Console.WriteLine("1. Add in cache");
            Console.WriteLine("2. Get in cache");
            Console.WriteLine("3. quit");

            string? id = Console.ReadLine();

            Action act = id switch
            {
                "1" => AddInCache,
                "2" => GetInCache,
                "3" => Quit,
                _ => Quit
            };

            act();
        }
    }

    private static void AddInCache()
    {
        Console.Clear();
        Console.Write("Key : ");
        string? key = Console.ReadLine();
        Console.Write("Value : ");
        string? value = Console.ReadLine();

        cache.Add(key, value);
    }

    private static void GetInCache()
    {
        Console.Clear();
        Console.Write("Key : ");
        string? key = Console.ReadLine();

        string? val = cache.GetElement(key) as string;

        Console.WriteLine(val);
        _ = Console.ReadKey();
    }

    private static void Quit() =>
        Environment.Exit(0);
}