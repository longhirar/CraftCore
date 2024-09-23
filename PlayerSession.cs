using System;
using System.IO;
using System.Net.Sockets;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using CraftCore.Entities;
using CraftCore.ProtocolUtils;
using CraftCore.ProtocolUtils.Packets;

namespace CraftCore
{
    class PlayerSession
    {
        private TcpClient client;
        private NetworkStream stream;
        private ProtocolState state;
        private bool shouldDisconnect = false;

        public string? Username { get; private set; }
        public Guid? UUID { get; private set; }
        public string? ClientBrand { get; private set; }
        public string? Locale { get; private set; }
        public int? ViewDistance { get; private set; }
        public ClientChatMode? ChatMode { get; private set; }
        public bool? ChatColors { get; private set; }
        public bool? EnableTextFiltering { get; private set; }
        public bool? AllowServerListing { get; private set; }
        public Player player { get; private set; }
        


        public PlayerSession(TcpClient tcpClient)
        {
            this.client = tcpClient;
            this.stream = client.GetStream();
            this.state = ProtocolState.Handshake;
        }

        public void HandlePlayer()
        {

            while (client.Connected && !shouldDisconnect)
            {
                try
                {
                    Packet genericPacket = Packet.ReadPacket(stream, state);
                    switch (state)
                    {
                        case ProtocolState.Handshake:
                            ProcessPacketHandshake(genericPacket);
                            break;
                        case ProtocolState.Status:
                            ProcessPacketStatus(genericPacket);
                            break;
                        case ProtocolState.Login:
                            ProcessPacketLogin(genericPacket);
                            break;
                        case ProtocolState.Configuration:
                            ProcessPacketConfig(genericPacket);
                            break;
                        default:
                            throw new Exception("Invalid Protocol State");
                    }
                    
                }
                catch (Exception e)
                {
                    Log.Write($"Error handling player: {e.Message}");
                    break;
                }

                
            }

            if (Dispatcher.GetInstance().sessions.Contains(this))
            {
                Dispatcher.GetInstance().sessions.Remove(this);
            }
            client.Close();
            Log.Write($"Client {Username ?? "Unknown"} disconnected.");
        }

        private void ProcessPacketHandshake(Packet genericPacket)
        {
            switch ((PacketTypeHandshake)genericPacket.Type)
            {
                case PacketTypeHandshake.Handshake_C2S_Intention:
                    Handshake_C2S_Intention packet = (Handshake_C2S_Intention)genericPacket;

                    state = (ProtocolState)packet.NextState;
                    Log.Write($"Switching State: {state}", channel: LogChannel.DEBUG);
                    break;
                default:
                    throw new Exception("Invalid Packet");
            }
        }

        private void ProcessPacketStatus(Packet genericPacket)
        {
            switch ((PacketTypeStatus)genericPacket.Type)
            {
                case PacketTypeStatus.Status_C2S_Ping:
                    Status_C2S_Ping packet = (Status_C2S_Ping)genericPacket;
                    Status_S2C_Pong pongPacket = new Status_S2C_Pong { Payload = packet.Payload };
                    stream.Write(pongPacket.ToBytes());
                    shouldDisconnect = true;
                    break;
                case PacketTypeStatus.Status_C2S_Request:
                    Status_S2C_Response responsePacket = new Status_S2C_Response();
                    stream.Write(responsePacket.ToBytes());
                    break;
                default:
                    throw new Exception($"Invalid Packet: {(PacketTypeStatus)genericPacket.Type}");
            }
        }

        private void ProcessPacketLogin(Packet genericPacket)
        {
            switch ((PacketTypeLogin)genericPacket.Type)
            {
                case PacketTypeLogin.Login_C2S_Start:
                    Login_C2S_Start loginPacket = (Login_C2S_Start)genericPacket;
                    Log.Write($"Login attempt for: {loginPacket.Username} ({loginPacket.UUID})");

                    this.Username = loginPacket.Username;
                    this.UUID = loginPacket.UUID;

                    Login_S2C_Success successPacket = new Login_S2C_Success { Username = loginPacket.Username, UUID = loginPacket.UUID };
                    stream.Write(successPacket.ToBytes());
                    break;
                case PacketTypeLogin.Login_C2S_Ack:
                    state = ProtocolState.Configuration;
                    Log.Write($"Switching State: {state}", LogChannel.DEBUG);
                    Dispatcher.GetInstance().sessions.Add(this); // player has oficially joined the server

                    Config_S2C_ReportDetails reportDetails = new Config_S2C_ReportDetails();
                    stream.Write(reportDetails.ToBytes());

                    break;
                default:
                    throw new Exception($"Invalid Packet: {(PacketTypeLogin)genericPacket.Type}");
            }
        }

        private void ProcessPacketConfig(Packet genericPacket)
        {
            switch((PacketTypeConfig)genericPacket.Type)
            {
                case PacketTypeConfig.Config_C2S_Plugin:
                    Config_C2S_Plugin pluginPacket = (Config_C2S_Plugin)genericPacket;
                    string DataString;
                    try
                    {
                        DataString = Encoding.UTF8.GetString(pluginPacket.Data);
                    } catch (Exception e)
                    {
                        StringBuilder builder = new StringBuilder();
                        for (int i = 0; i < pluginPacket.Data.Length; i++)
                        {
                            builder.Append(pluginPacket.Data[i].ToString("x2"));
                        }
                        DataString = builder.ToString();
                    }

                    if (pluginPacket.Identifier == "minecraft:brand")
                    {
                        ClientBrand = DataString;
                    }

                    Log.Write($"[Plugin {pluginPacket.Identifier}] {DataString}", LogChannel.DEBUG);
                    break;
                case PacketTypeConfig.Config_C2S_Information:
                    Config_C2S_Information informationPacket = (Config_C2S_Information)genericPacket;

                    Locale = informationPacket.Locale;
                    ViewDistance = informationPacket.ViewDistance;
                    ChatMode = informationPacket.ChatMode;
                    ChatColors = informationPacket.ChatColors;
                    EnableTextFiltering = informationPacket.EnableTextFiltering;
                    AllowServerListing = informationPacket.AllowServerListing;

                    player = new Player(
                        Dispatcher.GetInstance().GenerateEntityID(),
                        (Guid)UUID,
                        Vector3.Zero,
                        Username,
                        informationPacket.SkinParts,
                        informationPacket.MainHand
                    );

                    Config_S2C_FinishConfig finishPacket = new Config_S2C_FinishConfig();
                    stream.Write(finishPacket.ToBytes());
                    break;
                case PacketTypeConfig.Config_C2S_FinishConfigAck:
                    state = ProtocolState.Play;
                    Log.Write($"Switching State: {state}", LogChannel.DEBUG);

                    byte[] brandData = null;
                    using (var serverBrandStream = new MemoryStream())
                    {
                        VarString.WriteVarString(serverBrandStream, "CraftCore");
                        brandData = serverBrandStream.ToArray();
                    }
                    S2C_Plugin serverBrand = new S2C_Plugin
                    {
                        Identifier = "minecraft:brand",
                        Data = brandData
                    };
                    stream.Write(serverBrand.ToBytes());


                    //S2C_SpawnEntity spawnPlayer = new S2C_SpawnEntity { entity = player };
                    //stream.Write(spawnPlayer.ToBytes());

                    S2C_Login loginPlayer = new S2C_Login { player = player };
                    stream.Write(loginPlayer.ToBytes());
                    break;
                default:
                    break;
            }
        }
    }

}
