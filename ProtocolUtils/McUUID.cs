using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils
{

    public class McUUID
    {
        public static Guid ReadUUID(byte[] buffer, int offset, out int bytesRead)
        {
            if (buffer.Length < offset + 16)
                throw new ArgumentException("Buffer does not contain enough data for a UUID.");

            bytesRead = 16;

            // UUID has a specific byte order (endianness) to follow for the first three components

            // Rearrange bytes to match the .NET Guid format
            byte[] reorderedBytes = new byte[16];

            // First 4 bytes (most significant 32 bits) need to be reversed
            reorderedBytes[0] = buffer[offset + 3];
            reorderedBytes[1] = buffer[offset + 2];
            reorderedBytes[2] = buffer[offset + 1];
            reorderedBytes[3] = buffer[offset];

            // Next 2 bytes (16 bits) need to be reversed
            reorderedBytes[4] = buffer[offset + 5];
            reorderedBytes[5] = buffer[offset + 4];

            // Next 2 bytes (16 bits) need to be reversed
            reorderedBytes[6] = buffer[offset + 7];
            reorderedBytes[7] = buffer[offset + 6];

            // The last 8 bytes (least significant 64 bits) remain as is
            reorderedBytes[8] = buffer[offset + 8];
            reorderedBytes[9] = buffer[offset + 9];
            reorderedBytes[10] = buffer[offset + 10];
            reorderedBytes[11] = buffer[offset + 11];
            reorderedBytes[12] = buffer[offset + 12];
            reorderedBytes[13] = buffer[offset + 13];
            reorderedBytes[14] = buffer[offset + 14];
            reorderedBytes[15] = buffer[offset + 15];

            // Convert the reordered byte array to a Guid
            return new Guid(reorderedBytes);
        }

        public static int WriteUUID(byte[] buffer, int offset, Guid value)
        {
            byte[] uuidBytes = value.ToByteArray();

            // Write in the correct order for the UUID
            // Reverse the first 4 bytes (most significant 32 bits)
            buffer[offset] = uuidBytes[3];
            buffer[offset + 1] = uuidBytes[2];
            buffer[offset + 2] = uuidBytes[1];
            buffer[offset + 3] = uuidBytes[0];

            // Reverse the next 2 bytes (16 bits)
            buffer[offset + 4] = uuidBytes[5];
            buffer[offset + 5] = uuidBytes[4];

            // Reverse the next 2 bytes (16 bits)
            buffer[offset + 6] = uuidBytes[7];
            buffer[offset + 7] = uuidBytes[6];

            // The last 8 bytes remain as is
            buffer[offset + 8] = uuidBytes[8];
            buffer[offset + 9] = uuidBytes[9];
            buffer[offset + 10] = uuidBytes[10];
            buffer[offset + 11] = uuidBytes[11];
            buffer[offset + 12] = uuidBytes[12];
            buffer[offset + 13] = uuidBytes[13];
            buffer[offset + 14] = uuidBytes[14];
            buffer[offset + 15] = uuidBytes[15];

            // Always writes exactly 16 bytes
            return 16;
        }
        public static void WriteUUID(MemoryStream stream, Guid value)
        {
            byte[] uuidBytes = value.ToByteArray();

            // Write UUID in correct big-endian format
            stream.WriteByte(uuidBytes[3]);
            stream.WriteByte(uuidBytes[2]);
            stream.WriteByte(uuidBytes[1]);
            stream.WriteByte(uuidBytes[0]);

            stream.WriteByte(uuidBytes[5]);
            stream.WriteByte(uuidBytes[4]);

            stream.WriteByte(uuidBytes[7]);
            stream.WriteByte(uuidBytes[6]);

            // Last 8 bytes as is
            stream.Write(uuidBytes, 8, 8);
        }
    }
}
