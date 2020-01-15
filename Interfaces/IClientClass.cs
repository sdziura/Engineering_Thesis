using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client;
using clientlib;

namespace Interfaces
{
    public static class Constants
    {
        public const int MAXWEIGHT = 100;
        public const int MAXSIZE = 500000000;
    }
    // Class used to pack all information that need to be send to Server 
    public class DataToSend
    {
        public Graph graph;
        public Threats threats;
        public DataToSend(Graph _graph, Threats _threats)
        {
            graph = _graph;
            threats = _threats;
        }
        public DataToSend(Graph _graph)
        {
            graph = _graph;
            threats = new Threats(graph);
        }
    }

    public interface IClientClass
    {
        // Send graph and threats to server
        void sendGraph(Graph graf, Threats threats);
        // Sends graph without threats (threats object should be created, but with default)
        void sendGraph(Graph graf);
        // Receives path from server
        List<int> receivePath();
        // Cancel connection
        void closeConnection();

    }
}
