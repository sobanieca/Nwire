namespace NWire
{
    using System;
    using System.IO;
    using System.Reflection;
    using NWire.Domain;

    internal class Scanner
    {
        public ScanResult Scan()
        {
            ScanResult scanResult = new ScanResult();
            string location = Assembly.GetExecutingAssembly().Location;
            DirectoryInfo di = new DirectoryInfo(Path.GetDirectoryName(location));
            ScanDirectory(di.Parent, scanResult);

            return scanResult;
        }

        public void ScanDirectory(DirectoryInfo directory, ScanResult scanResult)
        {
            if (IsGitSubmodule(directory))
            {
                return;
            }

            foreach (var file in directory.EnumerateFiles())
            {
                string fileName = file.Name.ToLower();
                if (fileName.EndsWith(".sln"))
                {
                    Solution sln = new Solution();
                    sln.Name = file.Name.Replace(".sln", String.Empty);
                    sln.SlnContent = file.OpenText().ReadToEnd();
                    sln.DirectoryInfo = directory;
                    scanResult.Solutions.Add(sln);
                }

                if (fileName.EndsWith(".csproj"))
                {
                    Project proj = new Project();
                    proj.Name = file.Name.Replace(".csproj", String.Empty);
                    proj.DirectoryInfo = directory;
                    proj.CsProjContent = file.OpenText().ReadToEnd();

                    FileInfo[] packagesFiles = directory.GetFiles("packages.config");
                    if (packagesFiles.Length > 0)
                    {
                        FileInfo packagesFile = packagesFiles[0];
                        proj.PackagesContent = packagesFile.OpenText().ReadToEnd();
                    }

                    scanResult.Projects.Add(proj);
                }
            }

            foreach (var subdirectory in directory.EnumerateDirectories())
            {
                if (subdirectory.Name.ToLower() == ".git")
                {
                    GitRepository repository = new GitRepository();
                    repository.DirectoryInfo = directory;
                    scanResult.Repositories.Add(repository);
                }
                else
                {
                    ScanDirectory(subdirectory, scanResult);
                }
            }
        }

        private bool IsGitSubmodule(DirectoryInfo directory)
        {
            foreach (var file in directory.EnumerateFiles())
            {
                if (file.Name.ToLower() == ".git")
                {
                    using (var sr = file.OpenText())
                    {
                        if (sr.ReadToEnd().IndexOf("/modules") != -1)
                            return true;
                    }
                }
            }
            return false;
        }
    }
}
