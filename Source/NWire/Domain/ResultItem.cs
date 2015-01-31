namespace NWire.Domain
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NWire.Domain.Enums;

    public class ResultItem
    {
        public string RepositoryName { get; set; }

        public string SolutionName { get; set; }

        public string ProjectName { get; set; }

        public string Message { get; set; }

        public EMessageLevel MessageLevel { get; set; }
    }
}
