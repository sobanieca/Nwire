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

    public class GitPullModule : Module
    {
        public GitPullModule()
        {
            Priority = EPriority.High;
        }

        public override string Name
        {
            get { return "GitPull"; }
        }

        public override string Description
        {
            get { return "Performs git pull command for each repository, if there are no local changes"; }
        }

        public override void Run(Result result, ScanResult scanResult)
        {
            throw new NotImplementedException();
        }
    }
}
