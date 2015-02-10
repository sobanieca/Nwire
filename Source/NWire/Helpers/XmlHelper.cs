namespace NWire.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using NWire.Domain;
    using NWire.Domain.Enums;

    internal class XmlHelper
    {
        public void ProjectsXmlNodePresenceRun(string nodeName, Result result, ScanResult scanResult)
        {
            foreach (var project in scanResult.Projects)
            {
                XDocument projectXml = XDocument.Parse(project.CsProjContent);
                var query = from n in projectXml.Descendants() where n.Name.LocalName == nodeName select n;
                if (query.Count() == 0)
                {
                    result.AddResultItem(project, EMessageLevel.Error, String.Format("No {0} flag found!", nodeName));
                }

                foreach (var node in query)
                {
                    if (!String.IsNullOrWhiteSpace(node.Value))
                    {
                        if (node.Value.ToLower().Trim() != "true")
                        {
                            result.AddResultItem(project, EMessageLevel.Error, String.Format("{0} is set to false!", nodeName));
                        }
                    }
                }
            }
        }
    }
}
