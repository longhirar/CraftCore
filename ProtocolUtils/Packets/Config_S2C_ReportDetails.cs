using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils.Packets
{
    public class Config_S2C_ReportDetails : Packet
    {
        public byte[] ToBytes() {
            using (var stream = new MemoryStream())
            {
                VarInt.WriteVarInt(stream, (int)PacketTypeConfig.Config_S2C_ReportDetails);
                VarInt.WriteVarInt(stream, 1); // array size
                VarString.WriteVarString(stream, "CraftCore");
                VarString.WriteVarString(stream, Dispatcher.GetInstance().config.Version);

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
