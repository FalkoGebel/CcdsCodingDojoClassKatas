using _06_OrderedJobs;
using FluentAssertions;

namespace Tests
{
    [TestClass]
    public class OrderedJobsTests
    {
        [TestMethod]
        public void Sut_not_null()
        {
            OrderedJobs sut = new();
            sut.Should().NotBeNull();
        }

        [DataTestMethod]
        [DataRow('-')]
        [DataRow('/')]
        [DataRow('?')]
        [DataRow('#')]
        [DataRow('D')]
        [DataRow('K')]
        public void Register_invalid_job_and_get_argument_exception(char job)
        {
            OrderedJobs sut = new();
            Action act = () => sut.Register(job);
            act.Should().Throw<ArgumentException>();
        }


        [DataTestMethod]
        [DataRow('-', 'a')]
        [DataRow('a', '&')]
        [DataRow('a', 'a')]
        public void Register_invalid_jobs_and_get_argument_exception(char dependendJob, char independendJob)
        {
            OrderedJobs sut = new();
            Action act = () => sut.Register(dependendJob, independendJob);
            act.Should().Throw<ArgumentException>();
        }

        [DataTestMethod]
        [DataRow('a', "a")]
        [DataRow('b', "b")]
        [DataRow('x', "x")]
        [DataRow('y', "y")]
        [DataRow('t', "t")]
        [DataRow('ß', "ß")]
        public void Register_one_job_and_get_this_job(char job, string expected)
        {
            OrderedJobs sut = new();
            sut.Register(job);
            sut.Sort().Should().Be(expected);
        }

        [TestMethod]
        public void Test_example()
        {
            OrderedJobs sut = new();
            sut.Register('c');
            sut.Register('b', 'a');
            sut.Register('c', 'b');
            sut.Sort().Should().Be("abc");
        }

        [TestMethod]
        public void Test_circular_dependency()
        {
            OrderedJobs sut = new();
            sut.Register('c');
            sut.Register('b', 'a');
            sut.Register('c', 'b');
            sut.Register('a', 'c');

            Action act = () => sut.Sort();
            act.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void Test_more_complex_example()
        {
            OrderedJobs sut = new();
            sut.Register('b', 'a');
            sut.Register('c', 'b');

            sut.Register('x', 'y');
            sut.Register('d', 'z');

            sut.Sort().Should().Be("ayzbcxd");
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("\n")]
        [DataRow("c => b => a\n")]
        [DataRow("cb => b\n")]
        public void Invalid_string_registration_and_get_argument_exception(string registrations)
        {
            OrderedJobs sut = new();
            Action act = () => sut.Register(registrations);
            act.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void Test_example_variation()
        {
            OrderedJobs sut = new();
            string registrations = "c =>\nb => a\nc => b";
            sut.Register(registrations);
            sut.Sort().Should().Be("abc");
        }

        [TestMethod]
        public void Test_more_complex_example_variation()
        {
            OrderedJobs sut = new();
            string registrations = "b => a\nc => b\nx => y\nd => z";
            sut.Register(registrations);
            sut.Sort().Should().Be("ayzbcxd");
        }
    }
}