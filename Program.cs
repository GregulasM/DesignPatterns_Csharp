namespace DesignPatterns_Csharp;

// ------ Паттерн Bridge ------ //
//
// Довольно сложный, комплексный паттерн. Сферы применения можно спокойно найти, но для этого нужно понимать, что где и как делать.
// Имеется интерфейс «IMessage_sender» с отправкой и проверкой сообщения. Проверка работает, но я ее тут не использую.
// Имеется абстрактный класс с методом для отправки сообщение и связкой с «IMessage_sender».
// Классы от «IMessage_sender» отправляют сообщения в разные приложения. Классы от «Тип_сообщения» добавляют к сообщениям всякое.
//
// ------------------------------ //

class Program
{
    static void Main(string[] args)
    {
        IMessage_sender консольное_сообщение = new Консольное_сообщение();
        IMessage_sender телеграм_сообщение = new Telegram_сообщение();
        IMessage_sender почтовое_сообщение = new Email_сообщение();

        Тип_сообщения сообщение_админ_1 = new Сообщение_админ(консольное_сообщение);
        Тип_сообщения сообщение_админ_2 = new Сообщение_админ(телеграм_сообщение);
        Тип_сообщения сообщения_гость_1 = new Сообщение_гость(почтовое_сообщение);
        
        сообщение_админ_1.Текст = "Лол";
        сообщение_админ_1.Отправить_сообщение();
        
        
        сообщение_админ_2.Текст = "Круглый стол";
        сообщение_админ_2.Отправить_сообщение();
        
        сообщения_гость_1.Текст = "Ваши союзники";
        сообщения_гость_1.Отправить_сообщение();
        
    }
}

class Консольное_сообщение : IMessage_sender
{
    public void Отправить(string сообщение)
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        
        Console.WriteLine("Ого, сообщение в консоль!");
        Console.WriteLine(сообщение);
    }

    public bool Проверить(string сообщение)
    {
        if (сообщение.Contains("Кал") || сообщение.Contains("кал"))
        {
            Console.WriteLine("Сообщение кал.");
            return true;
        }

        Console.WriteLine("Все хорошо.");

        return false;
    }
}

class Telegram_сообщение : IMessage_sender
{
    public void Отправить(string сообщение)
    {
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Представь, что сообщение отправлено в бот/группу Telegram!");
        Console.WriteLine(сообщение);
    }

    public bool Проверить(string сообщение)
    {
        if (сообщение.Contains("Кал") || сообщение.Contains("кал"))
        {
            Console.WriteLine("Сообщение кал.");
            return true;
        }

        Console.WriteLine("Все хорошо.");

        return false;
    }
}

class Email_сообщение : IMessage_sender
{
    public void Отправить(string сообщение)
    {
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Представь, что сообщение отправлено на почту Email!");
        Console.WriteLine(сообщение);
    }

    public bool Проверить(string сообщение)
    {
        if (сообщение.Contains("Кал") || сообщение.Contains("кал"))
        {
            Console.WriteLine("Сообщение кал.");
            return true;
        }

        Console.WriteLine("Все хорошо.");

        return false;
    }
}


abstract class Тип_сообщения
{
    protected IMessage_sender _отправщик_сообщений;
    
    public string Текст { get; set; }

    public Тип_сообщения(IMessage_sender отправщик_сообщений)
    {
        _отправщик_сообщений = отправщик_сообщений;
    }

    public abstract void Отправить_сообщение();

}

class Сообщение_админ : Тип_сообщения
{
    public Сообщение_админ(IMessage_sender отправщик_сообщений) : base(отправщик_сообщений)
    {
    }

    public override void Отправить_сообщение()
    {
        Console.WriteLine("Внимание: Говорит Админ!");
        string сообщение_админ = "Великий владыка: " + this.Текст;
        _отправщик_сообщений.Отправить(сообщение_админ);
    }
}

class Сообщение_гость : Тип_сообщения
{
    public Сообщение_гость(IMessage_sender отправщик_сообщений) : base(отправщик_сообщений)
    {
    }

    public override void Отправить_сообщение()
    {
        Console.WriteLine("Пользователь-гость");
        string сообщение_гость = "Гость: " + this.Текст;
        _отправщик_сообщений.Отправить(сообщение_гость);
    }
}

// class Сообщение_премиум : Тип_сообщения
// {
//     
// }