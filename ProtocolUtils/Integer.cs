using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils
{
    public class Integer
    {
        public static void WriteInt(Stream stream, int value)
        {
            // Convert the integer to bytes in big-endian order (most significant byte first)
            byte[] buffer = new byte[4];

            buffer[0] = (byte)((value >> 24) & 0xFF); // Most significant byte
            buffer[1] = (byte)((value >> 16) & 0xFF);
            buffer[2] = (byte)((value >> 8) & 0xFF);
            buffer[3] = (byte)(value & 0xFF);         // Least significant byte

            // Write the 4-byte integer to the stream
            stream.Write(buffer, 0, buffer.Length);
        }

    }
}
