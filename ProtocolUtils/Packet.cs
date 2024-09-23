using CraftCore.ProtocolUtils.Packets;
using System;
using System.IO;
using System.Net.Sockets;

namespace CraftCore.ProtocolUtils
{
    public abstract class Packet
    {

        public int Type;

        public static Packet ReadPacket(NetworkStream stream, ProtocolState state)
        {
            // Read the packet length (VarInt)
            int length = VarInt.ReadVarInt(stream);
            if (length <= 0) return null;

            byte[] buffer = new byte[length];
            int bytesRead = 0;

            while (bytesRead < length)
            {
                bytesRead += stream.Read(buffer, bytesRead, length - bytesRead);
            }

            // buffer;
            switch (state)
            {
                case ProtocolState.Handshake:
                    return ParsePacketHandshake(buffer);
                case ProtocolState.Status:
                    return ParsePacketStatus(buffer);
                case ProtocolState.Login:
                    return ParsePacketLogin(buffer);
                case ProtocolState.Configuration:
                    return ParsePacketConfig(buffer, length);
                default:
                    throw new ArgumentOutOfRangeException(nameof(state));
            }
            
        }

        private static Packet ParsePacketHandshake(byte[] buffer)
        {
            int offset = 0;
            PacketTypeHandshake packetType = (PacketTypeHandshake) VarInt.ReadVarInt(buffer, offset, out int packetIDBytesRead);
            offset += packetIDBytesRead;

            switch (packetType)
            {
                case PacketTypeHandshake.Handshake_C2S_Intention:
                    return new Handshake_C2S_Intention(buffer, offset);
                default:
                    throw new Exception($"Unexpected Packet: {packetType}");
            }
        }
        private static Packet ParsePacketStatus(byte[] buffer)
        {
            int offset = 0;
            PacketTypeStatus packetType = (PacketTypeStatus)VarInt.ReadVarInt(buffer, offset, out int packetIDBytesRead);
            offset += packetIDBytesRead;

            switch (packetType)
            {
                case PacketTypeStatus.Status_C2S_Ping:
                    return new Status_C2S_Ping(buffer, offset);
                case PacketTypeStatus.Status_C2S_Request:
                    return new Status_C2S_Request(buffer, offset);
                default:
                    throw new Exception($"Unexpected Packet: {packetType}");
            }
        }
        private static Packet ParsePacketLogin(byte[] buffer)
        {
            int offset = 0;
            PacketTypeLogin packetType = (PacketTypeLogin)VarInt.ReadVarInt(buffer, offset, out int packetIDBytesRead);
            offset += packetIDBytesRead;

            switch (packetType)
            {
                case PacketTypeLogin.Login_C2S_Start:
                    return new Login_C2S_Start(buffer, offset);
                case PacketTypeLogin.Login_C2S_Ack:
                    return new Login_C2S_Ack(buffer, offset);
                default:
                    throw new Exception($"Unexpected Packet: {packetType}");
            }
        }

        private static Packet ParsePacketConfig(byte[] buffer, int length)
        {
            int offset = 0;
            PacketTypeConfig packetType = (PacketTypeConfig)VarInt.ReadVarInt(buffer, offset, out int packetIDBytesRead);
            offset += packetIDBytesRead;


            switch(packetType)
            {
                case PacketTypeConfig.Config_C2S_Plugin:
                    return new Config_C2S_Plugin(buffer, offset, length);
                case PacketTypeConfig.Config_C2S_Information:
                    return new Config_C2S_Information(buffer, offset);
                case PacketTypeConfig.Config_C2S_FinishConfigAck:
                    return new Config_C2S_FinishConfigAck();
                default:
                    Log.Write($"Unexpected Packet: {packetType}", LogChannel.DEBUG);
                    throw new NotImplementedException();
            }
        }



        /*
            ABSTRACT STUFF
        */

    }
}
