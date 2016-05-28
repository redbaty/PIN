using System.Collections.Generic;
using System.IO;
using System.Linq;
using PIN.Core.Packages;

namespace PIN.Core.Misc
{
    class Scanner
    {

        public List<Package> Scan()
        {
            return Directory.GetFiles(Directory.GetCurrentDirectory(), $"*{Package.FileType}", SearchOption.AllDirectories).Select(file => new Package(file)).ToList();
        }

        public string[] Find(string name)
        {
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), name, SearchOption.AllDirectories);
            return files;
        }
    }
}
