using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;

namespace ClientSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("192.168.0.19", 53188);

            if (socket.Connected)
            {
                Console.WriteLine("서버에 연결 되었습니다.");
            }

            string message = string.Empty;
            
            while((message = Console.ReadLine()) != "x")
            {
                byte[] buff = Encoding.UTF8.GetBytes(message);
                socket.Send(buff);
            }
        }
    }
}
