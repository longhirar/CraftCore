using System;
using System.IO;

namespace CraftCore.ProtocolUtils
{
    public static class VarLong
    {
        public static long ReadVarLong(Stream stream)
        {
            int numRead = 0;
            long result = 0;
            byte read;

            do
            {
                read = (byte)stream.ReadByte();
                long value = read & 0b01111111;
                result |= value << (7 * numRead);

                numRead++;
                if (numRead > 10) throw new Exception("VarLong is too big");
            } while ((read & 0b10000000) != 0);

            return result;
        }

        public static byte[] WriteVarLong(long value)
        {
            var buffer = new MemoryStream();
            do
            {
                byte temp = (byte)(value & 0b01111111);
                value >>= 7;
                if (value != 0)
                {
                    temp |= 0b10000000;
                }

                buffer.WriteByte(temp);
            } while (value != 0);

            return buffer.ToArray();
        }
    }
}
