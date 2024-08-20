namespace _09_Stack
{
    public class Stack<T> : IStack<T>
    {
        private T[] _elements = [];

        public Stack(T[]? elements = null)
        {
            if (elements != null)
                _elements = elements;
        }

        public int Count
        {
            get
            {
                return _elements.Length;
            }
        }

        public T Pop()
        {
            if (_elements.Length == 0)
                throw new InvalidOperationException("Stack is empty");

            T output = _elements[^1];

            Array.Resize(ref _elements, _elements.Length - 1);

            return output;
        }

        public void Push(T element)
        {
            Array.Resize(ref _elements, _elements.Length + 1);
            _elements[^1] = element;
        }
    }
}