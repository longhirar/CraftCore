using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils.Packets
{
    public class Config_S2C_KnownPacks : Packet
    {

        public DataPack[] packs;

        public Config_S2C_KnownPacks(DataPack[] packs)
        {
            this.packs = packs;
        }

        public byte[] ToBytes()
        {
            using (var stream = new MemoryStream())
            {
                VarInt.WriteVarInt(stream, (int)PacketTypeConfig.Config_S2C_KnownPacks);

                VarInt.WriteVarInt(stream, packs.Length); // knownPacks count
                foreach (var pack in packs)
                {
                    VarString.WriteVarString(stream, pack.Namespace);
                    VarString.WriteVarString(stream, pack.ID);
                    VarString.WriteVarString(stream, pack.Version);
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
