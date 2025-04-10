namespace DesignPatterns_Csharp;

class Program
{
    static void Main(string[] args)
    {
        List<string> папки = new List<string>();

        string папки_для_добавления =
            "dp_abstract_factory dp_adapter dp_bridge dp_chain dp_composite dp_decorator dp_factory dp_observer dp_proxy dp_singleton dp_strategy";
        
        string[] обработанные_папки = папки_для_добавления.Split(" ");

        foreach (var папка in обработанные_папки)
        {
            Directory.CreateDirectory($"D:\\Vasnie Progi\\C# projects\\DesignPatterns_Csharp\\{папка}");
        }
    }
}