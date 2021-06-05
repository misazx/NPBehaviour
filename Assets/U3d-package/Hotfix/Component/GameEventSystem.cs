using System;
using System.Collections.Generic;
using ETModel;

namespace ETHotfix
{
    public class GameEventSystem: Component
    {
        private readonly Dictionary<string, LinkedList<IEvent>> m_AllEvents = new Dictionary<string, LinkedList<IEvent>>();

        /// <summary>
        /// 缓存的结点字典
        /// </summary>
        private readonly Dictionary<string, LinkedListNode<IEvent>> m_CachedNodes = new Dictionary<string, LinkedListNode<IEvent>>();

        /// <summary>
        /// 临时结点字典
        /// </summary>
        private readonly Dictionary<string, LinkedListNode<IEvent>> m_TempNodes = new Dictionary<string, LinkedListNode<IEvent>>();

        public void RegisterEvent(string eventId, IEvent e)
        {
            if (!this.m_AllEvents.ContainsKey(eventId))
            {
                this.m_AllEvents.Add(eventId, new LinkedList<IEvent>());
            }

            this.m_AllEvents[eventId].AddLast(e);
        }

        public void UnRegisterEvent(string eventId, IEvent e)
        {
            if (m_CachedNodes.Count > 0)
            {
                foreach (KeyValuePair<string, LinkedListNode<IEvent>> cachedNode in m_CachedNodes)
                {
                    //预防极端情况，比如两个不同的事件id订阅了同一个事件处理者
                    if (cachedNode.Value != null && cachedNode.Key == eventId && cachedNode.Value.Value == e)
                    {
                        //注意这里添加的Handler是下一个
                        m_TempNodes.Add(cachedNode.Key, cachedNode.Value.Next);
                    }
                }

                //把临时结点字典中的目标元素值更新到缓存结点字典
                if (m_TempNodes.Count > 0)
                {
                    foreach (KeyValuePair<string, LinkedListNode<IEvent>> cachedNode in m_TempNodes)
                    {
                        m_CachedNodes[cachedNode.Key] = cachedNode.Value;
                    }

                    //清除临时结点
                    m_TempNodes.Clear();
                }
            }

            if (this.m_AllEvents.ContainsKey(eventId))
            {
                this.m_AllEvents[eventId].Remove(e);
            }
        }

        public void Run(string type)
        {
            LinkedList<IEvent> iEvents;
            if (!this.m_AllEvents.TryGetValue(type, out iEvents))
            {
                return;
            }

            LinkedListNode<IEvent> temp = iEvents.First;

            while (temp != null)
            {
                try
                {
                    this.m_CachedNodes[type] = temp.Next;
                    temp.Value?.Handle();
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }

                temp = this.m_CachedNodes[type];
            }

            this.m_CachedNodes.Remove(type);
        }

        public void Run<A>(string type, A a)
        {
            LinkedList<IEvent> iEvents;
            if (!this.m_AllEvents.TryGetValue(type, out iEvents))
            {
                return;
            }

            LinkedListNode<IEvent> temp = iEvents.First;

            while (temp != null)
            {
                try
                {
                    this.m_CachedNodes[type] = temp.Next;
                    temp.Value?.Handle(a);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }

                temp = this.m_CachedNodes[type];
            }

            this.m_CachedNodes.Remove(type);
        }

        public void Run<A, B>(string type, A a, B b)
        {
            LinkedList<IEvent> iEvents;
            if (!this.m_AllEvents.TryGetValue(type, out iEvents))
            {
                return;
            }

            LinkedListNode<IEvent> temp = iEvents.First;

            while (temp != null)
            {
                try
                {
                    this.m_CachedNodes[type] = temp.Next;
                    temp.Value?.Handle(a, b);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }

                temp = this.m_CachedNodes[type];
            }

            this.m_CachedNodes.Remove(type);
        }

        public void Run<A, B, C>(string type, A a, B b, C c)
        {
            LinkedList<IEvent> iEvents;
            if (!this.m_AllEvents.TryGetValue(type, out iEvents))
            {
                return;
            }

            LinkedListNode<IEvent> temp = iEvents.First;

            while (temp != null)
            {
                try
                {
                    this.m_CachedNodes[type] = temp.Next;
                    temp.Value?.Handle(a, b, c);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }

                temp = this.m_CachedNodes[type];
            }

            this.m_CachedNodes.Remove(type);
        }
    }
}