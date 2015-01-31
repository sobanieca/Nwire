namespace NWire.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NWire.Domain;
    using NWire.Modules.Abstract;

    public class GlobalNuGetConsistencyModule : Module
    {
        public override string Name
        {
            get { return "GlobalNuGetConsistencyModule"; }
        }

        public override string Description
        {
            get { return "Verifies that all projects in all solutions in all repositories, use the same NuGet package version"; }
        }

        public override void Run(Result result, ScanResult scanResult)
        {
            throw new NotImplementedException();
        }
    }
}
