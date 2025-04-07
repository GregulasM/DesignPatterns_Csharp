

using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Serilog;

namespace DesignPatterns_Csharp;

// ------ Описание работы ------ //

// IJson_file_converter - это целевой интерфейс с методами, которые потом реализуются в адаптере.
// Адаптируемый - это легаси библиотека, приложение, класс, тоси боси
// Адаптер - специальный класс, который работает с нашей старой библиотекой (тоси боси Адаптируемый).
// «Адаптируемый» смотрит текстовые файлы в папке, читает их, берет их заголовки (название файла) и текст и вставляет в словарь «Dictionary<string, string> файлы».
// «Адаптер», наследуя «IJson_file_converter» с двумя методами, читает данные из словаря «Dictionary<string, string> файлы», заголовок (которое название файла) и текст превращает в json по паттерну "название":string, "текст": string
// Еще есть логирование, чтоб удобнее было смотреть, что скушалось в словарь или переменную

// ------------------------------ //





class Program
{
    static void Main(string[] args)
    {
        // ------ Логирование! ------ //
        
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        
        // ------------------------------ //
        
        // ------ Основной код программы ------ //
        
        Адаптируемый адаптируемый = new Адаптируемый();
        
        адаптируемый.Чтение_сохранение_папки_txt();

        адаптируемый.Чтение_вывод_папки_txt_консоль();

        Адаптер адаптер = new Адаптер(адаптируемый);

        адаптер.Конвертер_файла_в_json();

        адаптер.Чтение_файла_json();

        // ------------------------------ //

    }
    
    
    
}


public class Адаптируемый
{
    public Dictionary<string, string> файлы = new Dictionary<string, string> { };
    public void Чтение_сохранение_папки_txt()
    {
        Log.Warning("Работает метод Чтение_сохранение_папки_txt()");
        foreach (var file in Directory.GetFiles("text_files", "*.txt"))
        {
            string только_название = Path.GetFileNameWithoutExtension(file);
            файлы.Add(только_название,File.ReadAllText(file));
        }
        Log.Information("Содержимое списка «Dictionary<string, string> файлы»:\n{@файлы}", файлы);
        
    }
    
    public void Чтение_вывод_папки_txt_консоль()
    {
        Log.Warning("Работает метод Чтение_вывод_папки_txt_консоль()");
        foreach (var file in Directory.GetFiles("text_files", "*.txt"))
        {
            Console.WriteLine(File.ReadAllText(file));
        }
        
    }
    
}

class Адаптер : IJson_file_converter
{
    private readonly Адаптируемый _адаптируемый;

    public Адаптер(Адаптируемый адаптируемый)
    {
        _адаптируемый = адаптируемый;
    }
    public void Конвертер_файла_в_json()
    {
        Log.Warning("Работает метод Конвертер_файла_в_json()");
        var правила_json = new JsonSerializerOptions
        {
            WriteIndented = true, // для красивого json, а не в одну строку
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) // без этого русский текст превращается в unicode!
        };
        
        if (!Directory.Exists("json_converter_folder/"))
        {
            Directory.CreateDirectory("json_converter_folder/");
            Log.Information("Создана директории «json_converter_folder»!");
        }
        else
        {
            Log.Information("Проверка директории «json_converter_folder» успешна!");
        }
        
        foreach (var контент in _адаптируемый.файлы)
        {
            string заголовок = контент.Key;
            string текст = контент.Value;
            Log.Information("\nТекущий заголовок:\n{@заголовок}\nТекущий текст:\n{@текст}", заголовок,текст);
            
            Шаблон_файла_json шаблон_файла_json = new Шаблон_файла_json
            {
                Название = заголовок,
                Текст = текст
            };

            string json_файл_текст = JsonSerializer.Serialize(шаблон_файла_json, правила_json);
            File.WriteAllText($"json_converter_folder/{заголовок}.json", json_файл_текст);
            Log.Information("Внутренности json: {@json_файл_текст}",json_файл_текст);
            // File.AppendAllText($"json_converter_folder/{заголовок}.json",текст);
        }

        // string заголовок = _адаптируемый.файлы.Keys.ToList()[0];
        // string текст = _адаптируемый.файлы.Values.ToList()[0];
        
        // Log.Information("Содержимое заголовок:\n{@заголовок}", заголовок);
        // Log.Information("Содержимое текст:\n{@текст}", текст);
        //
        // var правила_json = new JsonSerializerOptions
        // {
        //     WriteIndented = true,
        //     PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        //     Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        // };
        //
        // Шаблон_файла_json шаблон_файла_json = new Шаблон_файла_json
        // {
        //     Название = заголовок,
        //     Текст = текст
        // };
        //
        // if (!Directory.Exists("json_converter_folder/"))
        // {
        //     Directory.CreateDirectory("json_converter_folder/");
        // }
        //
        // string json_файл_текст = JsonSerializer.Serialize(шаблон_файла_json, правила_json);
        // File.WriteAllText($"json_converter_folder/{заголовок}.json", json_файл_текст);
        // // File.AppendAllText($"json_converter_folder/{заголовок}.json",текст);
    }

    public void Чтение_файла_json()
    {
        Log.Warning("Работает метод Чтение_файла_json()");
        var правила_json = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };
        
        if (Directory.Exists("json_converter_folder/"))
        {
            Log.Information("Проверка директории «json_converter_folder» успешна!");
            foreach (string file in Directory.GetFiles("json_converter_folder/"))
            {
                Log.Information("Путь до файла: {@file}",file);
                Console.WriteLine(Path.GetFileNameWithoutExtension(file));
                // File.ReadAllText($"json_converter_folder/", file);
                // string заголовок = контент.Key;
                // string текст = контент.Value;
                //
                // Log.Information("Содержимое заголовок:\n{@заголовок}", заголовок);
                // Log.Information("Содержимое текст:\n{@текст}", текст);
            
                // Шаблон_файла_json шаблон_файла_json = new Шаблон_файла_json
                // {
                //     Название = заголовок,
                //     Текст = текст
                // };
                string содержимое_файла = File.ReadAllText(file);
                Log.Information("Содержимое файла: {@содержимое_файла}",содержимое_файла);

                var json_файл_текст = JsonSerializer.Deserialize<Шаблон_файла_json>(содержимое_файла, правила_json);
                Console.WriteLine(json_файл_текст.Текст);
                Log.Information("Содержимое json: {@json_файл_текст}",json_файл_текст);
                
                // File.AppendAllText($"json_converter_folder/{заголовок}.json",текст);
            }
        }
        else
        {
            Console.WriteLine("Не найдена директория «json_converter_folder»");
            Log.Error("Директория «json_converter_folder» отсутствует!");
        }
        
        
    }

    class Шаблон_файла_json
    {
        public string Название { get; set; }
        public string Текст { get; set; }
        
    }
}