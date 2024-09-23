using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils.Packets
{
    public class Config_S2C_FinishConfig : Packet
    {
        public byte[] ToBytes()
        {
            using (var stream = new MemoryStream())
            {
                VarInt.WriteVarInt(stream, (int)PacketTypeConfig.Config_S2C_FinishConfig);

                using (var finalStream =  new MemoryStream())
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
