using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils.Packets
{


    public class Status_S2C_Response {
    

        //public string StatusJSON = "{\"version\":{\"name\":\"1.21.1\",\"protocol\":767},\"players\":{\"max\":100,\"online\":5,\"sample\":[{\"name\":\"thinkofdeath\",\"id\":\"4566e69f-c907-48ee-8d71-d7ba5aa00d20\"}]},\"description\":{\"text\":\"A CraftCore Minecraft Server\"},\"favicon\":\"data:image/png;base64,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\",\"enforcesSecureChat\":false,\"preventsChatReports\":true}";

        public byte[] ToBytes()
        {
            string statusJSON = GenerateStatusJSON();
            
            // Calculate the sizes of the components
            int idSize = VarInt.GetVarIntSize((int)PacketTypeStatus.Status_S2C_Response);
            int payloadSize = VarString.GetVarStringSize(statusJSON);
            int totalSize = VarInt.GetVarIntSize(idSize + payloadSize) + idSize + payloadSize;

            // Create the byte array
            byte[] buffer = new byte[totalSize];

            // Write the total size as a VarInt
            int offset = 0;
            int varIntSize = VarInt.WriteVarInt(buffer, offset, idSize + payloadSize);
            offset += varIntSize;

            // Write the packet ID as a VarInt
            varIntSize = VarInt.WriteVarInt(buffer, offset, (int)PacketTypeStatus.Status_S2C_Response);
            offset += varIntSize;

            // Write the Payload as a long
            VarString.WriteVarString(buffer, offset, statusJSON);

            return buffer;
        }

        private string GenerateStatusJSON()
        {

            // Prepare the server status object
            var serverStatus = new
            {
                version = new
                {
                    name = "1.21.1",
                    protocol = 767
                },
                players = new
                {
                    max = Dispatcher.GetInstance().config.MaxPlayers,
                    online = Dispatcher.GetInstance().sessions.Count
                },
                description = new
                {
                    text = Dispatcher.GetInstance().config.StatusMessage // Keep special characters
                },
                favicon = Dispatcher.GetInstance().config.Favicon,
                enforcesSecureChat = false,
                preventsChatReports = true
            };

            // Serialize the object to JSON
            return JsonConvert.SerializeObject(serverStatus, Formatting.Indented);
        }
    }
}
