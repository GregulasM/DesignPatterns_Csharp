namespace SingletonClass;

public sealed class Синглтон_класс
{
    private static Синглтон_класс _instance = null; 
    
    private static readonly object _syncRoot = new();

    private Синглтон_класс()
    {
        Имя_базы_данных = "database_lol.db";
        Адрес_сервера = "localhost";
        Логин = "admin";
        Пароль = "2281337";
    }
    
    public string Имя_базы_данных { get; set; }
    public string Адрес_сервера { get; set; }
    public string Логин { get; set; }
    public string Пароль { get; set; }

    public static Синглтон_класс GetInstance()
    {
        if (_instance == null)
        {
            lock (_syncRoot)
            {
                if (_instance == null)
                {
                    _instance = new Синглтон_класс();
                }
            }
        }
        return _instance;
    }

    public void Логирование(string message)
    {
        string сообщение_логгер = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
        Console.WriteLine(сообщение_логгер);
        
        
        // File.AppendAllText("log.txt", сообщение_логгер + Environment.NewLine);

    }
    
    
}




class Program
{
    static void Main(string[] args)
    {
        Task[] tasks = new Task[10];
        for (int i = 0; i < 10; i++)
        {
            int taskNumber = i;
            tasks[i] = Task.Run(() =>
                {
                    Синглтон_класс синглтонКласс = Синглтон_класс.GetInstance();
                    // синглтонКласс.Логирование($"Задача №{taskNumber}");
                    Console.WriteLine(
                        $"БД: {синглтонКласс.Имя_базы_данных}, " +
                        $"Сервер: {синглтонКласс.Адрес_сервера}, " +
                        $"Логин: {синглтонКласс.Логин}, " +
                        $"Пароль: {синглтонКласс.Пароль}" 
                    );
                    Thread.Sleep(100);
                }
                                        
            );
        }
        Task.WaitAll(tasks);
        // Console.WriteLine("Произошло Логирование");
        Console.ReadKey();
    }
}