using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils
{
    public enum PacketTypeLogin
    {
        // serverbound
        Login_C2S_Start = 0x00,
        Login_C2S_Ack = 0x03,

        // clientbound
        Login_S2C_Success = 0x02,
    }
}
