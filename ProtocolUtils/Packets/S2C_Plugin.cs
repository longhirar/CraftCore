using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils.Packets
{
    public class S2C_Plugin : Packet
    {
        public string Identifier;
        public byte[] Data;

        public byte[] ToBytes()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                VarInt.WriteVarInt(stream, (int)PacketType.S2C_Plugin);
                VarString.WriteVarString(stream, Identifier);
                stream.Write(Data);


                using (MemoryStream finalStream = new MemoryStream())
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
