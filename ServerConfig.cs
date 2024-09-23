using System.IO;
using YamlDotNet.Serialization;

namespace CraftCore
{
    internal class ServerConfig
    {

        public string Version = "v1, for Minecraft 1.21.1, protocol 767";

        public int MaxPlayers { get; set; }
        public string StatusMessage { get; set; }
        public string FaviconPath { get; set; }
        public string Favicon { get; set; }
        public int LogLevel { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

        public static ServerConfig LoadConfig(string filePath)
        {
            // Read the YAML file
            var yaml = File.ReadAllText(filePath);

            // Create a deserializer
            var deserializer = new DeserializerBuilder().Build();

            // Deserialize the YAML to ServerConfig
            var config = deserializer.Deserialize<ConfigRoot>(yaml);

            return new ServerConfig
            {
                MaxPlayers = config.CraftCore.MaxPlayers,
                StatusMessage = config.CraftCore.StatusMessage,
                FaviconPath = config.CraftCore.Favicon,
                Favicon = $"data:image/png;base64,{Convert.ToBase64String(File.ReadAllBytes(config.CraftCore.Favicon))}",
                Host = config.CraftCore.Host,
                Port = config.CraftCore.Port,
                LogLevel = config.CraftCore.LogLevel,
            };
        }
    }

    // Helper class to match the YAML structure
    public class ConfigRoot
    {
        public CraftCoreConfig CraftCore { get; set; }
    }

    public class CraftCoreConfig
    {
        public int MaxPlayers { get; set; }
        public string StatusMessage { get; set; }
        public string Favicon { get; set; }
        public int LogLevel { get; set; }
        public string Host {  get; set; }
        public int Port { get; set; }
    }
}
