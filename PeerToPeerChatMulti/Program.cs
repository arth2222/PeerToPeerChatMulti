//This code uses the UdpClient class from the System.Net.Sockets namespace to send and receive messages over the
//network using the
// User Datagram Protocol(UDP). The user is prompted for the IP address and port number to connect to, as well as
// their name.
//The code runs two threads: one for sending messages and one for receiving messages. The ReceiveData method
//listens for incoming
//messages on the specified port and prints them to the console. The SendData method sends messages to the specified
//IP address and port.









using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PeerToPeerChatMulti
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the IP address to connect to:");
            string remoteIP = Console.ReadLine();

            Console.WriteLine("Enter the port number to use:");
            int remotePort = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();

            Thread receiveThread = new Thread(new ThreadStart(() => ReceiveData(remoteIP, remotePort)));
            receiveThread.Start();

            while (true)
            {
                string message = Console.ReadLine();
                SendData(remoteIP, remotePort, name + ": " + message);
            }
        }

        private static void SendData(string remoteIP, int remotePort, string message)
        {
            UdpClient client = new UdpClient();
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(remoteIP), remotePort);
            byte[] sendBytes = Encoding.ASCII.GetBytes(message);
            client.Send(sendBytes, sendBytes.Length, endPoint);
        }

        private static void ReceiveData(string remoteIP, int remotePort)
        {
            UdpClient client = new UdpClient(remotePort);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(remoteIP), remotePort);
            while (true)
            {
                byte[] receiveBytes = client.Receive(ref endPoint);
                string message = Encoding.ASCII.GetString(receiveBytes);
                Console.WriteLine(message);
            }
        }
    }
}
