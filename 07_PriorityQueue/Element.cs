namespace _07_PriorityQueue
{
    internal class Element<T>
    {
        public T Item { get; private set; }
        public int Priority { get; private set; }

        public Element(T item, int priority)
        {
            Item = item;
            Priority = priority;
        }
    }
}