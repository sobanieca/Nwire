namespace NWire.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using LibGit2Sharp;
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
                return "Performs git clean and git reset --hard command for each repository, if there are no uncommitted changes";
            }
        }

        public override void Run(Result result, ScanResult scanResult)
        {
            foreach(var gitRepository in scanResult.Repositories)
            {
                using(var repo = new Repository(gitRepository.DirectoryInfo.FullName))
                {
                    StatusOptions options = new StatusOptions();
                    options.Show = StatusShowOption.IndexAndWorkDir;
                    options.DetectRenamesInIndex = true;
                    options.DetectRenamesInWorkDir = true;
                    options.RecurseIgnoredDirs = true;
                    RepositoryStatus status = repo.RetrieveStatus(options);
                    if(!status.IsDirty)
                    {
                        repo.RemoveUntrackedFiles();
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
