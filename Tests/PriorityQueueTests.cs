using _07_PriorityQueue;
using FluentAssertions;

namespace Tests
{
    [TestClass]
    public class PriorityQueueTests
    {
        [TestMethod]
        public void Test_Count_zero_at_the_beginning()
        {
            PriorityQueue<int> sut = new();
            sut.Count().Should().Be(0);
        }

        [TestMethod]
        public void Empty_sut_Dequeue_throws_InvalidOperationException()
        {
            PriorityQueue<int> sut = new();

            Action act = () => sut.Dequeue();
            act.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void Enqueue_one_element_and_get_correct_count()
        {
            PriorityQueue<string> sut = new();
            sut.Enqueue("word", 1);
            sut.Count().Should().Be(1);
        }

        [TestMethod]
        public void Enqueue_five_elements_and_Dequeue_three_and_get_correct_count()
        {
            PriorityQueue<double> sut = new();
            sut.Enqueue(1.1, 1);
            sut.Enqueue(1.2, 1);
            sut.Enqueue(1.3, 1);
            sut.Enqueue(99.11, 1);
            sut.Enqueue(111.11, 1);
            sut.Dequeue();
            sut.Dequeue();
            sut.Count().Should().Be(3);
        }

        [TestMethod]
        public void Test_extended_example_with_priorities()
        {
            PriorityQueue<int> sut = new();
            sut.Enqueue(1, 5);
            sut.Count().Should().Be(1);

            sut.Enqueue(2, 5);
            sut.Count().Should().Be(2);

            sut.Dequeue().Should().Be(1);
            sut.Count().Should().Be(1);

            sut.Enqueue(4, 7);
            sut.Count().Should().Be(2);

            sut.Enqueue(3, 7);
            sut.Count().Should().Be(3);

            sut.Enqueue(5, 3);
            sut.Count().Should().Be(4);

            sut.Dequeue().Should().Be(4);
            sut.Count().Should().Be(3);

            sut.Enqueue(11, 9);
            sut.Count().Should().Be(4);

            sut.Dequeue().Should().Be(11);
            sut.Count().Should().Be(3);

            sut.Dequeue().Should().Be(3);
            sut.Count().Should().Be(2);
        }
    }
}