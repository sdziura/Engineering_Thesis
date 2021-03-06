﻿using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Collections.Generic;
using Server;

namespace serverlib
{
    public class ServerClass : Server.IServerClass
    {
        // use local m/c IP address, and 
        // use the same in the client
        IPAddress ipAd = IPAddress.Parse("192.168.55.103");
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

        public DataToSend listen()
        {
            // Receiving data from client
            byte[] b = new byte[Constants.MAXSIZE];
            int k = socket.Receive(b);

            // Decoding message to json string
            var jsonReceived = new StringBuilder();
            Console.WriteLine("Recieved...");
            for (int i = 0; i < k; i++)
            {
                jsonReceived.Append(Convert.ToChar(b[i]));
            }
            string strr = jsonReceived.ToString();

            // From json to Object
            DataToSend receivedData = JsonConvert.DeserializeObject<DataToSend>(strr);

            return receivedData;
        }

        public void sendPath(List<int> path)
        {
            Console.WriteLine("Sending shortest path to client ...");
            string jsonString = JsonConvert.SerializeObject(path);
            ASCIIEncoding asen = new ASCIIEncoding();
            socket.Send(asen.GetBytes(jsonString));
            Console.WriteLine("Path sent to client\n");
        }

        public void closeServer()
        {
            socket.Close();
            myList.Stop();
        }

    }
}
