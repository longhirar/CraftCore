
using System.Text;

namespace CraftCore.ProtocolUtils
{
    // This type isn't actually called VarString in the minecraft protocol,
    // but to differentiate between C#'s String, we are calling it this.
    public static class VarString
    {
        public static string ReadVarString(byte[] buffer, int offset, out int bytesRead)
        {
            int length = VarInt.ReadVarInt(buffer, offset, out int varIntBytesRead);

            // Calculate where the actual string starts
            int stringStartIndex = offset + varIntBytesRead;

            if (buffer.Length < stringStartIndex + length)
                throw new Exception("Buffer does not contain enough data for the string.");

            // Read the string data
            string result = Encoding.UTF8.GetString(buffer, stringStartIndex, length);

            bytesRead = varIntBytesRead + length; // total bytes read: length of VarInt + string length

            return result;
        }

        public static int GetVarStringSize(string value)
        {
            byte[] stringBytes = Encoding.UTF8.GetBytes(value);
            int length = stringBytes.Length;
            int varIntSize = VarInt.GetVarIntSize(length);
            return varIntSize + length;
        }

        public static void WriteVarString(byte[] buffer, int offset, string value)
        {
            // Convert the string to a byte array using UTF-8 encoding
            byte[] stringBytes = Encoding.UTF8.GetBytes(value);
            int length = stringBytes.Length;

            // Calculate the total size needed for the VarInt and the string
            int varIntSize = VarInt.GetVarIntSize(length);

            // Check if the buffer has enough space
            if (buffer.Length < offset + varIntSize + length)
            {
                throw new Exception("Buffer does not have enough space to write the VarString.");
            }

            // Write the length of the string as a VarInt
            offset += VarInt.WriteVarInt(buffer, offset, length);

            // Write the string bytes
            Buffer.BlockCopy(stringBytes, 0, buffer, offset, length);
        }

        public static void WriteVarString(MemoryStream stream, string value)
        {
            // Convert the string to a byte array using UTF-8 encoding
            byte[] stringBytes = Encoding.UTF8.GetBytes(value);

            // Write the length of the string as a VarInt
            VarInt.WriteVarInt(stream, stringBytes.Length);


            stream.Write(stringBytes);
           
        }

    }

}
