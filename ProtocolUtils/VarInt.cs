using System;
using System.IO;
using System.Runtime.Intrinsics.Arm;

namespace CraftCore.ProtocolUtils
{
    public static class VarInt
    {
        public static int ReadVarInt(Stream stream)
        {
            int numRead = 0;
            int result = 0;
            byte read;

            do
            {
                read = (byte)stream.ReadByte();
                int value = read & 0b01111111;
                result |= value << (7 * numRead);

                numRead++;
                if (numRead > 5) throw new Exception("VarInt is too big");
            } while ((read & 0b10000000) != 0);

            return result;
        }

        public static int ReadVarInt(byte[] data, int offset, out int bytesRead)
        {
            int numRead = 0;
            int result = 0;
            byte read;
            bytesRead = 0;

            do
            {
                if (numRead + offset >= data.Length)
                {
                    throw new Exception("VarInt data is incomplete.");
                }

                read = data[numRead + offset];
                int value = read & 0b01111111;
                result |= value << (7 * numRead);

                numRead++;
                if (numRead > 5) throw new Exception("VarInt is too big");
            } while ((read & 0b10000000) != 0);

            bytesRead = numRead; // Return how many bytes were read
            return result;
        }


        public static int GetVarIntSize(int value)
        {
            int size = 0;
            do
            {
                size++;
                value >>= 7;
            } while (value != 0);
            return size;
        }

        public static int WriteVarInt(byte[] buffer, int offset, int value)
        {
            int bytesWritten = 0;

            do
            {
                byte temp = (byte)(value & 0b01111111);
                value >>= 7;

                if (value != 0)
                {
                    temp |= 0b10000000; // Set the continuation bit
                }

                buffer[offset + bytesWritten++] = temp;
            } while (value != 0);

            return bytesWritten;
        }

        public static int WriteVarInt(MemoryStream memoryStream, int value)
        {
            int bytesWritten = 0;


            do
            {
                byte temp = (byte)(value & 0b01111111);
                value >>= 7;

                if (value != 0)
                {
                    temp |= 0b10000000; // Set the continuation bit
                }

                memoryStream.WriteByte(temp);
                bytesWritten++;
            } while (value != 0);

            return bytesWritten;

        }
    }
}
