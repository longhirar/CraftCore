using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils.Packets
{
    public class Handshake_C2S_Intention : Packet
    {
        public int ProtocolVersion;
        public string ServerAddress;
        public short ServerPort;
        public int NextState;
        
        public Handshake_C2S_Intention(byte[] buffer, int offset)
        {
            Type = (int)PacketTypeHandshake.Handshake_C2S_Intention;

            ProtocolVersion = VarInt.ReadVarInt(buffer, offset, out int varIntBytesRead);
            offset += varIntBytesRead;
            
            ServerAddress = VarString.ReadVarString(buffer, offset, out int stringBytesRead);
            offset += stringBytesRead;

            ServerPort = Short.ReadShort(buffer, offset, out int shortBytesRead);
            offset += shortBytesRead;

            NextState = VarInt.ReadVarInt(buffer, offset, out varIntBytesRead);
            offset += varIntBytesRead;
        }
    }
}
