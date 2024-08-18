using _08_CircularBuffer;
using FluentAssertions;

namespace Tests
{
    [TestClass]
    public class CircularBufferTests
    {
        [TestMethod]
        public void New_CircularBuffer_not_null_and_Count_zero()
        {
            CircularBuffer<int> sut = new(1);
            sut.Should().NotBeNull();
            sut.Count().Should().Be(0);
        }

        [TestMethod]
        public void New_CircularBuffer_Size_equals_initialized_size()
        {
            int size = 3;
            CircularBuffer<int> sut = new(size);
            sut.Size().Should().Be(size);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(-5)]
        [DataRow(-1000)]
        public void Invalid_initialzed_size_throws_ArgumentException(int size)
        {
            Action act = () => new CircularBuffer<int>(size);
            act.Should().Throw<ArgumentException>();
        }

        [DataTestMethod]
        [DataRow(new int[] { 1, 2, 3, 4 }, 10)]
        [DataRow(new int[] { 1 }, 5)]
        [DataRow(new int[] { 1, 2, 3, 4, 3, 2, 1 }, 20)]
        public void Add_elements_and_get_correct_count_and_size(int[] elements, int size)
        {
            CircularBuffer<int> sut = new(size);

            foreach (int element in elements)
                sut.Add(element);

            sut.Size().Should().Be(size);
            sut.Count().Should().Be(elements.Length);
        }

        [DataTestMethod]
        [DataRow(new int[] { 1, 2, 3, 4 }, 10, 2)]
        [DataRow(new int[] { 1 }, 5, 1)]
        [DataRow(new int[] { 1, 2, 3, 4, 3, 2, 1 }, 20, 5)]
        public void Add_and_take_elements_and_get_correct_count_and_size(int[] elements, int size, int take)
        {
            CircularBuffer<int> sut = new(size);

            foreach (int element in elements)
                sut.Add(element);

            sut.Size().Should().Be(size);
            sut.Count().Should().Be(elements.Length);

            for (int i = 0; i < take; i++)
                sut.Take();

            sut.Size().Should().Be(size);
            sut.Count().Should().Be(elements.Length - take);
        }

        [DataTestMethod]
        [DataRow(new int[] { 1, 2, 3, 4 }, 10, 5)]
        [DataRow(new int[] { 1 }, 5, 7)]
        [DataRow(new int[] { 1, 2, 3, 4, 3, 2, 1 }, 20, 30)]
        public void Take_too_much_elements_throws_InvalidOperationException(int[] elements, int size, int take)
        {
            CircularBuffer<int> sut = new(size);

            foreach (int element in elements)
                sut.Add(element);

            sut.Size().Should().Be(size);
            sut.Count().Should().Be(elements.Length);

            for (int i = 0; i < take; i++)
            {
                if (sut.Count() == 0)
                {
                    Action act = () => sut.Take();
                    act.Should().Throw<InvalidOperationException>().WithMessage("Circular Buffer is empty");
                }
                else
                {
                    sut.Take();
                }
            }
        }

        [TestMethod]
        public void Test_example()
        {
            CircularBuffer<int> sut = new(3);
            sut.Add(1);
            sut.Add(2);
            sut.Size().Should().Be(3);
            sut.Count().Should().Be(2);
            sut.Take().Should().Be(1);
            sut.Add(3);
            sut.Add(4);
            sut.Add(5);
            sut.Take().Should().Be(3);
            sut.Add(6);
            sut.Add(7);
        }

        [TestMethod]
        public void Test_example_without_silent_replacement_throws_exception()
        {
            CircularBuffer<int> sut = new(3);
            sut.Add(1);
            sut.Add(2);
            sut.Size().Should().Be(3);
            sut.Count().Should().Be(2);
            sut.Take().Should().Be(1);
            sut.Add(3);
            sut.Add(4);
            sut.Add(5);
            sut.Take().Should().Be(3);
            sut.SilentReplacement = false;
            sut.Add(6);
            Action act = () => sut.Add(7);
            act.Should().Throw<InvalidOperationException>("Circular Buffer is full and replacement not allowed");
            sut.SilentReplacement = true;
            sut.Add(7);
        }
    }
}