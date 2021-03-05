using System.Collections;
using System.Collections.Generic;

namespace Steeltoe.Initializr.ApiTests
{
    public class AllProjectCombinations : IEnumerable<object[]>
    {
        private readonly List<object[]> _combinations;

        public AllProjectCombinations()
        {
            _combinations = new List<object[]>();
            var versions = Configuration.SteeltoeVersions;
            var frameworks = Configuration.DotNetFrameworks;
            // versions = new[] {"3.0.2"};
            // frameworks = new[] {"netcoreapp3.1"};
            foreach (var version in versions)
            {
                foreach (var framework in frameworks)
                {
                    _combinations.Add(new object[] {version, framework});
                }
            }
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            return _combinations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class AllProjectCombinationsWithDependencies : IEnumerable<object[]>
    {
        private readonly List<object[]> _combinations;

        public AllProjectCombinationsWithDependencies()
        {
            _combinations = new List<object[]>();
            var versions = Configuration.SteeltoeVersions;
            var frameworks = Configuration.DotNetFrameworks;
            var deps = Configuration.Dependencies;
            // versions = new[] {"3.0.2"};
            // frameworks = new[] {"netcoreapp3.1"};
            // deps = new[] {"docker"};
            foreach (var version in versions)
            {
                foreach (var framework in frameworks)
                {
                    foreach (var dep in deps)
                    {
                        _combinations.Add(new object[] {version, framework, dep});
                    }
                }
            }
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            return _combinations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
