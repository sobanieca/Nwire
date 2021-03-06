﻿namespace NWire
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NWire.Domain;
    using NWire.Modules.Abstract;

    internal class NwireCore
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
            Output.Clear();
            Output.WriteLine("NWire - .NET projects consistency check");
            Output.WriteLine("Choose which of the following modules you want to run");
            if (_showHelp)
                Output.WriteLine();
            foreach (var key in _moduleManager.Modules.Keys)
            {
                Module module = _moduleManager.Modules[key];
                if (_showHelp)
                {
                    Output.WriteLine("{0} - [{1}] - {2} - {3}", key, module.IsEnabled ? "X" : " ", module.Name, module.Description);
                    Output.WriteLine();
                }
                else
                {
                    Output.WriteLine("{0} - [{1}] - {2}", key, module.IsEnabled ? "X" : " ", module.Name);
                }
            }

            Output.WriteLine();
            Output.WriteLine("Press R to RUN selected modules, H for HELP toggle, or 0 to EXIT.");
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
            Output.WriteLine();
            Output.WriteLine("Starting scan...");
            sw.Start();
            ScanResult scanResult = scanner.Scan();
            sw.Stop();
            Output.WriteLine("Finished scan in {0} ms", sw.ElapsedMilliseconds);

            Result result = new Result();
            List<Module> modules = ReadModules();

            if (modules.Any(x => x.IsEnabled))
            {
                foreach (var module in modules)
                {
                    if (module.IsEnabled)
                    {
                        sw.Reset();
                        sw.Start();
                        Output.WriteLine("Running {0} module...", module.Name);
                        module.Run(result, scanResult);
                        sw.Stop();
                        Output.WriteLine("Finished {0} in {1} ms", module.Name, sw.ElapsedMilliseconds);
                    }
                }

                DisplayResult(result);
                Output.Flush();
            }
            else
            {
                Output.WriteLine("[ERROR] Please choose at least one module!");
            }
        }

        private void DisplayResult(Result result)
        {
            if (result.ResultItems.Count == 0)
            {
                Output.WriteLine("[SUCCESS] All projects look fine!");
            }
            else
            {
                foreach (var resultItem in result.ResultItems)
                {
                    Output.WriteLine("[{0}][{1}][{2}] {3}", resultItem.MessageLevel.ToString(), resultItem.ObjectType, resultItem.ObjectName, resultItem.Message);
                }
            }
        }

        private List<Module> ReadModules()
        {
            List<Module> result = new List<Module>();
            foreach (var key in _moduleManager.Modules.Keys)
            {
                var module = _moduleManager.Modules[key];
                result.Add(module);
            }

            return result.OrderByDescending(x => x.Priority).ToList();
        }
    }
}
