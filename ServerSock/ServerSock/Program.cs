using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ServerSock
{
    class Program
    {
        static Socket clientSocket;

        static void DoSomething()
        {
            Console.WriteLine("쓰레드가 호출되었습니다.");
            Console.WriteLine();

            while (true)
            {
                string a = Console.ReadLine();
                byte[] buff = Encoding.UTF8.GetBytes(a);
                clientSocket.Send(buff);
            }
        }
        static void Main(string[] args)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("192.168.0.19"), 53188);
            socket.Bind(ep);

            socket.Listen(10);

            clientSocket = socket.Accept();

            if (clientSocket.Connected)
            {
                Console.WriteLine("클라이언트가 서버에 접속하였습니다.");
                Thread t1 = new Thread(new ThreadStart(DoSomething));
                t1.Start();
            }

            while (!Console.KeyAvailable)
            {
                byte[] buff = new byte[2048];

                int n = clientSocket.Receive(buff);

                string result = Encoding.UTF8.GetString(buff, 0, n);

                Console.Write("안효인 : ");
                Console.WriteLine(result);
                Console.WriteLine();
            }
        }
    }
}
