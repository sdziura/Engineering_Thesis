using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace Client
{
    class ClientClass
    {
        TcpClient tcpclnt;
        string ipAdress = "192.168.1.2";

        public ClientClass()
        {
            try
            {
                tcpclnt = new TcpClient();
                Console.WriteLine("Connecting.....");

                tcpclnt.Connect(ipAdress, 8021);
                // use the ipaddress as in the server program

                Console.WriteLine("Connected");
            }

            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }

        public void sendGraph(Graph graf)
        {
            string jsonString;
            jsonString = JsonConvert.SerializeObject(graf);
            Stream stm = tcpclnt.GetStream();

            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] ba = asen.GetBytes(jsonString);
            Console.WriteLine("Transmitting.....");

            stm.Write(ba, 0, ba.Length);

            byte[] bb = new byte[100];
            int k = stm.Read(bb, 0, 100);

            for (int i = 0; i < k; i++)
                Console.Write(Convert.ToChar(bb[i]));
        }

        public void closeConnection()
        {
            tcpclnt.Close();
        }
    }
}
