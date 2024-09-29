using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils.Packets
{
    public class Config_C2S_KnownPacks : Packet
    {

        public DataPack[] packs;


        public Config_C2S_KnownPacks(byte[] buffer, int offset)
        {
            Type = (int)PacketTypeConfig.Config_C2S_KnownPacks;

            int count = VarInt.ReadVarInt(buffer, offset, out int countBytesRead);
            offset += countBytesRead;

            packs = new DataPack[count];
            for (int i = 0; i < count; i++)
            {
                string ns = VarString.ReadVarString(buffer, offset, out int namespaceBytesRead);
                offset += namespaceBytesRead;
                string id = VarString.ReadVarString(buffer, offset, out int idBytesRead);
                offset += idBytesRead;
                string ver = VarString.ReadVarString(buffer, offset, out int verBytesRead);

                DataPack pack = new DataPack(ns, id, ver);
                packs.Append(pack);
            }
        }
    }
}
