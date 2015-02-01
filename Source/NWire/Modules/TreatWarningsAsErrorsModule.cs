namespace NWire.Modules
{
    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NWire.Domain;
using NWire.Domain.Enums;
using NWire.Helpers;
using NWire.Modules.Abstract;

    public class TreatWarningsAsErrorsModule : Module
    {
        private XmlHelper _xmlHelper = new XmlHelper();

        public override string Name
        {
            get { return "TreatWarningsAsErrorsCheck"; }
        }

        public override string Description
        {
            get { return "Verifies that each project has \"Treat warnings as errors\" flag set"; }
        }

        public override void Run(Result result, ScanResult scanResult)
        {
            _xmlHelper.ProjectsXmlNodePresenceRun("TreatWarningsAsErrors", result, scanResult);
        }
    }
}
