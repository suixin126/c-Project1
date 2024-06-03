using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login2
{
    public class Friends
    {
        private string ownUserName = null; // 自己用户名
        private string friendName = null; // 好友用户名（唯一标识）
        private string nickName = null; // 昵称
        private string head = null; // 头像
        private int isOnline = 0; // 在线状况

        // 封装字段
        public string FriendName { get => friendName; set => friendName = value; }
        public int IsOnline { get => isOnline; set => isOnline = value; }
        public string OwnUserName { get => ownUserName; set => ownUserName = value; }
        public string Head { get => head; set => head = value; }
        public string NickName { get => nickName; set => nickName = value; }
    }
}
