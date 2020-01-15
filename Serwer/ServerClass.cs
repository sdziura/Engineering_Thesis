using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace Server
{
    public static class Constants
    {
        public const int MAXWEIGHT = 100;
        public const int MAXSIZE = 500000000;
    }
    public class DataToSend
    {
        public Graph graph { get; set; }
        public Threats threats { get; set; }
        public DataToSend(Graph _graph, Threats _threats)
        {
            graph = _graph;
            threats = _threats;
        }
        public DataToSend()
        {
        }
    }

    public interface IServerClass
    {
        // Function that turns on, waiting for message with data
        DataToSend listen();
        // Sends calculated path to client
        void sendPath(List<int> path);
        // Close server
        void closeServer();

    }
}
