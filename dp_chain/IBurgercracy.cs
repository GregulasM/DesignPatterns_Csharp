namespace DesignPatterns_Csharp;

public interface IBurgercracy
{
    IBurgercracy Передача_документа(IBurgercracy операция);
    void Обработчик_документа(Документ документ);
}