using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils
{
    public class Float
    {
        public static void WriteFloat(Stream stream, float value)
        {
            // Convert the float to bytes
            byte[] floatBytes = BitConverter.GetBytes(value);

            // Ensure correct endianness (if needed)
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(floatBytes); // Reverse to big-endian
            }

            // Write the bytes to the stream
            stream.Write(floatBytes, 0, floatBytes.Length);
        }

        public static float ReadFloat(Stream stream)
        {
            byte[] floatBytes = new byte[4];

            // Read 4 bytes from the stream (a float is 4 bytes)
            int bytesRead = stream.Read(floatBytes, 0, 4);

            if (bytesRead != 4)
            {
                throw new Exception("Failed to read 4 bytes for a float value.");
            }

            // Ensure correct endianness (if needed)
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(floatBytes); // Reverse to big-endian if needed
            }

            // Convert bytes to float and return the result
            return BitConverter.ToSingle(floatBytes, 0);
        }


    }
}
