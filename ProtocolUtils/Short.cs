using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils
{
    public static class Short
    {
        public static short ReadShort(byte[] buffer, int offset, out int bytesRead)
        {
            if (buffer.Length < offset + 2)
                throw new Exception("Not enough bytes to read a short");

            // Read two bytes and convert them to a short (big endian)
            bytesRead = 2;
            return (short)((buffer[offset] << 8) | buffer[offset + 1]);
        }

        public static short ReadShort(Stream stream)
        {
            byte[] shortBytes = new byte[2]; // A short is 2 bytes

            // Read 2 bytes from the stream
            int bytesRead = stream.Read(shortBytes, 0, 2);

            if (bytesRead != 2)
            {
                throw new Exception("Failed to read 2 bytes for a short value.");
            }

            // Ensure correct endianness (if needed)
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(shortBytes); // Reverse to big-endian if needed
            }

            // Convert bytes to short and return the result
            return BitConverter.ToInt16(shortBytes, 0);
        }

        public static void WriteShort(Stream stream, short value)
        {
            // Convert the short to bytes
            byte[] shortBytes = BitConverter.GetBytes(value);

            // Ensure correct endianness (if needed)
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(shortBytes); // Reverse to big-endian
            }

            // Write the bytes to the stream
            stream.Write(shortBytes, 0, shortBytes.Length);
        }


    }
}
