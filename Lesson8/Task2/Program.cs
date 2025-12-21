using System.Text;

Console.OutputEncoding = Encoding.UTF8;

public delegate void Notifier(string message);

var source = new MessageSource();

source.OnNotify += NamedHandler;

source.OnNotify += delegate (string msg)
{
    Console.WriteLine("Anon: " + msg);
};

source.OnNotify += msg => Console.WriteLine("Lambda: " + msg);

source.Send("Перше повідомлення");
source.Send("Друге повідомлення");

static void NamedHandler(string message)
{
    Console.WriteLine("Named: " + message);
}

class MessageSource
{
    public event Notifier OnNotify;

    public void Send(string message)
    {
        if (OnNotify != null)
            OnNotify(message);
    }
}
