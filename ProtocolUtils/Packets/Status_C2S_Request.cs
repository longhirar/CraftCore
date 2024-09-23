using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils.Packets
{
    public class Status_C2S_Request : Packet
    {
        public Status_C2S_Request(byte[] buffer, int offset) {
            Type = (int)PacketTypeStatus.Status_C2S_Request;
        }
    }
}
