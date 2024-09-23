using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils.Packets
{
    public class Config_C2S_FinishConfigAck : Packet
    {
        public Config_C2S_FinishConfigAck() {
            Type = (int)PacketTypeConfig.Config_C2S_FinishConfigAck;
        }
    }
}
