using System;
using System.Collections.Generic;

// Фабрика плагінів
// Потрібна щоб Main не створював плагіни сам через new
// Можна підмінити фабрику і отримати інший набір плагінів
public interface IPluginFactory
{
    List<IPlugin> Create(string mode);
}

// Шина подій
// Потрібна щоб плагіни не виводили все напряму в консоль
// Хто хоче — підписується на подію і реагує як йому треба
public interface IEventBus
{
    void Subscribe(string eventName, Action<object> handler);
    void Publish(string eventName, object payload);
}
