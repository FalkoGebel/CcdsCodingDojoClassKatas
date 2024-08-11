namespace _01_BoundedQueue
{
    public class BoundedQueue<T>
    {
        private const int _sleepStep = 100;
        private readonly Queue<T> _queue;
        private readonly int _size;

        public BoundedQueue(int size)
        {
            _size = size;
            _queue = new Queue<T>();
        }

        public void Enqueue(T element)
        {
            while (Count() == _size)
                Thread.Sleep(_sleepStep);

            _queue.Enqueue(element);
        }

        public T Dequeue()
        {
            while (Count() == 0)
                Thread.Sleep(_sleepStep);

            return _queue.Dequeue();
        }

        public int Count() => _queue.Count;

        public int Size() => _size;

        public bool TryEnqueue(T element, int timeoutMsec)
        {
            if (!TryLoop(_size, timeoutMsec))
                return false;

            _queue.Enqueue(element);
            return true;
        }

        public bool TryDequeue(int timeoutMsec, out T? element)
        {
            element = default;

            if (!TryLoop(0, timeoutMsec))
                return false;

            element = _queue.Dequeue();
            return true;
        }

        private bool TryLoop(int size, int timeoutMsec)
        {
            int sleepTimeTotal = 0;

            while (Count() == size)
            {
                Thread.Sleep(_sleepStep);
                sleepTimeTotal += _sleepStep;

                if (sleepTimeTotal > timeoutMsec)
                    return false;
            }

            return true;
        }
    }
}