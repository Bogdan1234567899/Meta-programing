using System;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

IPlugin[] plugins = new IPlugin[]
{
    new HelloPlugin(),
    new TimePlugin(),
    new ByePlugin()
};

for (int i = 0; i < plugins.Length; i++)
{
    plugins[i].Execute();
}

interface IPlugin
{
    void Execute();
}

class HelloPlugin : IPlugin
{
    public void Execute()
    {
        Console.WriteLine("HelloPlugin: Привіт!");
    }
}

class TimePlugin : IPlugin
{
    public void Execute()
    {
        Console.WriteLine("TimePlugin: " + DateTime.Now);
    }
}

class ByePlugin : IPlugin
{
    public void Execute()
    {
        Console.WriteLine("ByePlugin: Бувай!");
    }
}
