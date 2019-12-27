using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace Server
{
    class ServerClass
    {   
        // use local m/c IP address, and 
        // use the same in the client
        IPAddress ipAd = IPAddress.Parse("192.168.1.2");
        TcpListener myList;
        Socket socket;

        public ServerClass()
        { 
            try
            {
                /* Initializes the Listener */
                myList = new TcpListener(ipAd, 8021);

                /* Start Listeneting at the specified port */
                myList.Start();

                Console.WriteLine("The server is running at port 8001...");
                Console.WriteLine("The local End point is  :" +
                                  myList.LocalEndpoint);
                Console.WriteLine("Waiting for a connection.....");
                socket = myList.AcceptSocket();
                Console.WriteLine("Connection accepted from " + socket.RemoteEndPoint);

                /* clean up */

            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
                Console.ReadKey();
            }
        }

        public Graph listen()
        {
            byte[] b = new byte[100];
            int k = socket.Receive(b);
            var jsonReceived = new StringBuilder();
            Console.WriteLine("Recieved...");
            for (int i = 0; i < k; i++)
            {
                Console.Write(Convert.ToChar(b[i]));
                jsonReceived.Append(Convert.ToChar(b[i]));
            }
            string strr = jsonReceived.ToString();
            Graph graf = JsonConvert.DeserializeObject<Graph>(strr);
            ASCIIEncoding asen = new ASCIIEncoding();
            socket.Send(asen.GetBytes("The string was recieved by the server."));
            Console.WriteLine("\nSent Acknowledgement");
            return graf;
        }

        public void closeServer()
        {
            socket.Close();
            myList.Stop();
        }

    }
}
