using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils
{

    // All packet types are in the following format:
    // Direction_Name
    //
    // All values are in CamelCase
    // If the ProtocolState isn't play, then refer to the
    // file for that state, and prefix the name:
    // ProtocolState_Direction_Name

    public enum PacketType
    {
        S2C_BundleDelimiter = 0x00,
        S2C_SpawnEntity = 0x01,
        S2C_Plugin = 0x19,
        S2C_Login = 0x2B
    }
}
