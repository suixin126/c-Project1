using login2.Properties;
using MySqlX.XDevAPI;
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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace login2
{
    public partial class Chat : Form
    {
        public EmotionForm form2 = null; //插入表情窗口
        private Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); // 客户端
        private string friendnickName; // 好友的昵称
        private string messageType; // 消息类型
        private string ownNickName; // 自己昵称
        
        // 蛋糕雨
        private int mPosX = 0; // 对应X坐标
        private int mPosY = 0; // 对应Y坐标
        private bool isShow = false; // 是否显示蛋糕雨

        // 拖动窗体
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        private void Chat_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        public Chat(string userName, string nickName)
        {
            this.ownNickName = userName; // 自己的昵称
            this.friendnickName = nickName; // 好友昵称
            ConnectToServer();
            InitializeComponent();
        }

        // 调整发送图片的大小
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

            // 获取图片
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            // 获得更平滑、更自然的图像质量。
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //绘制图像
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }

        // 连接服务器
        private void ConnectToServer()
        {
            try
            {
                client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234));
                // 开始监听来自服务器的消息  
                new Thread(ReceiveMessages).Start();

                // 显示连接成功的消息或更新UI状态  
                MessageBox.Show("Connected to the server!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting: " + ex.Message);
            }
        }

        // 接收信息
        private void ReceiveMessages()
        {
            // 接收缓存
            byte[] buffer = new byte[1024];
            // 接收字节大小
            int bytesRead;
            // 连接一直未断开
            while (client.Connected)
            {
                try
                {
                    // 阻塞调用，直到有数据可读 ，获取接收的数据长度
                    bytesRead = client.Receive(buffer);

                    // 将接收到的字节转换为字符串  
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    // 发送的数据以 & 进行分割，得到一个字符串数组
                    string[] s = message.Split('&');
                    messageType = s[0]; // 发送的数据类型
                    string name = s[1]; // 发送的来源
                    string bodyMessage = s[2]; // 发送的具体信息

                    if (bytesRead == 0)
                    {
                        // 服务器可能已关闭连接  
                        break;
                    }

                    // 接收的是文本信息
                    if (messageType.Equals("Text"))
                    {
                        // 关闭 “蛋糕雨” 的计时器
                        timer1.Enabled = false;
                        // 更新UI以显示接收到的消息（注意：这里需要在UI线程上执行）  
                        Invoke(new Action(() =>
                        {
                            // 假设有一个名为messagesListBox的ListBox控件用于显示消息  
                            rtbReceive.AppendText("Received from" + name + ":" + bodyMessage + "\n");
                        }));

                        // 判断消息体如果是 “生日快乐”的字符串执行下列命令
                        if (bodyMessage.Equals("生日快乐"))
                        {
                            mPosX = 0;
                            mPosY = 0;

                            this.Invoke(new Action(() =>
                            {
                                // 打开计时器，开始“蛋糕雨”
                                open();
                            }));
                        }
                    }
                    // 接收的信息是图片
                    if (messageType.Equals("Img"))
                    {

                        // 更新UI以显示接收到的消息（注意：这里需要在UI线程上执行）  
                        Invoke(new Action(() =>
                        {
                            // 假设有一个名为messagesListBox的ListBox控件用于显示消息  
                            //rtbMessage.AppendText("Received: " + bodyMessage);
                            rtbReceive.AppendText("Received from" + name + ":");
                            Image image = Image.FromFile(bodyMessage);

                            // 将图片粘贴到rtbReceive控件中
                            Bitmap b = new Bitmap(image);
                            System.Drawing.Image i = resizeImage(b, new Size(40, 40)); // 调整图片大小
                            DataObject data = new DataObject();
                            data.SetData(i);
                            // false参数是一个布尔值，它指定是否应该延迟呈现剪贴板上的数据。
                            Clipboard.SetDataObject(data, false);
                            // 设置 RichTextBox 控件的插入点到当前文本内容的末尾。
                            rtbReceive.SelectionStart = rtbReceive.Text.Length;
                            // 粘贴
                            rtbReceive.Paste();
                            // 换行
                            rtbReceive.AppendText("\n");
                        }));
                    }
                    // 文件
                    if (messageType.Equals("File"))
                    {
                        try
                        {
                            string _path = @"C:\test"; // 接收文件的存放位置
                            string fileName = bodyMessage; // 文件名
                            long fileLength = long.Parse(s[3]); // 文件长度
                            Console.WriteLine("接收文件" + fileName + "请稍等...");

                            long recieved = 0L; // 总接收长度

                            using (FileStream fsWriter = new FileStream(Path.Combine(_path, fileName), FileMode.Create, FileAccess.Write, FileShare.None))
                            {
                                int recive; // 单次接收长度

                                while (recieved < fileLength)
                                {
                                    recive = client.Receive(buffer); // 接收数据
                                    fsWriter.Write(buffer, 0, recive); // 将数据写入文件
                                    fsWriter.Flush(); // 清除缓存区，确保所有内容都可以写入文件
                                    recieved += recive; // 总接收长度增加
                                    Console.WriteLine("已接收数据：{0}/{1}", recieved.ToString(), fileLength.ToString());
                                }

                                Console.WriteLine("接收完成……\n");
                                Invoke(new Action(() =>
                                {
                                    rtbReceive.AppendText("Received from " + name + ": " + fileName + "  接收完成。\n");
                                }));
                            }
                            }catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // 处理异常，可能是连接已断开  
                    Console.WriteLine("Error receiving messages: " + ex.Message);
                    break;
                }
            }
            // 关闭连接
            client.Close();
        }

        // 窗体加载
        private void Chat_Load(object sender, EventArgs e)
        {
            // 获取好友昵称
            labNickName.Text = friendnickName;
        }

        // 发送表情
        private void btnSendEmotion_Click(object sender, EventArgs e)
        {
            // 关闭计时器
            timer1.Enabled = false;
            mPosX = 0;
            mPosY = 0;
            // 发送的类型是图片
            messageType = "Img&";
            string message = messageType + ownNickName + "&" + rtbSend.Text + "&";

            // 更新窗体
            Invoke(new Action(() =>
            {
                rtbReceive.AppendText("我的消息：");
                
                // 图片显示
                Image image = Image.FromFile(rtbSend.Text);
                Bitmap b = new Bitmap(image);
                System.Drawing.Image i = resizeImage(b, new Size(40, 40));
                IDataObject data = new DataObject();
                data.SetData(i);
                Clipboard.SetDataObject(data, false);
                rtbReceive.SelectionStart = rtbReceive.Text.Length;
                rtbReceive.Paste();

                // 换行
                rtbReceive.AppendText("\n");
            }));

            if (!string.IsNullOrEmpty(message))
            {
                // 将消息转换为字节数组并发送到服务器  
                byte[] buffer = Encoding.UTF8.GetBytes(message + Environment.NewLine); // 添加换行符以便在服务器端区分消息  
                client.Send(buffer, 0, buffer.Length, SocketFlags.None); // 发送数据

                // 清空TextBox以便输入新的消息 
                rtbSend.Clear();
            }
        }

        //发送文字
        private void btnSendText_Click(object sender, EventArgs e)
        {
            // 关闭计时器
            timer1.Enabled = false;
            mPosX = 0;
            mPosY = 0;

            // 获取要发送的消息
            messageType = "Text&";
            string message = messageType + ownNickName + "&" + rtbSend.Text + "&";

            // 更新窗体
            Invoke(new Action(() =>
            {
                rtbReceive.AppendText("我的消息：" + rtbSend.Text + "\n");
            }));


            // 判断发送的内容是否是 “生日快乐”
            if (rtbSend.Text.Equals("生日快乐"))
            {
                // 更新窗体
                Invoke(new Action(() =>
                {
                    // 开启计时器  显示“蛋糕雨”
                    open();
                }));
            }

            if (!string.IsNullOrEmpty(message))
            {
                // 将消息转换为字节数组并发送到服务器  
                byte[] buffer = Encoding.UTF8.GetBytes(message + Environment.NewLine); // 添加换行符以便在服务器端区分消息  
                client.Send(buffer, 0, buffer.Length, SocketFlags.None); // 发送信息 参数：SocketFlags.None是一个特殊的SocketFlags枚举值，它表示没有为这次发送操作指定任何特殊选项或标志。

                // 清空TextBox以便输入新的消息 
                rtbSend.Clear();
            }
        }


        // 发送图片
        private void picImg_Click(object sender, EventArgs e)
        {
            // 关闭计时器
            timer1.Enabled = false;
            mPosX = 0;
            mPosY = 0;
            // 发送的数据类型
            messageType = "Img&";
            string imageString = "";

            // 打开本地文件夹
            OpenFileDialog open = new OpenFileDialog();
            DialogResult result = open.ShowDialog();
            // 获取到文件对应的路径
            if (result == DialogResult.OK)
            {
                imageString = open.FileName;

                // 调试
               // Console.WriteLine(imageString);
            }

            // 获取要发送的消息 
            string message = messageType + ownNickName + "&" + imageString;

            // 更新窗体
            if (!string.IsNullOrEmpty(message))
            {
                // 更新UI以显示接收到的消息（注意：这里需要在UI线程上执行）  
                Invoke(new Action(() =>
                {
                    // 假设有一个名为messagesListBox的ListBox控件用于显示消息  
                    //rtbMessage.AppendText("Received: " + bodyMessage);
                    rtbReceive.AppendText("我的消息:");

                    // 获取图片到粘贴板
                    Image image = Image.FromFile(imageString);
                    Bitmap b = new Bitmap(image);
                    System.Drawing.Image i = resizeImage(b, new Size(40, 40));
                    IDataObject data = new DataObject();
                    data.SetData(i);
                    Clipboard.SetDataObject(data, false);
                    rtbReceive.SelectionStart = rtbReceive.Text.Length;
                    // 粘贴
                    rtbReceive.Paste();
                    // 换行
                    rtbReceive.AppendText("\n");
                }));
                // 将消息转换为字节数组并发送到服务器  
                byte[] buffer = Encoding.UTF8.GetBytes(message + Environment.NewLine); // 添加换行符以便在服务器端区分消息  
                client.Send(buffer, 0, buffer.Length, SocketFlags.None); // 发送数据
            }
        }

        // 截屏
        // 全屏
        public Bitmap CaptureFullScreen()
        {
            // 这里定义了一个Rectangle对象tScreenRect，它表示主屏幕的整个区域。这个矩形的左上角坐标是(0, 0)，宽度和高度分别是主屏幕的宽度和高度。
            Rectangle tScreenRect = new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Bitmap tSrcBmp = new Bitmap(tScreenRect.Width, tScreenRect.Height); // 这里创建了一个新的Bitmap对象tSrcBmp，其尺寸与tScreenRect的尺寸相同，即与主屏幕的尺寸相同。这个Bitmap对象将用于保存屏幕截图。
            // 首先，通过Graphics.FromImage(tSrcBmp)方法获取与tSrcBmp关联的Graphics对象。然后，使用CopyFromScreen方法从屏幕复制图像到tSrcBmp。这里，复制的起始屏幕坐标是(0, 0)，目标Bitmap的起始坐标也是(0, 0)，要复制的区域大小是tScreenRect.Size，即整个屏幕的大小。
            Graphics gp = Graphics.FromImage(tSrcBmp);
            gp.CopyFromScreen(0, 0, 0, 0, tScreenRect.Size);
            gp.DrawImage(tSrcBmp, 0, 0, tScreenRect, GraphicsUnit.Pixel);
            return tSrcBmp;
        }

        // 获取指定矩形区域的屏幕截图  
        public Bitmap CaptureScreen(Rectangle bounds)
        {
            Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
            }

            return bitmap;
        }

        // 标注
        public Bitmap AddTextToImage(Bitmap image, string text, Font font, Color textColor, Point location)
        {
            using (Graphics graphics = Graphics.FromImage(image))
            {
                // 设置高质量插值法以改善图像质量  
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                // 绘制文本
                graphics.DrawString(text, font, new SolidBrush(textColor), location);
            }
            return image; // 注意：这里直接返回原始Bitmap对象（因为它已经被修改）  
        }

        private void picCut_Click(object sender, EventArgs e)
        {
            // 关闭计时器
            timer1.Enabled = false;
            mPosX = 0;
            mPosY = 0;
            // 全屏截图
            Bitmap screenshot = CaptureFullScreen();
            Bitmap annotatedImage = AddTextToImage(screenshot, "这是标注", new Font("微软雅黑", 20), Color.Red, new Point(50, 50)); // 在图像上添加文本  

            // 指定区域截图
            //Bitmap s = CaptureScreen(new Rectangle(100, 100, 200, 150));
            //Bitmap annotatedImage1 = AddTextToImage(screenshot, "这是标注", new Font("Arial", 12), Color.Red, new Point(50, 50)); // 在图像上添加文本  

            // 获取随机数
            Random random = new Random();
            int index = random.Next();
            
            // 保存截屏标注后的图片
            annotatedImage.Save("img//" + index + ".png");
        }


        // 上传文件
        private void picFile_Click(object sender, EventArgs e)
        {
            // 关闭计时器
            timer1.Enabled = false;
            mPosX = 0;
            mPosY = 0;
            // 获取消息类型
            messageType = "File&";
            string path = "";

            // 打开本地文件夹
            OpenFileDialog ofd = new OpenFileDialog();
            // 获取对应文件路径
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path = ofd.FileName;
            }

            try
            {
                using (FileStream fsReader = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    long sended = 0L; // 已发送总长度
                    long fileLength = fsReader.Length; // 获取文件的总长度
                    string fileName = Path.GetFileName(path); // 获取文件名
                    // 获取发送数据信息
                    string sendStr = messageType + ownNickName + "&" + fileName + "&" + fileLength.ToString();

                    // 发送数据
                    client.Send(Encoding.UTF8.GetBytes(sendStr));

                    // 每次发送的数据大小
                    const int BUFFERSIZE = 1024;
                    Console.WriteLine("发送文件：" + fileName + " 请等待……");
                    byte[] fileBuffer = new byte[BUFFERSIZE]; // 存储从文件中读取的数据的字节数组

                    int readCount; // 每次读取的字节长度
                    int sending; // 已发送字节长度
                    int readCountLeft;  // 剩余未发送字节长度

                    // 更新窗体
                    this.Invoke(new Action(() =>
                    {
                        rtbReceive.AppendText("我的消息: 正在上传文件" + fileName);
                    }));

                    // 循环发送文件内容
                    while ((readCount = fsReader.Read(fileBuffer, 0, BUFFERSIZE)) != 0)
                    {
                        sending = 0;
                        readCountLeft = readCount;
                        // 判断发送是否完成
                        while ((sending += client.Send(fileBuffer, sending, readCountLeft, SocketFlags.None)) < readCount)
                        {
                            readCountLeft = readCount - sending;
                        }
                        // 总字节长度增加
                        sended += sending;

                        // 追踪
                        Console.WriteLine("发送数据：{0}/{1}", sended.ToString(), fileLength.ToString());
                    }

                    // 追踪
                    Console.WriteLine("发送完毕……\n");
                    // 更新窗体
                    this.Invoke(new Action(() =>
                    {
                        rtbReceive.AppendText("...上传完成\n");
                    }));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // 打开表情窗体
        private void picEmotion_Click(object sender, EventArgs e)
        {
            form2 = new EmotionForm(this);
            form2.Left = this.Left;
            form2.Top = this.Top + 210;
            form2.Show();
        }


        // 更多
        private void picMore_Click(object sender, EventArgs e)
        {
            MessageBox.Show("尽情期待");
        }


        // 蛋糕雨
        private void timer1_Tick(object sender, EventArgs e)
        {
            mPosY += 10;
            this.Invalidate();
        }

        private void Chat_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Random r = new Random();
            g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao.png"), mPosX, mPosY, 20, 20);
            if (isShow == true)
            {
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao.png"), mPosX, mPosY, 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao1.png"), mPosX + r.Next(0, 700), mPosY - r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao2.png"), mPosX + r.Next(0, 700), mPosY - r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao3.png"), mPosX + r.Next(0, 700), mPosY + r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao4.png"), mPosX + r.Next(0, 700), mPosY + r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao5.png"), mPosX + r.Next(0, 700), mPosY + r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao6.png"), mPosX + r.Next(0, 700), mPosY - r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao7.png"), mPosX + r.Next(0, 700), mPosY - r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao8.png"), mPosX + r.Next(0, 700), mPosY - r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao9.png"), mPosX + r.Next(0, 700), mPosY - r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao10.png"), mPosX + r.Next(0, 700), mPosY - r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao11.png"), mPosX + r.Next(0, 700), mPosY - r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao12.png"), mPosX + r.Next(0, 700), mPosY - r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao13.png"), mPosX + r.Next(0, 700), mPosY - r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao14.png"), mPosX + r.Next(0, 700), mPosY - r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao15.png"), mPosX + r.Next(0, 700), mPosY - r.Next(0, 200), 20, 20);
                g.DrawImage(Image.FromFile(@"C:\picture\dangao\dangao16.png"), mPosX + r.Next(0, 700), mPosY - r.Next(0, 200), 20, 20);
            }
        }

        // 开启计时器，展示蛋糕雨
        private void open()
        {
            timer1.Enabled = true;
            isShow = true;
        }



        // 最小化窗体
        private void picMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        // 两张图片比较是否相等
        private bool ImageCompareArray(Bitmap firstImage, Bitmap secondImage)
        {
            bool flag = true;
            string firstPixel;
            string secondPixel;

            if (firstImage.Width == secondImage.Width
                && firstImage.Height == secondImage.Height)
            {
                for (int i = 0; i < firstImage.Width; i++)
                {
                    for (int j = 0; j < firstImage.Height; j++)
                    {
                        firstPixel = firstImage.GetPixel(i, j).ToString();
                        secondPixel = secondImage.GetPixel(i, j).ToString();
                        if (firstPixel != secondPixel)
                        {
                            flag = false;
                            break;
                        }
                    }
                }

                if (flag == false)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        Image image = Properties.Resources.Maximize_1;

        // 最大化与还原

        private void picMax_Click(object sender, EventArgs e)
        {
            Bitmap map1 = new Bitmap(picMax.BackgroundImage);
            Bitmap map2 = new Bitmap(image);
            if (ImageCompareArray(map1, map2))
            {
                this.WindowState = FormWindowState.Maximized;
                picMax.BackgroundImage = Properties.Resources.窗口还原;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                picMax.BackgroundImage = Properties.Resources.Maximize_1;
            }
        }
        // 关闭窗体
        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
