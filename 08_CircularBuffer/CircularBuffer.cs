namespace _08_CircularBuffer
{
    public class CircularBuffer<T>
    {
        private readonly int _size;
        private Queue<T> _elements;

        public bool SilentReplacement { get; set; } = true;

        public CircularBuffer(int size)
        {
            if (size <= 0)
                throw new ArgumentException("Invalid size - has to be positive integer");

            _size = size;
            _elements = new Queue<T>();
        }

        public void Add(T element)
        {
            if (Count() == Size())
            {
                if (!SilentReplacement)
                    throw new InvalidOperationException("Circular Buffer is full and replacement not allowed");

                _elements.Dequeue();
            }

            _elements.Enqueue(element);
        }

        public int Count() => _elements.Count;

        public int Size() => _size;

        public T Take()
        {
            try
            {
                return _elements.Dequeue();
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException("Circular Buffer is empty");
            }
        }
    }
}