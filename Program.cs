﻿namespace DesignPatterns_Csharp;

// ------ Паттерн Chain of Responsibility ------ //
//
// Для формирования цепочки тех. поддержки или всяких проверок.
// Полезный, комплексный и интересный паттерн.
// К сожалению, слишком сонный, чтобы делать хоть сколько-то полезное решение, поэтому делаю минимум.
// «IBurgercracy» предоставляет два метода для обработки документа и вызова другого обработчика. «» «» «»
// Класс «Документ» позволяет создать свой документ с текстом. 
// «Проверка_символы», «Проверка_пробелы», «Проверка_итог» - это классы проверок, обработчики документа, каждый со своей задачей и дальнейшей передачей.
//
// ------------------------------------------------ //

class Program
{
    static void Main(string[] args)
    {
        IBurgercracy проверка_символы = new Проверка_символы();
        IBurgercracy проверка_пробелы = new Проверка_пробелы();
        IBurgercracy проверка_итог = new Проверка_итог();
        
        проверка_символы.Передача_документа(проверка_пробелы);
        проверка_пробелы.Передача_документа(проверка_итог);

        Документ документ = new Документ("Сто-");
        проверка_символы.Обработчик_документа(документ);
        
    }
}

public class Документ
{
    private string _текст_документ;

    public string Текст_документа
    {
        get
        {
            return _текст_документ; 
            
        }
        set
        {
            _текст_документ = value; 
            
        }
    }

    public Документ(string текст_документ)
    {
        _текст_документ = текст_документ;
    }
}

class Проверка_символы : IBurgercracy
{
    private IBurgercracy _документооборот_операция;

    public IBurgercracy Передача_документа(IBurgercracy операция)
    {
        _документооборот_операция = операция;
        return this;
    }

    public void Обработчик_документа(Документ документ)
    {
        bool пройден = true;
        foreach (var символ in документ.Текст_документа)
        {
            if (символ == '-')
            {
                Console.WriteLine("Проверка на СТО не пройдена: найден символ '-'");
                пройден = false;
                break;
            }
        }
        if (пройден)
        {
            _документооборот_операция?.Обработчик_документа(документ);
        }
    }
}

class Проверка_пробелы : IBurgercracy
{
    private IBurgercracy _документооборот_операция;

    public IBurgercracy Передача_документа(IBurgercracy операция)
    {
        _документооборот_операция = операция;
        return this;
    }

    public void Обработчик_документа(Документ документ)
    {
        bool пройден = true;
        foreach (var символ in документ.Текст_документа)
        {
            if (символ == ' ')
            {
                Console.WriteLine("Проверка на СТО не пройдена: найден пробел");
                пройден = false;
                break;
            }
        }
        if (пройден)
        {
            _документооборот_операция?.Обработчик_документа(документ);
        }
    }
}

class Проверка_итог : IBurgercracy
{
    private IBurgercracy _документооборот_операция;

    public IBurgercracy Передача_документа(IBurgercracy операция)
    {
        _документооборот_операция = операция;
        return this;
    }

    public void Обработчик_документа(Документ документ)
    {
        bool пройден = true;
        foreach (var символ in документ.Текст_документа)
        {
            if (!char.IsUpper(символ))
            {
                Console.WriteLine("Проверка на СТО не пройдена: не все символы в верхнем регистре");
                Console.WriteLine("ПЕРЕДЕЛЫВАЙ!");
                пройден = false;
                break;
            }
        }
        if (пройден)
        {
            Console.WriteLine("Все проверки пройдены успешно!");
        }
    }
}
