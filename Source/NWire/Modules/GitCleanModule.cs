namespace NWire.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using LibGit2Sharp;
    using NWire.Domain;
    using NWire.Modules.Abstract;
    using NWire.Modules.Enums;

    internal class GitCleanModule : Module
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
                return "Performs git clean -xdf command for each repository, if there are no uncommitted changes";
            }
        }

        public override void Run(Result result, ScanResult scanResult)
        {
            foreach (var gitRepository in scanResult.Repositories)
            {
                using (var repo = new Repository(gitRepository.DirectoryInfo.FullName))
                {
                    StatusOptions options = new StatusOptions();
                    options.Show = StatusShowOption.IndexAndWorkDir;
                    options.DetectRenamesInIndex = true;
                    options.DetectRenamesInWorkDir = true;
                    options.RecurseIgnoredDirs = true;
                    RepositoryStatus status = repo.RetrieveStatus(options);
                    if (!status.IsDirty)
                    {
                        string gitPath = ConfigurationManager.AppSettings["GitPath"];
                        if (!String.IsNullOrWhiteSpace(gitPath))
                        {
                            ProcessStartInfo psi = new ProcessStartInfo();
                            psi.FileName = gitPath;
                            psi.WorkingDirectory = gitRepository.DirectoryInfo.FullName;
                            psi.Arguments = String.Format("clean -xdf \"{0}\"", gitRepository.DirectoryInfo.FullName);
                            Process.Start(psi);
                        }
                    }
                    else
                    {
                        result.AddResultItem(gitRepository, Domain.Enums.EMessageLevel.Warning, "Cannot clean - there are uncommitted changes");
                    }
                }
            }
        }
    }
}
