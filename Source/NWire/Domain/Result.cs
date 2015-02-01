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

        public void AddResultItem(GitRepository repository, EMessageLevel level, string message)
        {
            AddResultItem("Repository", repository.DirectoryInfo.Name, level, message);
        }

        public void AddResultItem(Solution solution, EMessageLevel level, string message)
        {
            AddResultItem("Solution", solution.Name, level, message);
        }

        public void AddResultItem(Project project, EMessageLevel level, string message)
        {
            AddResultItem("Project", project.Name, level, message);
        }

        public void AddResultItem(string objectType, string objectName, EMessageLevel level, string message)
        {
            ResultItem resultItem = new ResultItem();
            resultItem.ObjectType = objectType;
            resultItem.ObjectName = objectName;
            resultItem.MessageLevel = level;
            resultItem.Message = message;
            ResultItems.Add(resultItem);
        }
    }
}
