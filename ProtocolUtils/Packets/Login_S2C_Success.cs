using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils.Packets
{
    public class Login_S2C_Success : Packet
    {
        public Guid UUID;
        public string Username;

        public byte[] ToBytes()
        {
            using (var memoryStream = new MemoryStream())
            {
                // Write the Packet ID for Login_S2C_Success
                VarInt.WriteVarInt(memoryStream, (int)PacketTypeLogin.Login_S2C_Success);

                // Write UUID (16 bytes)
                McUUID.WriteUUID(memoryStream, UUID);

                // Write Username (VarString - length-prefixed)
                VarString.WriteVarString(memoryStream, Username);

                // Write the VarInt 0
                VarInt.WriteVarInt(memoryStream, 0);

                // Write the terminating byte 0x01
                memoryStream.WriteByte(0x01);

                // Convert the body to a byte array
                byte[] packetBody = memoryStream.ToArray();

                // Now write the packet size (as a VarInt)
                using (var finalStream = new MemoryStream())
                {
                    // Write packet size as VarInt (size of packetBody)
                    VarInt.WriteVarInt(finalStream, packetBody.Length);

                    // Write the packetBody
                    finalStream.Write(packetBody, 0, packetBody.Length);

                    // Return the final byte array
                    return finalStream.ToArray();
                }
            }
        }

    }
}
