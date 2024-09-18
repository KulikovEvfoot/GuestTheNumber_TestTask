using System;
using System.Collections.Generic;

namespace Common
{
    public class EventProducer<T> : IEventProducer<T>, IDisposable
    {
        private readonly List<T> m_Observers = new List<T>();
        public int ObserversCount => m_Observers.Count;

        public void Attach(T observer)
        {
            if (m_Observers.Contains(observer))
            {
                return;
            }
            
            m_Observers.Add(observer);
        }

        public void Detach(T observer)
        {
            m_Observers.Remove(observer);
        }
        
        public void NotifyAll(Action<T> notification)
        {
            for (var index = m_Observers.Count - 1; index >= 0; index--)
            {
                notification?.Invoke(m_Observers[index]);
            }
        }

        public void Dispose()
        {
            m_Observers.Clear();
        }
    }
}