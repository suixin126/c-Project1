using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileServer
{
    public partial class Form1 : Form
    {
        private Socket listener;
        private List<Socket> clients = new List<Socket>();
        public Form1()
        {
            InitializeComponent();
            StarListening();
        }
        int mPosX = 0;
        int mPosY = 0;
        bool isShow = false;
        // 图片处理
        private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
        {
            //获取图片宽度
            int sourceWidth = imgToResize.Width;
            //获取图片高度
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //计算宽度的缩放比例
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //计算高度的缩放比例
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //期望的宽度
            int destWidth = (int)(sourceWidth * nPercent);
            //期望的高度
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //绘制图像
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }

        private void StarListening()
        {
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(new IPEndPoint(IPAddress.Any, 1234));
            listener.Listen(10);
            AcceptClients();
        }

        private void AcceptClients()
        {
            listener.BeginAccept(new AsyncCallback(OnClientConnected), null);
        }
        private void OnClientConnected(IAsyncResult ar)
        {
            Socket client = listener.EndAccept(ar);
            clients.Add(client);
            Console.WriteLine("客户连接成功");

            Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
            clientThread.Start(client);

            AcceptClients();
        }

        private void HandleClientComm(object clientObj)
        {
            Socket client = (Socket)clientObj;

            byte[] buffer = new byte[1024 * 1024];
            int bytesRead = 0;
            string headMessage;
            string bodyMessage;
            string name;
            while (true)
            {

                try
                {
                    bytesRead = client.Receive(buffer);

                    if (bytesRead == 0) break;
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine(message);
                    string[] s = message.Split('&');
                    headMessage = s[0];

                    // 文本
                    if (headMessage.Equals("Text"))
                    {
                        name = s[1];
                        bodyMessage = s[2];
                        Invoke(new Action(() =>
                        {
                            richTextBox1.Show();
                            Label label = new Label();
                            richTextBox1.AppendText("Received from " + name + ":" + bodyMessage);
                            richTextBox1.CreateControl();
                            
                        }));

                        Console.WriteLine(bodyMessage);
                        Console.WriteLine(bodyMessage.Equals("生日快乐"));

                        if (bodyMessage.Equals("生日快乐"))
                        {
                            mPosX = 0;
                            mPosY = 0;
                            this.Invoke(new Action(() =>
                            {
                                //this.Controls.Add(btn);
                                richTextBox1.Hide();
                                timer1.Enabled = true;
                                isShow = true;
                            }));
                           
                        }
                        Console.WriteLine(timer1.Enabled);
                    }
                    // 图片
                    if (headMessage.Equals("Img"))
                    {
                        name = s[1];
                        bodyMessage = s[2];
                        Invoke(new Action(() =>
                        {
                            richTextBox1.Show();
                            richTextBox1.AppendText(bodyMessage);
                            Image image = Image.FromFile(bodyMessage);
                            Bitmap b = new Bitmap(image);
                            System.Drawing.Image i = resizeImage(b, new Size(40, 40));
                            IDataObject data = new DataObject();
                            data.SetData(i);
                            Clipboard.SetDataObject(data, false);
                            richTextBox1.SelectionStart = richTextBox1.Text.Length;
                            richTextBox1.Paste();
                            richTextBox1.AppendText(Environment.NewLine + "测试结束！");
                        }));
                    }
                    // 文件
                    if (headMessage.Equals("File"))
                    {
                    }

                    // 转发消息给其他客户端（不包括发送者）  
                    foreach (Socket c in clients)
                    {
                        if (c != client)
                        {
                            c.Send(buffer, 0, bytesRead, SocketFlags.None);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    break;
                }
            }

            client.Close();
            clients.Remove(client);
            Console.WriteLine("Client disconnected.");
        }



        // 蛋糕雨
        private void timer1_Tick(object sender, EventArgs e)
        {
            mPosY += 10;
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Random r = new Random();
            
            if (isShow == true)
            {
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao.png"), mPosX, mPosY, 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao1.png"), mPosX + r.Next(0, 200), mPosY - r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao2.png"), mPosX + r.Next(0, 200), mPosY - r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao3.png"), mPosX + r.Next(0, 200), mPosY + r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao4.png"), mPosX + r.Next(0, 200), mPosY + r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao5.png"), mPosX + r.Next(0, 200), mPosY + r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao6.png"), mPosX + r.Next(0, 200), mPosY - r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao7.png"), mPosX + r.Next(0, 200), mPosY - r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao8.png"), mPosX + r.Next(0, 200), mPosY - r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao9.png"), mPosX + r.Next(0, 200), mPosY - r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao10.png"), mPosX + r.Next(0, 200), mPosY - r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao11.png"), mPosX + r.Next(0, 200), mPosY - r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao12.png"), mPosX + r.Next(0, 200), mPosY - r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao13.png"), mPosX + r.Next(0, 200), mPosY - r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao14.png"), mPosX + r.Next(0, 200), mPosY - r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao15.png"), mPosX + r.Next(0, 200), mPosY - r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao16.png"), mPosX + r.Next(0, 200), mPosY - r.Next(0, 200), 20, 20);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
