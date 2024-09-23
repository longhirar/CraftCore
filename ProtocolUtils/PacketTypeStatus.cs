using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils
{
    public enum PacketTypeStatus
    {
        // serverbound
        Status_C2S_Request = 0x00,
        Status_C2S_Ping = 0x01,

        // clientbound
        Status_S2C_Response = 0x00,
        Status_S2C_Pong = 0x01,

    }
}
