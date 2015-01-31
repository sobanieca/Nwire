namespace NWire.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NWire.Domain;
    using NWire.Modules.Abstract;

    public class StyleCopEnabledModule : Module
    {
        public override string Name
        {
            get { return "StyleCopEnabledCheck"; }
        }

        public override string Description
        {
            get { return "Ensures that each project in each solution has StyleCop enabled to check rules on each build"; }
        }

        public override void Run(Result result, ScanResult scanResult)
        {
            throw new NotImplementedException();
        }
    }
}
