using System.Collections.Generic;
using System.IO;
using System.Linq;
using PIN.Core.Packages;

namespace PIN.Core
{
    class Scanner
    {

        public List<IAP> Scan()
        {
            return Directory.GetFiles(Directory.GetCurrentDirectory(), "*.iap", SearchOption.AllDirectories).Select(file => new IAP(file)).ToList();
        }

        public string[] Find(string name)
        {
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), name, SearchOption.AllDirectories);
            return files;
        }
    }
}
