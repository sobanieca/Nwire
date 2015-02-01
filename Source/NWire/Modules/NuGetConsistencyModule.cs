namespace NWire.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NWire.Domain;
    using NWire.Domain.Enums;
    using NWire.Modules.Abstract;

    public class NuGetConsistencyModule : Module
    {
        public override string Name
        {
            get { return "NuGetConsistencyModule"; }
        }

        public override string Description
        {
            get { return "Verifies that all projects use the same (latest, stable) NuGet package version"; }
        }

        public override void Run(Result result, ScanResult scanResult)
        {
            Dictionary<string, string> mostRecentPackages = ReadMostRecentPackages(result, scanResult);

            foreach(var project in scanResult.Projects)
            {
                List<NuGetPackage> projectPackages = project.ReadPackages();
                foreach(var package in projectPackages)
                {
                    if(package.Version.IndexOf("-") != -1)
                    {
                        result.AddResultItem(
                            project, 
                            EMessageLevel.Error,
                            String.Format("Unstable NuGet package reference found - {0} - {1}", package.Name, package.Version));
                    }
                    else
                    {
                        string key = ReadPackageNameKey(package);
                        string latestVersion = mostRecentPackages[key];
                        if (package.Version != latestVersion)
                        {
                            result.AddResultItem(
                                project, 
                                EMessageLevel.Error, 
                                String.Format("Old NuGet package reference found - {0} - {1}. Current version: {2}", package.Name, package.Version, latestVersion));
                        }
                    }
                }
            }
        }

        private Dictionary<string, string> ReadMostRecentPackages(Result result, ScanResult scanResult)
        {
            Dictionary<string, string> mostRecentPackages = new Dictionary<string, string>();

            foreach(var project in scanResult.Projects)
            {
                List<NuGetPackage> projectPackages = project.ReadPackages();
                foreach(var package in projectPackages)
                {
                    string key = ReadPackageNameKey(package);
                    if(package.Version.IndexOf("-") == -1)
                    {
                        if(!mostRecentPackages.ContainsKey(key))
                        {
                            mostRecentPackages.Add(key, package.Version);
                        }
                        else
                        {
                            string mostRecentVersion = mostRecentPackages[key];
                            if (IsLaterVersion(package, mostRecentVersion))
                                mostRecentPackages[key] = package.Version;
                        }
                    }
                }
            }

            return mostRecentPackages;
        }

        private bool IsLaterVersion(NuGetPackage package, string strMostRecentVersion)
        {
            Version mostRecentVersion = new Version(strMostRecentVersion);
            Version packageVersion = new Version(package.Version);

            if (packageVersion > mostRecentVersion)
                return true;
            else
                return false;
        }

        private string ReadPackageNameKey(NuGetPackage package)
        {
            return package.Name.ToLower().Trim();
        }
    }
}
