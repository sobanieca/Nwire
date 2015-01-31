namespace NWire
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using NWire.Modules.Abstract;

    public class ModuleManager
    {
        private static Dictionary<int, NWire.Modules.Abstract.Module> _modules = null;
        private static object _locker = new object();

        public ModuleManager()
        {
            if(_modules == null)
            {
                lock(_locker)
                {
                    _modules = new Dictionary<int, NWire.Modules.Abstract.Module>();
                    int counter = 1;
                    foreach(var type in Assembly.GetExecutingAssembly().GetTypes())
                    {
                        if(type.IsSubclassOf(typeof(NWire.Modules.Abstract.Module)) && !type.IsAbstract)
                        {
                            NWire.Modules.Abstract.Module module = (NWire.Modules.Abstract.Module)Activator.CreateInstance(type);
                            _modules.Add(counter++, module);
                        }
                    }
                }
            }
        }

        public Dictionary<int, NWire.Modules.Abstract.Module> Modules
        {
            get
            {
                return _modules;
            }
        }
    }
}
