namespace NWire.Modules.Abstract
{
    using System.IO;
    using System.Xml.Linq;
    using NWire.Domain;
    using NWire.Modules.Enums;

    public abstract class Module
    {
        public Module()
        {
            Priority = EPriority.Normal;
        }

        public abstract string Name { get; }

        public abstract string Description { get; }

        public EPriority Priority { get; set; }

        public bool IsEnabled { get; set; }

        public abstract void Run(Result result, ScanResult scanResult);
    }
}
