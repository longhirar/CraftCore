using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils.Packets
{
    public class Login_C2S_Start : Packet
    {
        public string Username;
        public Guid UUID;

        public Login_C2S_Start(byte[] buffer, int offset)
        {
            Type = (int)PacketTypeLogin.Login_C2S_Start;
            Username = VarString.ReadVarString(buffer, offset, out int usernameBytesRead);
            offset += usernameBytesRead;
            UUID = McUUID.ReadUUID(buffer, offset, out int uuidBytesRead);
        }
    }
}
