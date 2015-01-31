namespace NWire.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NWire.Domain;
    using NWire.Modules.Abstract;
    using NWire.Modules.Enums;

    public class GitCleanModule : Module
    {
        public GitCleanModule()
        {
            Priority = EPriority.High;
        }

        public override string Name
        {
            get
            {
                return "GitClean";
            }
        }

        public override string Description
        {
            get
            {
                return "Performs git clean -xdf command for each repository, if there are no uncommited changes";
            }
        }

        public override void Run(Result result, ScanResult scanResult)
        {
            throw new NotImplementedException();
        }
    }
}
