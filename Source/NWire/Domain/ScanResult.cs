namespace NWire.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ScanResult
    {
        public ScanResult()
        {
            Repositories = new List<GitRepository>();
            Solutions = new List<Solution>();
            Projects = new List<Project>();
        }

        public List<GitRepository> Repositories { get; set; }

        public List<Solution> Solutions { get; set; }

        public List<Project> Projects { get; set; }
    }
}
