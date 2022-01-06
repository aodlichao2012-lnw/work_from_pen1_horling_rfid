using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TCP_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Timer time = new Timer();

            time.Interval = 300;
            time.Start();

            IPAddress[] addess = Dns.GetHostAddresses("127.0.0.1");

            //IPAddress[] addess = Dns.GetHostAddresses("192.168.1.90");
            IPEndPoint endpoint = new IPEndPoint(addess[0], 13000);

            //    HttpClient client = new HttpClient();
            //    var content =   client.GetAsync("http://www.namede.net/get.php");
            //    string res  = content.Result.StatusCode.ToString();

            //    if(res.Equals("OK"))
            //{
            //         var conent = content.Result.Content.ReadAsStringAsync();
            //}

         TcpClient   TcpClient = new TcpClient();
            byte[] buffer = new byte[2048];
            TcpClient.Connect(endpoint);
            
            if (TcpClient.Connected)
            {
                Console.WriteLine("connect success full");
                NetworkStream stream = TcpClient.GetStream();
                stream.Read(buffer,0,buffer.Length);
                   stream.Write(buffer,0,buffer.Length);
                string text = Encoding.UTF8.GetString(buffer);
                Console.WriteLine(text);
                Console.ReadLine();
            }

        }
    }
}
