using CraftCore.Registries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.World
{
    public class Dimension : IRegistryItem
    {
        public int ID { get; private set; }
        public string Identifier { get; private set; }
        public bool HasNBTData => false;

        public byte[] NBTToBytes()
        {
            throw new NotImplementedException();
        }

        public void Register(int ID, string Identifier)
        {
            this.ID = ID;
            this.Identifier = Identifier;
        }
    }
}
