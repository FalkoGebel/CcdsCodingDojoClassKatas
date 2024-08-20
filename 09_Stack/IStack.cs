namespace _09_Stack
{
    internal interface IStack<T>
    {
        void Push(T element);

        T Pop();
    }
}