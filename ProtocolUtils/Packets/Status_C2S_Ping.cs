using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils.Packets
{
    public class Status_C2S_Ping : Packet
    {
        public long Payload;

        public Status_C2S_Ping(byte[] buffer, int offset)
        {
            Type = (int)PacketTypeStatus.Status_C2S_Ping;

            int bytesRead = 0;
            Payload = Long.ReadLong(buffer, offset, out bytesRead);
        }

    }
}
