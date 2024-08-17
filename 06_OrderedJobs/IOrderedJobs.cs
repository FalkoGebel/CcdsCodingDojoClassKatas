namespace _06_OrderedJobs
{
    internal interface IOrderedJobs
    {
        void Register(char dependentJob, char independentJob);
        void Register(char job);
        void Register(string registrations);
        string Sort();
    }
}