using _01_BoundedQueue;
using FluentAssertions;
using System.Diagnostics;

namespace Tests
{
    [TestClass]
    public class BoundedQueueTests
    {
        [TestMethod]
        public void Create_int_sut_without_error()
        {
            _ = new BoundedQueue<int>(1);
        }

        [TestMethod]
        public void Create_string_sut_without_error()
        {
            _ = new BoundedQueue<string>(2);
        }

        [TestMethod]
        public void Create_List_of_int_sut_without_error()
        {
            _ = new BoundedQueue<List<int>>(5);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(9)]
        [DataRow(100)]
        public void Create_int_sut_with_a_given_size_and_get_correct_size_from_size_method(int size)
        {
            BoundedQueue<int> sut = new(size);
            sut.Size().Should().Be(size);
        }

        [DataTestMethod]
        [DataRow(new int[] { })]
        [DataRow(new int[] { 1 })]
        [DataRow(new int[] { 1, 2, 3, 5, 10 })]
        [DataRow(new int[] { 1, 11, 111, 1111, 11111, 11111, 111111, 9, 99, 999, 999, 0 })]
        public void Create_int_sut_and_get_correct_count_from_count_method_after_adding_elements(int[] elements)
        {
            BoundedQueue<int> sut = new(elements.Length + 100);

            foreach (int element in elements)
                sut.Enqueue(element);

            sut.Count().Should().Be(elements.Length);
        }

        [DataTestMethod]
        [DataRow(new int[] { 1, 2, 3, 5, 10, 3 })]
        [DataRow(new int[] { 1, 11, 111, 1111, 11111, 11111, 111111, 9, 99, 999, 999, 0 })]
        public void Create_int_sut_and_get_correct_count_from_count_method_after_adding_and_removing_elements(int[] elements)
        {
            BoundedQueue<int> sut = new(elements.Length + 100);

            foreach (int element in elements)
                sut.Enqueue(element);

            for (int i = 0; i < elements.Length; i += 2)
                sut.Dequeue();

            sut.Count().Should().Be(elements.Length / 2);
        }

        [TestMethod]
        public void One_int_sut_with_two_threads_and_correct_finish_count()
        {
            BoundedQueue<int> sut = new(2);
            Thread threadWriting = new(() => WriteToSutEnqueue(sut));
            Thread threadReading = new(() => ReadFromSutDequeue(sut));
            threadWriting.Start();
            threadReading.Start();
            threadWriting.Join();
            threadReading.Join();
            sut.Count().Should().Be(0);
            Debug.WriteLine("Test (One_int_sut_with_two_threads_and_correct_finish_count) is over");
        }

        private void WriteToSutEnqueue(BoundedQueue<int> sut)
        {
            Debug.WriteLine($"current count: {sut.Count()} -> Enqueue 1 to sut and sleep 1000");
            sut.Enqueue(1);
            Thread.Sleep(1000);
            Debug.WriteLine($"current count: {sut.Count()} -> Enqueue 2 to sut and sleep 100");
            sut.Enqueue(2);
            Thread.Sleep(100);
            Debug.WriteLine($"current count: {sut.Count()} -> Enqueue 3 to sut and sleep 100");
            sut.Enqueue(3);
            Thread.Sleep(100);
            Debug.WriteLine($"current count: {sut.Count()} -> Enqueue 4 to sut and sleep 1000");
            sut.Enqueue(4);
            Thread.Sleep(1000);
            Debug.WriteLine($"current count: {sut.Count()} -> Enqueue 5 to sut");
            sut.Enqueue(5);
        }

        private void ReadFromSutDequeue(BoundedQueue<int> sut)
        {
            Debug.WriteLine($"Dequeue {sut.Dequeue()} from sut and sleep 1000");
            Thread.Sleep(1000);
            Debug.WriteLine($"Dequeue {sut.Dequeue()} from sut and sleep 1000");
            Thread.Sleep(1000);
            Debug.WriteLine($"Dequeue {sut.Dequeue()} from sut and sleep 100");
            Thread.Sleep(100);
            Debug.WriteLine($"Dequeue {sut.Dequeue()} from sut and sleep 100");
            Thread.Sleep(100);
            Debug.WriteLine($"Dequeue {sut.Dequeue()} from sut");
        }

        [TestMethod]
        public void One_int_sut_with_two_threads_and_correct_finish_count_using_try_methods()
        {
            BoundedQueue<int> sut = new(2);
            Thread threadWriting = new(() => WriteToSutTryEnqueue(sut));
            Thread threadReading = new(() => ReadFromSutTryDequeue(sut));
            threadWriting.Start();
            threadReading.Start();
            threadWriting.Join();
            threadReading.Join();
            sut.Count().Should().Be(1);
            Debug.WriteLine("Test (One_int_sut_with_two_threads_and_correct_finish_count) is over");
        }

        private void WriteToSutTryEnqueue(BoundedQueue<int> sut)
        {
            Debug.WriteLine($"current count: {sut.Count()} -> Enqueue 1 to sut and sleep 1000");
            if (sut.TryEnqueue(1, 500))
                Debug.WriteLine(" -> done");
            else
                Debug.WriteLine(" -> timed out");
            Thread.Sleep(1000);
            Debug.WriteLine($"current count: {sut.Count()} -> Enqueue 2 to sut and sleep 100");
            if (sut.TryEnqueue(2, 500))
                Debug.WriteLine(" -> done");
            else
                Debug.WriteLine(" -> timed out");
            Thread.Sleep(100);
            Debug.WriteLine($"current count: {sut.Count()} -> Enqueue 3 to sut and sleep 100");
            if (sut.TryEnqueue(3, 500))
                Debug.WriteLine(" -> done");
            else
                Debug.WriteLine(" -> timed out");
            Thread.Sleep(100);
            Debug.WriteLine($"current count: {sut.Count()} -> Enqueue 4 to sut and sleep 2000");
            if (sut.TryEnqueue(4, 500))
                Debug.WriteLine(" -> done");
            else
                Debug.WriteLine(" -> timed out");
            Thread.Sleep(2000);
            Debug.WriteLine($"current count: {sut.Count()} -> Enqueue 5 to sut");
            if (sut.TryEnqueue(5, 500))
                Debug.WriteLine(" -> done");
            else
                Debug.WriteLine(" -> timed out");
        }

        private void ReadFromSutTryDequeue(BoundedQueue<int> sut)
        {
            int number;

            try
            {
                if (sut.TryDequeue(500, out number))
                    Debug.WriteLine($"Dequeue {number} from sut and sleep 1000");
                else
                    Debug.WriteLine($"Dequeue timed out");
                Thread.Sleep(1000);
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine(e.Message);
            }

            try
            {
                if (sut.TryDequeue(500, out number))
                    Debug.WriteLine($"Dequeue {number} from sut and sleep 1000");
                else
                    Debug.WriteLine($"Dequeue timed out");
                Thread.Sleep(1000);
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine(e.Message);
            }

            try
            {
                if (sut.TryDequeue(500, out number))
                    Debug.WriteLine($"Dequeue {number} from sut and sleep 100");
                else
                    Debug.WriteLine($"Dequeue timed out");
                Thread.Sleep(100);
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine(e.Message);
            }

            try
            {
                if (sut.TryDequeue(500, out number))
                    Debug.WriteLine($"Dequeue {number} from sut and sleep 100");
                else
                    Debug.WriteLine($"Dequeue timed out");
                Thread.Sleep(100);
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine(e.Message);
            }

            try
            {
                if (sut.TryDequeue(500, out number))
                    Debug.WriteLine($"Dequeue {number} from sut and sleep 100");
                else
                    Debug.WriteLine($"Dequeue timed out");
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}