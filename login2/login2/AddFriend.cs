using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace login2
{
    public partial class AddFriend : Form
    {
        User user = new User();
        Friends friend = new Friends();
        private string textContent = "";
        public AddFriend(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        // 获取圆形图片
        private Image CutEllipse(Image img, Rectangle rec, Size size)
        {
            Bitmap bitmap = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // 该对象使用传入的 img 作为纹理，并使用 rec 矩形来定义纹理的边界。WrapMode.Clamp 表示纹理在边界之外的部分将被裁剪。
                using (TextureBrush br = new TextureBrush(img, System.Drawing.Drawing2D.WrapMode.Clamp, rec))
                {
                    // 缩放 TextureBrush
                    br.ScaleTransform(bitmap.Width / (float)rec.Width, bitmap.Height / (float)rec.Height);
                    // 设置 Graphics 对象的平滑模式
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    //椭圆的边界由 new Rectangle(Point.Empty, size) 定义，其中 Point.Empty 表示椭圆的左上角位于坐标 (0,0)，而 size 定义了椭圆的宽度和高度。
                    g.FillEllipse(br, new Rectangle(Point.Empty, size));
                }
            }
            return bitmap;
        }


        // 查询信息
        private void show_Click(object sender, EventArgs e)
        {
            textContent = textBox1.Text;


            // 连接数据库
            string connectionString = "Server='localhost';Database='db1';User='root';Password='123456'";
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string sql = "SELECT * FROM userslist";
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                // 将数据库内容添加到用户列表
                while (reader.Read())
                {
                    // 0：用户名 1：密码 2：昵称 3：格言 4：头像 5：电话 6：是否在线
                    Friends friend = new Friends();
                    friend.OwnUserName = reader[0].ToString();
                    friend.NickName = reader[2].ToString();
                    friend.Head = reader[4].ToString();
                    friend.IsOnline = int.Parse(reader[6].ToString());
                    if (friend.OwnUserName.Equals(textContent))
                    {
                        this.friend = friend;
                        break;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            // 查询好友的头像
            Image image = Image.FromFile(friend.Head);
            this.picHead.BackgroundImage = CutEllipse(image, new Rectangle(0, 0, image.Width, image.Height), new Size(80, 80));

            // 查询好友的用户名和昵称
            labUserName.Text = friend.OwnUserName;
            labNickName.Text = friend.NickName;

        }

        // 添加好友
        private void add_Click(object sender, EventArgs e)
        {
            user.Friends.Add(friend);
            MessageBox.Show("添加成功");

            // 同步数据库
            string connectionString = "Server='localhost';Database='db1';User='root';Password='123456'";
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

                int count = user.Friends.Count;

                string sql = "UPDATE friends SET f" + count + "= @newFriend WHERE own = @name";
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@newFriend", textContent);
                cmd.Parameters.AddWithValue("@name", user.UserName);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Main main = new Main(user);
            this.Close();
            main.Show();
        }
    }
}
