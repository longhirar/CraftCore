
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using CraftCore.ProtocolUtils;

namespace CraftCore.Entities
{
    public enum Hand {
        Main,
        Offhand
    }

    public abstract class LivingEntity : Entity
    {
        public bool IsHandActive = false;
        public Hand ActiveHand = Hand.Main;
        public bool IsInRiptideSpinAttack = false;
        public float Health;
        public int PotionEffectColor = 0;
        public bool IsAmbientEffect = false;
        public int NumberOfArrows = 0;
        public int NumberOfBeeStings = 0;
        public Vector3? BedLocation;

        public LivingEntity(int iD, Guid uUID, EntityType type, Vector3 position) : base(iD, uUID, type, position)
        {
            
        }

        public override byte[] ComposeChildrenMetadata()
        {
            using (var stream = new MemoryStream())
            {
                byte handStatus = 0x00;
                if (IsHandActive) handStatus |= 0b0001; // 0x1
                handStatus |= (ActiveHand == Hand.Main) ? (byte) 0b0010 : (byte) 0b0000; // 0x2
                if (IsInRiptideSpinAttack) handStatus |= 0b0100; // 0x4
                stream.WriteByte(handStatus);
                Float.WriteFloat(stream, Health);
                VarInt.WriteVarInt(stream, PotionEffectColor);
                stream.WriteByte( IsAmbientEffect ? (byte)0x01 : (byte)0x00);
                VarInt.WriteVarInt(stream, NumberOfArrows);
                VarInt.WriteVarInt(stream, NumberOfBeeStings);
                if (BedLocation != null)
                {
                    // TODO: Bed Location when sleeping
                    throw new NotImplementedException();
                } else
                {
                    stream.WriteByte(0x00);
                }

                stream.Write(ComposeLivingChildrenMetadata());

                return stream.ToArray();
            }

        }

        public abstract byte[] ComposeLivingChildrenMetadata();
    }
}
