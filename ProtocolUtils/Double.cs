using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftCore.ProtocolUtils
{
    public class Double
    {
        public static void WriteDouble(Stream stream, double value)
        {
            // Convert the double to bytes
            byte[] doubleBytes = BitConverter.GetBytes(value);

            // Ensure correct endianness (if needed)
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(doubleBytes); // Reverse to big-endian
            }

            // Write the bytes to the stream
            stream.Write(doubleBytes, 0, doubleBytes.Length);
        }
        public static double ReadDouble(Stream stream)
        {
            byte[] doubleBytes = new byte[8]; // A double is 8 bytes

            // Read 8 bytes from the stream
            int bytesRead = stream.Read(doubleBytes, 0, 8);

            if (bytesRead != 8)
            {
                throw new Exception("Failed to read 8 bytes for a double value.");
            }

            // Ensure correct endianness (if needed)
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(doubleBytes); // Reverse to big-endian if needed
            }

            // Convert bytes to double and return the result
            return BitConverter.ToDouble(doubleBytes, 0);
        }

    }
}
