namespace NWire.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using LibGit2Sharp;
    using NWire.Domain;
    using NWire.Domain.Enums;
    using NWire.Modules.Abstract;
    using NWire.Modules.Enums;

    internal class GitPullModule : Module
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
            string username = String.Empty;
            FetchOptions fetchOptions = ReadFetchOptions(out username);

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
                        if (repo.Head.TrackingDetails.AheadBy == 0)
                        {
                            Output.WriteLine("Pulling changes for repo: {0}", gitRepository.DirectoryInfo.FullName);
                            repo.Fetch("origin", fetchOptions);
                            Commit latestCommit = repo.Head.TrackedBranch.Commits.FirstOrDefault();
                            if (latestCommit != null)
                            {
                                repo.Merge(latestCommit, new Signature(username, username, new DateTimeOffset(DateTime.Now)));
                            }
                        }
                        else
                        {
                            result.AddResultItem(gitRepository, EMessageLevel.Warning, "Cannot pull - there are unpushed changes");
                        }
                    }
                    else
                    {
                        result.AddResultItem(gitRepository, EMessageLevel.Warning, "Cannot pull - there are uncommitted changes");
                    }
                }
            }
        }

        private FetchOptions ReadFetchOptions(out string username)
        {
            FetchOptions result = new FetchOptions();

            Output.WriteLine("Provide your GIT username and press ENTER");
            string providedUsername = Console.ReadLine();
            Output.WriteLine("Provide your GIT password and press ENTER");
            string providedPassword = ReadPassword();
            Output.WriteLine();

            result.CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials
            {
                Username = providedUsername,
                Password = providedPassword
            };

            username = providedUsername;

            return result;
        }

        private string ReadPassword()
        {
            string result = String.Empty;
            ConsoleKeyInfo key = default(ConsoleKeyInfo);

            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    result += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && result.Length > 0)
                    {
                        result = result.Substring(0, result.Length - 1);
                        Console.Write("\b \b");
                    }
                }
            }
            while (key.Key != ConsoleKey.Enter);

            for (int i = 0; i < result.Length; i++)
            {
                Console.Write("\b \b");
            }

            return result;
        }
    }
}
