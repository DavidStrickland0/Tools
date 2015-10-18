using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NunitTestFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseDirectory = Directory.GetCurrentDirectory();
            IEnumerable<string> files = AssemblyManager.GetNunitTestAssemblies(baseDirectory);
            NunitConfigFactory.CreateConfig(files).Save("generatedconfig.nunit");
        }
    }
}
