namespace DesignPatterns_Csharp;

public interface IMessage_sender
{
    void Отправить(string сообщение);
    bool Проверить(string сообщение);
}