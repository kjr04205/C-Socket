using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;
using System.Text;

namespace ClientSocket2
{
    class Program
    {
        static Socket socket;
        static void DoSomething()
        {
            Console.WriteLine("쓰레드가 호출되었습니다.");
            Console.WriteLine();

            while (true)
            {
                byte[] buff = new byte[1000];
                int n = socket.Receive(buff);

                string result = Encoding.UTF8.GetString(buff, 0, n);

                Console.Write("관리자 : ");
                Console.WriteLine(result);
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("192.168.0.19", 53188);

            if (socket.Connected)
            {
                Console.WriteLine("서버에 연결 되었습니다.");
            }
            Thread t1 = new Thread(new ThreadStart(DoSomething));
            t1.Start();
            string message = string.Empty;

            while ((message = Console.ReadLine()) != "x")
            {
                byte[] buff = Encoding.UTF8.GetBytes(message);
                socket.Send(buff);
            }
        }
    }
}
