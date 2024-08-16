using System.Collections;

namespace _05_LinkedList
{
    public class MyLinkedList<T> : IList<T>
    {
        private Element<T>? _element;

        private Element<T> GetElementAtIndex(int index)
        {
            int counter = 0;
            Element<T>? current = _element;

            while (counter < index && current != null)
            {
                counter++;
                current = current.Next;
            }

            if (current == null)
                throw new IndexOutOfRangeException("Invalid index on MyLinkdedList");

            return current;
        }

        public T this[int index]
        {
            get => GetElementAtIndex(index).Item;

            set => GetElementAtIndex(index).Item = value;
        }

        public int Count
        {
            get
            {
                int output = 0;
                Element<T>? current = _element;

                while (current != null)
                {
                    output++;
                    current = current.Next;
                }

                return output;
            }
        }

        public bool IsReadOnly => false;

        public void Add(T item)
        {

            if (_element == null)
            {
                _element = new Element<T>(item);
            }
            else
            {
                Element<T> current = _element;

                while (current.Next != null)
                    current = current.Next;

                current.Next = new Element<T>(item);
            }
        }

        public void Clear()
        {
            _element = null;
        }

        public bool Contains(T item)
        {
            Element<T>? current = _element;

            while (current != null)
            {
                if (current.Item.Equals(item))
                    return true;

                current = current.Next;
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < 0 || arrayIndex >= array.Length)
                throw new ArgumentOutOfRangeException("Invalid index for target array");

            if (array.Length < arrayIndex + Count)
                Array.Resize(ref array, arrayIndex + Count);

            Element<T> current = _element;

            for (int i = arrayIndex; i < array.Length; i++)
            {
                array[i] = current.Item;
                current = current.Next;
            }
        }

        public IEnumerator<T> GetEnumerator() => Elements().GetEnumerator();

        public int IndexOf(T item)
        {
            int index = 0;
            Element<T>? current = _element;

            while (current != null)
            {
                if (current.Item.Equals(item))
                    return index;

                current = current.Next;
                index++;
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException("Invalid index");

            Element<T> newElement = new(item);
            Element<T>? current = GetElementAtIndex(index);
            newElement.Next = current;

            if (index == 0)
            {
                _element = newElement;
            }
            else
            {
                Element<T>? previous = GetElementAtIndex(index - 1);
                previous.Next = newElement;
            }
        }

        public bool Remove(T item)
        {
            int idx = IndexOf(item);

            if (idx < 0)
                return false;

            RemoveAt(idx);

            return true;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException("Invalid index");

            if (index == 0)
            {
                _element = _element.Next;
            }
            else
            {
                Element<T>? previous = GetElementAtIndex(index - 1);
                Element<T>? current = GetElementAtIndex(index);
                previous.Next = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        private IEnumerable<T> Elements()
        {
            Element<T>? current = _element;

            while (current != null)
            {
                yield return current.Item;
                current = current.Next;
            }
        }
    }
}