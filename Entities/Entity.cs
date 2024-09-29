using CraftCore.ProtocolUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.Entities
{

    public enum EntityType
    {
        Player = 122
    }

    public enum EntityPose
    {
        Standing,
        FallFlying,
        Sleeping,
        Swimming,
        SpinAttack,
        Sneaking,
        LongJumping,
        Dying,
        Croaking,
        UsingTongue,
        Sitting,
        Roaring,
        Sniffing,
        Emerging,
        Digging
    }

    public abstract class Entity
    {
        public int ID { get; set; }
        public Guid UUID { get; private set; }
        public EntityType Type {  get; private set; }
        public Vector3 Position { get; set; }
        public Vector3 Velocity = Vector3.Zero;
        // Note for Rotation
        // X = Pitch
        // Y = Yaw
        public Vector2 Rotation = Vector2.Zero;
        public double HeadYaw = 0.0;
        public bool IsOnFire = false;
        public bool IsCrouching = false;
        public bool IsSprinting = false;
        public bool IsSwimming = false;
        public bool IsInvisible = false;
        public bool IsGlowing = false;
        public bool IsFlyingElytra = false;
        public int AirTicks = 300;
        public string? CustomName = null;
        public bool IsCustomNameVisible = false;
        public bool IsSilent = false;
        public bool NoGravity = false;
        public EntityPose Pose = EntityPose.Standing;
        public int TicksInPowderSnow = 0;

        public Entity(int iD, Guid uUID, EntityType type, Vector3 position)
        {
            ID = iD;
            UUID = uUID;
            Type = type;
            Position = position;
        }

        public byte[] ComposeMetadata()
        {
            using (var stream = new MemoryStream())
            {
                byte status = 0x00;
                if (IsOnFire) status |= 0x01; // 00000001
                if (IsCrouching) status |= 0x02; // 00000010
                // Unused 
                if (IsSprinting) status |= 0x08; // 00001000
                if (IsSwimming) status |= 0x10; // 00010000
                if (IsInvisible) status |= 0x20; // 00100000
                if (IsGlowing) status |= 0x40; // 01000000
                if (IsFlyingElytra) status |= 0x80; // 10000000
                stream.WriteByte(status);

                VarInt.WriteVarInt(stream, AirTicks);
                
                if(CustomName != null)
                {
                    stream.WriteByte(0x01);
                    VarString.WriteVarString(stream, CustomName);
                } else
                {
                    stream.WriteByte(0x00);
                }

                stream.WriteByte(IsCustomNameVisible ? (byte)0x01 : (byte)0x00);
                stream.WriteByte(IsSilent ? (byte)0x01 : (byte)0x00);
                stream.WriteByte(NoGravity ? (byte)0x01 : (byte)0x00);
                VarInt.WriteVarInt(stream, (int)Pose);
                VarInt.WriteVarInt(stream, TicksInPowderSnow);

                stream.Write(ComposeChildrenMetadata());

                return stream.ToArray();
            }
        }

        public abstract byte[] ComposeChildrenMetadata();
    }
}
