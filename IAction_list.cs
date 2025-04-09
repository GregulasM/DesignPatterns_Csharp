namespace DesignPatterns_Csharp;

public interface IAction_list
{
    public bool Создать_файл(string? путь_файла, string название_файла);
    public bool Удалить_файл(string название_файла);
    public bool Редактировать_файл(string название_файла, string новое_название_файла);
    public bool Копировать_вставить_файл(string название_файла, string путь_файла);

}