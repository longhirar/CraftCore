using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils
{
    public class DataPack
    {
        public string Namespace;
        public string ID;
        public string Version;

        public DataPack(string Namespace, string ID, string Version) {
            this.Namespace = Namespace;
            this.ID = ID;
            this.Version = Version;
        }
    }
}
