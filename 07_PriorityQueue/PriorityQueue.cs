namespace _07_PriorityQueue
{
    public class PriorityQueue<T>
    {
        Element<T>[] _items;

        public PriorityQueue()
        {
            _items = [];
        }

        public void Enqueue(T element, int priority)
        {
            Array.Resize(ref _items, Count() + 1);
            _items[^1] = new Element<T>(element, priority);
            _items = _items.OrderByDescending(i => i.Priority).ToArray();
        }

        public T Dequeue()
        {
            if (Count() == 0)
                throw new InvalidOperationException("PriorityQueue is empty");

            T output = _items[0].Item;

            if (Count() > 1)
            {
                Element<T>[] newItems = new Element<T>[Count() - 1];
                _items[1..].CopyTo(newItems, 0);
                _items = newItems;
            }
            else
            {
                _items = [];
            }

            return output;
        }

        public int Count()
        {
            return _items.Length;
        }
    }
}