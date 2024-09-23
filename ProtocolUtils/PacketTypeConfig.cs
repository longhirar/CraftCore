using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils
{
    public enum PacketTypeConfig
    {
        // serverbound
        Config_C2S_Information = 0x00,
        Config_C2S_Plugin = 0x02,
        Config_C2S_FinishConfigAck = 0x03,
        // clientbound
        Config_S2C_Plugin = 0x01,
        Config_S2C_ReportDetails = 0x0F,
        Config_S2C_FinishConfig = 0x03
    }
}
