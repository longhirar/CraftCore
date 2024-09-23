using CraftCore.ProtocolUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fNbt;
using System.Numerics;

namespace CraftCore.Entities
{
    public class Player : LivingEntity
    {
        public float AdditionalHearts = 0;
        public int Score = 0;
        public string Username = "Player";
        public byte SkinParts = 0;
        public ClientMainHand MainHand = ClientMainHand.Right;

        public Player(int iD, Guid uUID, Vector3 position, string username, byte skinParts, ClientMainHand mainHand) : base(iD, uUID, EntityType.Player, position )
        {
            Username = username;
            SkinParts = skinParts;
            MainHand = mainHand;
        }

        public override byte[] ComposeLivingChildrenMetadata()
        {
            using (var stream = new MemoryStream())
            {
                Float.WriteFloat(stream, AdditionalHearts);
                VarInt.WriteVarInt(stream, Score);
                stream.WriteByte(SkinParts);
                stream.WriteByte((byte)MainHand);
                NbtWriter leftShoulderData = new NbtWriter(stream, "");
                leftShoulderData.Finish();
                NbtWriter rightShoulderData = new NbtWriter(stream, "");
                rightShoulderData.Finish();
                

                return stream.ToArray();
            }
        }
    }
}
