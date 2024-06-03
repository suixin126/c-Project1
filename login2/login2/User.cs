using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login2
{
    public class User
    {
        private string sex = ""; // 性别
        private int age = 0; // 年龄
        private string phone = null; // 电话
        private string userName = null; // 用户名
        private string password = null; // 密码
        private string nickName = null; // 昵称
        private string motto = null; // 座右铭
        private string head = null; // 头像
        private int isOnline = 0; // 在线状况
        private List<Friends> friends = new List<Friends>(); // 好友列表

        // 构造函数
        public User()
        {

        }

        // 封装函数
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public string NickName { get => nickName; set => nickName = value; }
        public string Motto { get => motto; set => motto = value; }
        public string Head { get => head; set => head = value; }
        public int IsOnline { get => isOnline; set => isOnline = value; }
        public List<Friends> Friends { get => friends; set => friends = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Sex { get => sex; set => sex = value; }
        public int Age { get => age; set => age = value; }
    }
}
