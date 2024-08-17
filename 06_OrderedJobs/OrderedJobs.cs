namespace _06_OrderedJobs
{
    public class OrderedJobs : IOrderedJobs
    {
        Dictionary<char, List<char>> _jobs;

        public OrderedJobs()
        {
            _jobs = [];
        }

        public void Register(char dependentJob, char independentJob)
        {
            if (dependentJob == independentJob)
                throw new ArgumentException("Job can not be dependent from itself");

            CheckJob(dependentJob);
            CheckJob(independentJob);

            if (!_jobs.ContainsKey(dependentJob))
                _jobs[dependentJob] = [independentJob];
            else if (!_jobs[dependentJob].Contains(independentJob))
                _jobs[dependentJob].Add(independentJob);

            Register(independentJob);
        }

        public void Register(char job)
        {
            CheckJob(job);

            if (!_jobs.ContainsKey(job))
                _jobs[job] = [];
        }

        public void Register(string registrations)
        {
            string[] registrationArray = registrations.Split('\n');

            if (registrationArray.Length == 0)
                throw new ArgumentException("Invalid registration string");

            foreach (string registration in registrationArray)
            {
                string[] parts = registration.Split("=>", System.StringSplitOptions.RemoveEmptyEntries)
                                             .Select(r => r.Trim())
                                             .ToArray();

                if (parts.Length < 1 || parts.Length > 2 || parts.Any(p => p.Length != 1))
                    throw new ArgumentException("Invalid registration string");

                if (parts.Length == 1)
                    Register(parts[0][0]);
                else
                    Register(parts[0][0], parts[1][0]);
            }
        }

        public string Sort()
        {
            string output = string.Concat(_jobs.Where(j => j.Value.Count == 0).ToDictionary().Keys);
            List<char> dependentJobs = _jobs.Where(j => j.Value.Count > 0).ToDictionary().Keys.ToList();

            while (dependentJobs.Count > 0)
            {
                List<int> validIndexes = [];

                for (int i = 0; i < dependentJobs.Count; i++)
                {
                    char currentJob = dependentJobs[i];

                    if (_jobs[currentJob].All(j => output.Contains(j)))
                    {
                        output += currentJob;
                        validIndexes.Add(i);
                    }
                }

                if (validIndexes.Count == 0)
                    throw new InvalidOperationException("Operation not possible - there are circular dependencies in the data");

                validIndexes.Reverse();
                foreach (int i in validIndexes)
                    dependentJobs.RemoveAt(i);
            }

            return output;
        }

        private void CheckJob(char job)
        {
            if (!char.IsLetter(job) || !char.IsLower(job))
                throw new ArgumentException("Invalid job - only lower letters allowed");

        }
    }
}