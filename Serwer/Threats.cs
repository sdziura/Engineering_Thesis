using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Threats
    {
        int nrOfNodes { get; }
        public bool[] positionsOfThreats { get; set; }
        enum typeOfThreat { None = 0, Fire, Gas, Agressor };
        typeOfThreat type { get; set; }

        public Threats(Graph graph)
        {
            nrOfNodes = graph.nrOfNodes;
            positionsOfThreats = new bool[nrOfNodes];
            type = typeOfThreat.None;
        }
        public Threats()
        {
        }
    }
}
