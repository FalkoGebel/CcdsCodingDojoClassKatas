namespace _05_LinkedList
{
    public class Element<T>(T item)
    {
        public T Item { get; set; } = item;

        public Element<T>? Next { get; set; }
    }
}