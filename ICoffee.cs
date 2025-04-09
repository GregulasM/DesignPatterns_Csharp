namespace DesignPatterns_Csharp;

public interface ICoffee
{
    float Количество { get; }
    string Описание { get;}
    
    string Состав { get; }
    decimal Стоимость();
}