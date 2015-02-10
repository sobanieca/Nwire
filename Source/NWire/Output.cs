namespace NWire
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Security.AccessControl;
    using System.Text;
    using System.Threading.Tasks;

    public static class Output
    {
        private static StringBuilder _stringBuilder = new StringBuilder();

        public static void WriteLine()
        {
            Console.WriteLine();
            _stringBuilder.AppendLine();
        }

        public static void WriteLine(string line)
        {
            Console.WriteLine(line);
            _stringBuilder.AppendLine(line);
        }

        public static void WriteLine(string line, params object[] args)
        {
            Console.WriteLine(line, args);
            _stringBuilder.AppendFormat(line, args);
            _stringBuilder.AppendLine();
        }

        public static void Write(string text)
        {
            Console.Write(text);
            _stringBuilder.Append(text);
        }

        public static void Flush()
        {
            DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
            di.CreateSubdirectory("Output");
            string fileName = String.Format("Output\\{0}.txt", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            File.WriteAllText(fileName, _stringBuilder.ToString());
            Process.Start(fileName);
        }

        public static void Clear()
        {
            Console.Clear();
            _stringBuilder = new StringBuilder();
        }
    }
}
