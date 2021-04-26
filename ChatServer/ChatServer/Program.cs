using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatServer
{
    // 클라이언트 핸들러
    class ClientHadler
    {
        //소켓 
        Socket socket = null;
        // networkstream :: 네트워크 액세스를 위한 데이터의 기본 스트림을 제공
        NetworkStream stream = null;
        StreamReader reader = null;
        StreamWriter writer = null;

        public ClientHadler(Socket socket)
        {
            this.socket = socket;
        }

        public void chat()
        {
            stream = new NetworkStream(socket);
            Encoding encode = Encoding.GetEncoding("utf-8");
            reader = new StreamReader(stream, encode);
            writer = new StreamWriter(stream, encode) { AutoFlush = true };

            while (true)
            {
                string str = reader.ReadLine();
                Console.WriteLine(str);

                writer.WriteLine(str);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // NetworkStream 객체를 통해 데이터를 주고받음.
            NetworkStream stream = null;
            TcpListener tcpListener = null;
            Socket clientsocket = null;
            StreamReader reader = null;
            StreamWriter writer = null;

            try
            {
                // 1. ip 127.0.0.1을 ipAd에 저장.
                IPAddress ipAd = IPAddress.Parse("127.0.0.1");

                // 2. 127.0.0.1에 34624포트를 Listener
                tcpListener = new TcpListener(ipAd, 34624);

                // 3. Listener를 연결 요청 수신 대기
                tcpListener.Start();

                while (true)
                {
                    // 4. 클라이언트의 연결 요청을 수락, TcpClient 객체를 반환
                    clientsocket = tcpListener.AcceptSocket();

                    // 5. 클라이언트 핸들러 클래스
                    ClientHadler cHandler = new ClientHadler(clientsocket);
                    
                    // 6. ClientHadler 안에 t 쓰레드를 생성하고 chat() 내용 실행
                    Thread t = new Thread(cHandler.chat);
                    t.Start();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
