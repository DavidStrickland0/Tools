using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NunitTestFinder
{
    class AssemblyManager
    {
        public static IEnumerable<string> GetNunitTestAssemblies(string directoryPath,bool recursive = true)
        {
            string assemblyNameToSearchFor = "nunit.framework";
            if (!System.IO.Directory.Exists(directoryPath))
            {
                directoryPath = Directory.GetCurrentDirectory();
            }
            System.IO.SearchOption option = System.IO.SearchOption.AllDirectories;
            if (!recursive) option = System.IO.SearchOption.TopDirectoryOnly;
            string[] dllFiles = System.IO.Directory.GetFiles(directoryPath, "*.dll", option);
            string[] exeFiles = System.IO.Directory.GetFiles(directoryPath, "*.exe", option);
            var files = new List<string>();
            CheckFiles(assemblyNameToSearchFor, dllFiles, files);
            CheckFiles(assemblyNameToSearchFor, exeFiles, files);
            return files;
        }

        private static void CheckFiles(string assemblyNameToSearchFor, string[] dllDirectories, List<string> files)
        {
            IEnumerator dllEnum = dllDirectories.GetEnumerator();
            while (dllEnum.MoveNext())
            {
                try
                {
                    List<Guid> addedAssemblies = new List<Guid>();
                    Assembly dll = Assembly.ReflectionOnlyLoadFrom(dllEnum.Current.ToString());
                    AssemblyName name = dll.GetReferencedAssemblies().Where<AssemblyName>((a) => (a.Name == assemblyNameToSearchFor)).FirstOrDefault<AssemblyName>();
                    if (name != null)
                    {
                    if(!addedAssemblies.Contains(dll.GetType().GUID)) 
                    {
                        addedAssemblies.Add(dll.GetType().GUID);
                        files.Add(dll.Location);
                    }
                    }
                }
                catch (FileLoadException) { }
                catch (FileNotFoundException) { }
                catch (BadImageFormatException) { }
                //if an exception is thrown while loading the file we dont want to include it but the exception its self is not relavent and is thus ignored.
            };
        }
    }
}
