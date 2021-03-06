﻿using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Collections.Generic;
using Client;

namespace clientlib
{
    public class ClientClass : Client.IClientClass
    {
        TcpClient tcpclnt;
        string ipAdress = "192.168.55.103";
        int port = 8021;

        public ClientClass()
        {
            try
            {
                tcpclnt = new TcpClient();
                Console.WriteLine("Connecting.....");

                tcpclnt.Connect(ipAdress, port);
                // use the ipaddress as in the server program

                Console.WriteLine("Connected");
            }

            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }

        public void sendGraph(Graph graf, Threats threats)
        {
            // Creating stream and encoder
            Stream stm = tcpclnt.GetStream();
            ASCIIEncoding asen = new ASCIIEncoding();

            // Packing, serializing and encoding data to send
            DataToSend toSend = new DataToSend(graf, threats);
            string jsonString = JsonConvert.SerializeObject(toSend);
            byte[] ba = asen.GetBytes(jsonString);

            Console.WriteLine("Sending data about graph and threats to server ...");
            // Writing data to stream
            stm.Write(ba, 0, ba.Length);
        }
        public void sendGraph(Graph graf)
        {
            sendGraph(graf, new Threats(graf));
        }
        public List<int> receivePath()
        {
            // Creating stream and encoder
            Stream stm = tcpclnt.GetStream();
            ASCIIEncoding asen = new ASCIIEncoding();

            byte[] bb = new byte[Constants.MAXSIZE];
            int k = stm.Read(bb, 0, Constants.MAXSIZE);

            // Decoding message to json string
            var jsonReceived = new StringBuilder();
            Console.WriteLine("Recieved path ...");
            for (int i = 0; i < k; i++)
            {
                Console.Write(Convert.ToChar(bb[i]));
                jsonReceived.Append(Convert.ToChar(bb[i]));
            }
            Console.WriteLine();
            string strr = jsonReceived.ToString();

            // From json to Object
            List<int> receivedPath = JsonConvert.DeserializeObject<List<int>>(strr);

            return receivedPath;
        }

        public void closeConnection()
        {
            tcpclnt.Close();
        }
    }
}
