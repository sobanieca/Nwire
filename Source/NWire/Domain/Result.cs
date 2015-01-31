namespace NWire.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NWire.Domain.Enums;

    public class Result
    {
        public Result()
        {
            ResultItems = new List<ResultItem>();
        }

        public List<ResultItem> ResultItems { get; set; }

        public void AddResultItem(string repository, EMessageLevel level, string message)
        {
            AddResultItem(repository, null, null, level, message);
        }

        public void AddResultItem(string repository, string solution, EMessageLevel level, string message)
        {
            AddResultItem(repository, solution, null, level, message);
        }

        public void AddResultItem(string repository, string solution, string project, EMessageLevel level, string message)
        {
            ResultItem resultItem = new ResultItem();
            resultItem.RepositoryName = repository;
            resultItem.SolutionName = solution;
            resultItem.ProjectName = project;
            resultItem.MessageLevel = level;
            resultItem.Message = message;
        }
    }
}
