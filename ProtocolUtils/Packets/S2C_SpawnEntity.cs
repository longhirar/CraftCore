using CraftCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils.Packets
{
    public class S2C_SpawnEntity : Packet
    {
        public Entity entity;

        public byte[] ToBytes()
        {
            using (var stream = new MemoryStream())
            {
                VarInt.WriteVarInt(stream, (int)PacketType.S2C_SpawnEntity);
                VarInt.WriteVarInt(stream, entity.ID);
                McUUID.WriteUUID(stream, entity.UUID);
                VarInt.WriteVarInt(stream, (int)entity.Type);
                Double.WriteDouble(stream, entity.Position.X);
                Double.WriteDouble(stream, entity.Position.Y);
                Double.WriteDouble(stream, entity.Position.Z);
                stream.WriteByte((byte) (255 * (entity.Rotation.X % 360)));
                stream.WriteByte((byte) (255 * (entity.Rotation.Y % 360)));
                stream.WriteByte((byte) (255 * (entity.HeadYaw % 360)));
                VarInt.WriteVarInt(stream, 0);
                Short.WriteShort(stream, (short)entity.Velocity.X);
                Short.WriteShort(stream, (short)entity.Velocity.Y);
                Short.WriteShort(stream, (short)entity.Velocity.Z);

                using (var finalStream =  new MemoryStream())
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
