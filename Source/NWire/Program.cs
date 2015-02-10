namespace NWire
{
    using System;
    using NWire.Modules.Abstract;

    internal class Program
    {
        internal static void Main(string[] args)
        {
            NwireCore core = new NwireCore();
            core.Run();
            Output.WriteLine("Press ENTER to exit...");
            Console.ReadLine();
        }
    }
}
