using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils.Packets
{
    public class Config_C2S_Plugin : Packet
    {
        public string Identifier;
        public byte[] Data;

        public Config_C2S_Plugin(byte[] buffer, int offset, int length)
        {
            Type = (int)PacketTypeConfig.Config_C2S_Plugin;

            Identifier = VarString.ReadVarString(buffer, offset, out int idBytesRead);
            offset += idBytesRead;

            Data = new byte[length - offset];

            Buffer.BlockCopy(buffer, offset, Data, 0, length - offset);
        }
    }
}
