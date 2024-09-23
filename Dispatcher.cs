using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace CraftCore
{

    class Dispatcher
    {
        
        

        private TcpListener listener;
        private bool isRunning;
        private static Dispatcher? instance;

        public ServerConfig config;
        public List<PlayerSession> sessions = new List<PlayerSession>();

        private int entityIDCount = 0;

        public int GenerateEntityID()
        {
            return entityIDCount++;
        }

        public static Dispatcher GetInstance()
        {
            return instance;
        }

        public Dispatcher(ServerConfig config)
        {
            listener = new TcpListener(IPAddress.Parse(config.Host), config.Port);
            isRunning = true;
            this.config = config;
            instance = this;
        }

        public void Start()
        {
            listener.Start();
            Log.Write($"Server started on {listener.LocalEndpoint}");

            while (isRunning)
            {
                TcpClient client = listener.AcceptTcpClient();
                Log.Write("New client connected!");

                // Create a new player session in its own thread
                Thread playerThread = new Thread(() =>
                {
                    PlayerSession session = new PlayerSession(client);
                    session.HandlePlayer();
                });

                playerThread.Start();
            }
        }

        public void Stop()
        {
            isRunning = false;
            listener.Stop();
        }
    }

}
