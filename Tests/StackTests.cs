using FluentAssertions;

namespace Tests
{
    [TestClass]
    public class StackTests
    {
        [DataTestMethod]
        [DataRow(new int[] { 1, 2, 3, 4, 5 })]
        [DataRow(new int[] { 1, 2, 3 })]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6 })]
        [DataRow(new int[] { 1 })]
        [DataRow(new int[] { })]
        public void Test_Push_method_without_using_Pop_method(int[] elements)
        {
            _09_Stack.Stack<int> sut = new();

            foreach (int element in elements)
                sut.Push(element);

            sut.Count.Should().Be(elements.Length);
        }

        [DataTestMethod]
        [DataRow(new int[] { 1, 2, 3, 4, 5 }, new int[] { 5, 4 })]
        [DataRow(new int[] { 1, 2, 3 }, new int[] { 3 })]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 6, 5, 4, 3, 2 })]
        [DataRow(new int[] { 1 }, new int[] { })]
        [DataRow(new int[] { }, new int[] { })]
        public void Test_Pop_method_without_using_Push_method(int[] elements, int[] expectedPops)
        {
            _09_Stack.Stack<int> sut = new(elements);

            foreach (int element in expectedPops)
                sut.Pop().Should().Be(element);

            sut.Count.Should().Be(elements.Length - expectedPops.Length);
        }

        [TestMethod]
        public void Test_example()
        {
            _09_Stack.Stack<int> sut = new();
            sut.Count.Should().Be(0);
            sut.Push(5);
            sut.Count.Should().Be(1);
            sut.Pop().Should().Be(5);
            sut.Count.Should().Be(0);
            Action act = () => sut.Pop();
            act.Should().Throw<InvalidOperationException>("Stack is empty");
            sut.Push(7);
            sut.Count.Should().Be(1);
            sut.Push(9);
            sut.Count.Should().Be(2);
            sut.Push(1);
            sut.Count.Should().Be(3);
            sut.Pop().Should().Be(1);
            sut.Count.Should().Be(2);
            sut.Pop().Should().Be(9);
            sut.Count.Should().Be(1);
            sut.Pop().Should().Be(7);
            sut.Count.Should().Be(0);
        }
    }
}