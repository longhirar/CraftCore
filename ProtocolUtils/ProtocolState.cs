using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils
{
    public enum ProtocolState
    {
        Handshake,
        Status,
        Login,
        Configuration,
        Play
    }
}
