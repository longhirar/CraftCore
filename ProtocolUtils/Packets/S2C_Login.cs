using CraftCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils.Packets
{
    internal class S2C_Login : Packet
    {
        public Player player;
        public byte[] ToBytes() {
            using (var stream = new MemoryStream())
            {
                Integer.WriteInt(stream, player.ID);
                stream.WriteByte(0x00); // IsHardcore
                VarInt.WriteVarInt(stream, 1); // dimension array size
                VarString.WriteVarString(stream, "craftcore:overworld");
                VarInt.WriteVarInt(stream, 8); // Render Distance
                VarInt.WriteVarInt(stream, 8); // Simulation Distance
                stream.WriteByte(0x00); // Reduced Debug Info
                stream.WriteByte(0x01); // Enabled Respawn Screen
                stream.WriteByte(0x00); // doLimitedCrafting
                stream.WriteByte(0x00); // dimension ID
                VarString.WriteVarString(stream, "craftcore:overworld");
                byte[] longBuf = new byte[8];
                Long.WriteLong(longBuf, 0, 0); // hash seed
                stream.WriteByte(0x03); // spectator game mode
                stream.WriteByte(0x03); // previous game mode
                stream.WriteByte(0x00); // isDebugWorld
                stream.WriteByte(0x00); // isFlat
                stream.WriteByte(0x00); // Has Death Location
                stream.WriteByte(0x00); // death dimension
                stream.WriteByte(0x00); // death location
                VarInt.WriteVarInt(stream, 0); // portal cooldown
                stream.WriteByte(0x00); // enforce secure chat

                using (var finalStream = new MemoryStream())
                {
                    byte[] content = stream.ToArray();
                    VarInt.WriteVarInt(finalStream, content.Length);
                    finalStream.Write(content);
                    return finalStream.ToArray();
                }
            }
        }
    }
}
