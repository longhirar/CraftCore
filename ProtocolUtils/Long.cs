using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils
{
    public static class Long
    {
        public static long ReadLong(byte[] buffer, int offset, out int bytesRead)
        {
            if (buffer.Length < offset + 8)
            {
                throw new Exception("Not enough bytes to read a long");
            }

            // Read 8 bytes and combine them into a long (big endian)
            bytesRead = 8;
            return ((long)buffer[offset] << 56) |
                   ((long)buffer[offset + 1] << 48) |
                   ((long)buffer[offset + 2] << 40) |
                   ((long)buffer[offset + 3] << 32) |
                   ((long)buffer[offset + 4] << 24) |
                   ((long)buffer[offset + 5] << 16) |
                   ((long)buffer[offset + 6] << 8) |
                   buffer[offset + 7];
        }

        public static void WriteLong(byte[] buffer, int offset, long value)
        {
            if (buffer.Length < offset + 8)
            {
                throw new Exception("Not enough space in the buffer to write a long");
            }

            // Write the long value to the buffer in big-endian order
            buffer[offset] = (byte)(value >> 56);
            buffer[offset + 1] = (byte)(value >> 48);
            buffer[offset + 2] = (byte)(value >> 40);
            buffer[offset + 3] = (byte)(value >> 32);
            buffer[offset + 4] = (byte)(value >> 24);
            buffer[offset + 5] = (byte)(value >> 16);
            buffer[offset + 6] = (byte)(value >> 8);
            buffer[offset + 7] = (byte)value;
        }

    }
}
