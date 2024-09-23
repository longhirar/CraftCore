
using System;

namespace CraftCore
{

    class Program
    {
        static void Main(string[] args)
        {
            ServerConfig config = ServerConfig.LoadConfig("CraftCore.yml");
            Dispatcher dispatcher = new Dispatcher(config);

            Log.Write($"CraftCore {config.Version}");

            dispatcher.Start();
        }
    }

}