namespace DesignPatterns_Csharp;

public interface IFile_system_item
{
    public string Название { get; set; }
    public void Узнать_размер();
    public string Путь { get; }
    void Информация();
    void Отобразить(int вложенность = 0);

    public int Количество_файлов();
}