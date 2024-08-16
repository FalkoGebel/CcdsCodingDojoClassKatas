using _05_LinkedList;
using FluentAssertions;

namespace Tests
{
    [TestClass]
    public class LinkedListTests
    {
        [TestMethod]
        public void Test_LinkedList_Count()
        {
            MyLinkedList<int> sut = new();
            sut.Count.Should().Be(0);
            sut.Add(1);
            sut.Count.Should().Be(1);
            sut.Add(2);
            sut.Count.Should().Be(2);
            sut.Add(3);
            sut.Count.Should().Be(3);
        }

        [TestMethod]
        public void Test_LinkedList_Index()
        {
            MyLinkedList<int> sut = [0, 1, 2, 3, 4, 5];
            sut[3].Should().Be(3);
            sut[1].Should().Be(1);
            sut[2] = 99;
            sut[2].Should().Be(99);
            sut[5] += sut[0] + sut[1] + sut[2];
            sut[5].Should().Be(105);
        }

        [TestMethod]
        public void Test_LinkedList_Clear()
        {
            MyLinkedList<int> sut = [0, 1, 2, 3, 4, 5];
            sut.Count.Should().Be(6);
            sut.Clear();
            sut.Count.Should().Be(0);
        }

        [TestMethod]
        public void Test_LinkedList_Contains()
        {
            MyLinkedList<string> sut = ["0", "1", "2", "3", "4", "5"];
            sut.Contains("99").Should().BeFalse();
            sut.Contains(sut[3]).Should().BeTrue();
            sut.Contains("3").Should().BeTrue();
        }

        [TestMethod]
        public void Test_LinkedList_CopyTo()
        {
            MyLinkedList<char> sut = ['_', '-', '/', '|', '\\'];
            char[] target = ['0', 'A', 'C', 'G', '_', '1', 'x', 's', '$', '?'];
            char[] expected = ['0', 'A', 'C', 'G', '_', '_', '-', '/', '|', '\\'];
            sut.CopyTo(target, 5);
            target.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Test_LinkedList_IndexOf()
        {
            MyLinkedList<double> sut = [123, 234, 444, 4444, 444];
            sut.IndexOf(55.55).Should().Be(-1);
            sut.IndexOf(123).Should().Be(0);
            sut.IndexOf(444).Should().Be(2);
            sut.IndexOf(4444).Should().Be(3);
        }

        [TestMethod]
        public void Test_LinkedList_RemoveAt()
        {
            MyLinkedList<double> sut = [123, 234, 444, 4444, 444];
            MyLinkedList<double> excpected = [123, 234, 4444, 444];
            sut.RemoveAt(2);
            sut.ToArray().Should().BeEquivalentTo(excpected.ToArray());
        }

        [TestMethod]
        public void Test_LinkedList_Remove()
        {
            MyLinkedList<double> sut = [123, 234, 444, 4444, 444];
            MyLinkedList<double> excpected = [123, 234, 4444, 444];
            sut.Remove(444).Should().BeTrue();
            sut.ToArray().Should().BeEquivalentTo(excpected.ToArray());
            sut.Remove(55.55).Should().BeFalse();
        }

        [TestMethod]
        public void Test_LinkedList_Insert()
        {
            MyLinkedList<string> sut = ["This", "is", "the", "of", "the", "king"];
            string expected = "This is the house of the king";
            sut.Insert(3, "house");
            string.Join(' ', sut.ToArray()).Should().Be(expected);
            sut.Insert(6, "new");
            expected = "This is the house of the new king";
            string.Join(' ', sut.ToArray()).Should().Be(expected);
        }

        [TestMethod]
        public void Test_LinkedList_GetEnumerator()
        {
            MyLinkedList<string> sut = ["This", "is", "the", "of", "the", "king"];
            sut.Should().BeEquivalentTo(sut.ToArray());
            sut = [];
            string.Concat(sut).Should().Be(string.Empty);
        }
    }
}