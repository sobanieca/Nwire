namespace NWire
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NWire.Domain;
    using NWire.Modules.Abstract;

    public class NwireCore
    {
        private bool _showHelp = false;
        private ModuleManager _moduleManager = new ModuleManager();

        public void Run()
        {
            DisplayMenu();
        }

        /// <summary>
        /// Adds new module, it can be run by an external app, which implements it's own module
        /// </summary>
        /// <param name="module"></param>
        public void AddModule(Module module)
        {
            int modulesCount = _moduleManager.Modules.Keys.Count;
            _moduleManager.Modules.Add(++modulesCount, module);
        }

        private void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("NWire - .NET projects consistency check");
            Console.WriteLine("Choose which of the following modules you want to run");
            if (_showHelp)
                Console.WriteLine();
            foreach (var key in _moduleManager.Modules.Keys)
            {
                Module module = _moduleManager.Modules[key];
                if (_showHelp)
                {
                    Console.WriteLine("{0} - [{1}] - {2} - {3}", key, module.IsEnabled ? "X" : " ", module.Name, module.Description);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("{0} - [{1}] - {2}", key, module.IsEnabled ? "X" : " ", module.Name);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press R to RUN selected modules, H for HELP toggle, or 0 to EXIT.");
            ConsoleKeyInfo selection = Console.ReadKey();
            string selectionChar = selection.KeyChar.ToString().ToUpper();
            if (selectionChar == "0")
                return;

            if (selectionChar == "H")
                _showHelp = !_showHelp;

            if (selectionChar == "R")
            {
                Start();
                return;
            }

            int index = (int)selection.KeyChar - 48;

            Module selectedModule = null;
            if (_moduleManager.Modules.TryGetValue(index, out selectedModule))
            {
                selectedModule.IsEnabled = !selectedModule.IsEnabled;
            }

            DisplayMenu();
        }

        private void Start()
        {
            Scanner scanner = new Scanner();
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Starting scan...");
            sw.Start();
            ScanResult scanResult = scanner.Scan();
            sw.Stop();
            Console.WriteLine("Finished scan in {0} ms", sw.ElapsedMilliseconds);
        }
    }
}
