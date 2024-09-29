using CraftCore.Registries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CraftCore.ProtocolUtils.Packets
{
    public class Config_S2C_RegistryData<T> : Packet where T : IRegistryItem
    {
        public Registry<T> Registry;

        public Config_S2C_RegistryData(Registry<T> Registry)
        {
            this.Registry = Registry;
        }

        public byte[] ToBytes()
        {
            using (var stream = new MemoryStream())
            {
                VarInt.WriteVarInt(stream, (int)PacketTypeConfig.Config_S2C_RegistryData);

                Dictionary<string, T> registry = Registry.GetDictionary();

                VarString.WriteVarString(stream, Registry.identifier);
                VarInt.WriteVarInt(stream, registry.Count);
                foreach (KeyValuePair<string, T> entry in registry)
                {
                    VarString.WriteVarString(stream, entry.Key);
                    stream.WriteByte(entry.Value.HasNBTData ? (byte)0x01 : (byte)0x00);
                    if(entry.Value.HasNBTData)
                    {
                        stream.Write(entry.Value.NBTToBytes());
                    }
                }

                using (var finalStream = new MemoryStream())
                {
                    byte[] content = stream.ToArray();
                    VarInt.WriteVarInt(finalStream, content.Length);
                    finalStream.Write(content);
                    return finalStream.ToArray();
                }
            }
        }
    }
}
