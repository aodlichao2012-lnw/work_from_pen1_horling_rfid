using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tcp_test
{
    public partial class Form1 : Form
    {
        TcpClient tcp;
        public Form1()
        {
            InitializeComponent();
            //Startsevice();
            Connect_tcp(ref tcp);
            //cl_instance.instance.b = 1;
            openrelay();
        }

        private void openrelay()
        {
            SerialPort serialPort = new SerialPort();
            serialPort.PortName = "COM4";
            serialPort.ReadTimeout = 3000;
            serialPort.WriteTimeout = 3000;
            serialPort.Open();
            int bit = 1;
            int close_bit = 0;
            int shutter_relay = 1;
            int convoy_relay = 2;
            byte[] data1 = new byte[] { 255, (byte)shutter_relay, (byte)bit };
            byte[] data2 = new byte[] { 255, (byte)convoy_relay, (byte)bit };
            byte[] data3_close = new byte[] { 255, (byte)shutter_relay, (byte)close_bit };
            byte[] data4_close = new byte[] { 255, (byte)convoy_relay, (byte)close_bit };
            serialPort.Write(data1, 0, data1.Length);
            serialPort.Write(data2, 0, data2.Length);
            serialPort.Write(data3_close, 0, data3_close.Length);
            serialPort.Write(data4_close, 0, data4_close.Length);
        }

        private void Startsevice()
        {
            IPAddress[] iPAddress = Dns.GetHostAddresses("127.0.0.1");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress[0], 13000);
            TcpListener server = new TcpListener(iPEndPoint);
            server.Start();
            while (true)
            {
                TcpClient tcpClient = new TcpClient();
                tcpClient = server.AcceptTcpClient();
                string helloworld = "helloworld";
                byte[] buffer = Encoding.UTF8.GetBytes(helloworld);
                byte[] resive = new byte[2048];
                int i = 2;
                NetworkStream stream = tcpClient.GetStream();
                stream.Write(buffer, 0, buffer.Length);

                while (tcpClient.Connected)
                {

                    if (resive.Length > 0)
                    {
                        stream.Read(resive, 0, resive.Length);
                    }
                    server.Stop();
                    label1.Text = Encoding.UTF8.GetString(resive);
                }
            }
        }

        private void Connect_tcp(ref TcpClient tcpClient)
        {

            IPAddress[] iPAddresses = Dns.GetHostAddresses("192.168.1.90");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddresses[0] , 6001);
            tcpClient = new TcpClient();
            tcpClient.Connect(iPEndPoint);
            if(tcpClient.Connected)
            {
                MessageBox.Show("connected successfull");
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = "12345";
            string password = "";
            string login = "";
            string msgcheck = "9900402.00AY7AZFC9E\r";
            check_login(ref login);
            check_hand(ref msgcheck);
            pathodid(ref username, ref password);
        }

        private void check_hand(ref string msg)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(msg);
            byte[] res = new byte[2048];
            if (tcp.Connected)
            {
                NetworkStream steam = tcp.GetStream();
                steam.Write(buffer, 0, buffer.Length);
                steam.Read(res, 0, res.Length);
                msg = Encoding.UTF8.GetString(res);
            }
        }

        private void check_login(ref string loging)
        {
            string msgLogin = "9300CNadmin|COAdmin123|CPdefault_corp|AY5AZF120\r";
            byte[] buffer = Encoding.UTF8.GetBytes(msgLogin);
            byte[] res_buffer = new byte[2048];
            if (tcp.Connected)
            {
                NetworkStream steam = tcp.GetStream();
                steam.Write(buffer, 0, buffer.Length);
                steam.Read(res_buffer, 0, res_buffer.Length);
                loging = Encoding.UTF8.GetString(res_buffer);
            }

        }



        private void pathodid(ref string v1, ref string v2)
        {
            DateTime date = DateTime.Now;
            string years = date.Year.ToString("00");
            string mouth = date.Month.ToString("00");
            string day = date.Day.ToString("00");
            string hour = date.Hour.ToString("00");
            string mm = date.Minute.ToString("00");
            string second = date.Second.ToString("00");
            string msg = "63001" + years + mouth + day + "    " + hour + mm + second + "          " + "AOCPL" + "|AA" + v1 + "|AC|AD" + v2 + "|";
            string msg2 = "2300120220105    153835          AOCPL|AA12345|AC|AD|AY2AZF331\r";
            byte[] buffer = Encoding.UTF8.GetBytes(msg2);
            byte[] res_buffer = new byte[2048];
            if (tcp.Connected)
            {
                NetworkStream steam = tcp.GetStream();
                steam.Write(buffer, 0, buffer.Length);
                steam.Read(res_buffer, 0, res_buffer.Length);
                string res_text = Encoding.UTF8.GetString(res_buffer);
                label1.Text = res_text;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string key_check_book = "1720220105    152735AOCPL|AB999888|AC|AY2AZF5B1\r";
            check_book(ref key_check_book);
            label1.Text = key_check_book;
        }

        private void check_book(ref string key_check_book)
        {
            byte[] key = Encoding.UTF8.GetBytes(key_check_book);
            byte[] key_2 = new byte[2048];
            if (tcp.Connected)
            {
                NetworkStream steam = tcp.GetStream();
                steam.Write(key, 0, key.Length);
                steam.Read(key_2, 0, key_2.Length);

                string res = Encoding.UTF8.GetString(key_2);
                key_check_book = res;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string key = "09N20220105    15535320220105    155353AP|AOCPL|AB999888|AC|";
            byte[] buffer = Encoding.UTF8.GetBytes(key);
            byte[] res = new byte[2048];

            if (tcp.Connected)
            {
                NetworkStream steam = tcp.GetStream();
                steam.Write(buffer, 0, buffer.Length);
                steam.Read(res, 0, res.Length);

                string res_text = Encoding.UTF8.GetString(res);
                label1.Text = res_text;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string key = "11YN20220105    16005620220105    160056|AA12345|AB999888|AC|AY1AZF14A\r";
            byte[] buffer = Encoding.UTF8.GetBytes(key);
            byte[] res = new byte[2048];
            if (tcp.Connected)
            {
                NetworkStream steam = tcp.GetStream();
                steam.Write(buffer, 0, buffer.Length);
                steam.Read(res, 0, res.Length);
                string res_text = Encoding.UTF8.GetString(res);
                label1.Text = res_text;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tcp = new TcpClient();
            Connect_tcp(ref tcp);


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    //private async void TestAsync()
    //{
    //    await Task.Run(() => {
    //        for (int i = 0; i < 10; i++)
    //        {
    //            Thread.Sleep(1000);

    //            //Console.WriteLine(i);

    //            label1.Invoke((MethodInvoker)delegate
    //            {
    //                // Running on the UI thread
    //                label1.Text = i.ToString();
    //            });


    //        }
    //    });
    //}

}

