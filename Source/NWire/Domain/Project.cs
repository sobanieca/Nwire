namespace NWire.Domain
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    public class Project
    {
        public string Name { get; set; }

        public string CsProjContent { get; set; }

        public string PackagesContent { get; set; }

        public List<NuGetPackage> ReadPackages()
        {
            List<NuGetPackage> result = new List<NuGetPackage>();

            if(!String.IsNullOrWhiteSpace(PackagesContent))
            {
                XDocument document = XDocument.Parse(PackagesContent);
                foreach(var packageXml in document.Descendants("package"))
                {
                    NuGetPackage package = new NuGetPackage();
                    package.Name = packageXml.Attribute("id").Value;
                    package.Version = packageXml.Attribute("version").Value;

                    result.Add(package);
                }
            }

            return result;
        }
    }
}
