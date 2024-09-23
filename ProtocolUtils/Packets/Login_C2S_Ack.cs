using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils.Packets
{
    public class Login_C2S_Ack : Packet
    {
        public Login_C2S_Ack(byte[] buffer, int offset) {
            Type = (int)PacketTypeLogin.Login_C2S_Ack;
        }
    }
}
