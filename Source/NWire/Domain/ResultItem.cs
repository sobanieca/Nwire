namespace NWire.Domain
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NWire.Domain.Enums;

    public class ResultItem
    {
        /// <summary>
        /// Repository, solution, project, etc.
        /// </summary>
        public string ObjectType { get; set; }

        public string ObjectName { get; set; }

        public string Message { get; set; }

        public EMessageLevel MessageLevel { get; set; }
    }
}
