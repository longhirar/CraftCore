using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils.Packets
{
    public class Status_S2C_Pong : Packet
    {
        public long Payload;

        public byte[] ToBytes()
        {
            // Calculate the sizes of the components
            int idSize = VarInt.GetVarIntSize((int)PacketTypeStatus.Status_S2C_Pong);
            int payloadSize = 8; // A long is 8 bytes
            int totalSize = VarInt.GetVarIntSize(idSize + payloadSize) + idSize + payloadSize;

            // Create the byte array
            byte[] buffer = new byte[totalSize];

            // Write the total size as a VarInt
            int offset = 0;
            int varIntSize = VarInt.WriteVarInt(buffer, offset, idSize + payloadSize);
            offset += varIntSize;

            // Write the packet ID as a VarInt
            varIntSize = VarInt.WriteVarInt(buffer, offset, (int)PacketTypeStatus.Status_S2C_Pong);
            offset += varIntSize;

            // Write the Payload as a long
            Long.WriteLong(buffer, offset, Payload);

            return buffer;
        }
    }
}
