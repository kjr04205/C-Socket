using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace ChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // TcpClient 선언
            TcpClient client = null;
            try
            {
                // client 생성
                client = new TcpClient();
                // client 127.0.0.1/34624 로 연결
                client.Connect("127.0.0.1", 34624);

                // GetStream을 사용하여 내부를 가져오고 NetworkStream을 가져온 후에는 보내고 받는다.
                NetworkStream stream = client.GetStream();

                Encoding encode = Encoding.GetEncoding("utf-8");

                StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };
                StreamReader reader = new StreamReader(stream, encode);

                string dataToSend = Console.ReadLine();

                while (true)
                {
                    writer.WriteLine(dataToSend);

                    if(dataToSend.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }

                    Console.WriteLine(reader.ReadLine());

                    dataToSend = Console.ReadLine();
                }

            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
