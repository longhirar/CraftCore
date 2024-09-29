using CraftCore.ProtocolUtils.Packets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.Registries
{
    public interface IRegistryItem {
        public void Register(int ID, string Identifier);
        bool HasNBTData {  get; }
        public byte[] NBTToBytes();
    }


    public class Registry<T> where T : IRegistryItem
    {
        private Dictionary<string, T> registry;
        public string identifier { get; private set; }
        private int autoIncrement = 0;

        public int Count { get { return registry.Count; } }

        public Registry(string Identifier) {
            registry = new Dictionary<string, T>();
            identifier = Identifier;
        }

        public void Register(string identifier, T item)
        {
            if (registry.ContainsKey(identifier))
                throw new Exception("Identifier already registered");

            item.Register(autoIncrement++, identifier);
            registry[identifier] = item;
        }

        public Dictionary<string, T> GetDictionary() { return registry; }

        public Config_S2C_RegistryData<T> CreatePacket()
        {
            return new Config_S2C_RegistryData<T>(this);
        }



    }
}
