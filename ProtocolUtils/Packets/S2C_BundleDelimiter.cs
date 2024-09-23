using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils.Packets
{
    public class S2C_BundleDelimiter : Packet
    {
        public S2C_BundleDelimiter()
        {
            Type = (int)PacketType.S2C_BundleDelimiter;
        }

        public byte[] ToBytes()
        {
            using (var stream = new MemoryStream())
            {
                VarInt.WriteVarInt(stream, (int)PacketType.S2C_BundleDelimiter);

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
