using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils.Packets
{
    public class Config_C2S_Information : Packet
    {
        public string Locale;
        public int ViewDistance;
        public ClientChatMode ChatMode;
        public bool ChatColors;
        public byte SkinParts;
        public ClientMainHand MainHand;
        public bool EnableTextFiltering;
        public bool AllowServerListing;

        public Config_C2S_Information(byte[] buffer, int offset)
        {
            Locale = VarString.ReadVarString(buffer, offset, out int localeBytesRead);
            offset += localeBytesRead;
            ViewDistance = (int)buffer[offset];
            offset += 1;
            ChatMode = (ClientChatMode)VarInt.ReadVarInt(buffer, offset, out int chatModeBytesRead);
            offset += chatModeBytesRead;
            ChatColors = buffer[offset] == 0x01;
            offset += 1;
            SkinParts = buffer[offset];
            offset += 1;
            MainHand = (ClientMainHand)VarInt.ReadVarInt(buffer, offset, out int mainHandBytesRead);
            offset += mainHandBytesRead;
            EnableTextFiltering = buffer[offset] == 0x01;
            offset += 1;
            AllowServerListing = buffer[offset] == 0x01;
            offset += 1;



        }
    }
}
