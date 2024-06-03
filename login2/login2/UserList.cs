using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login2
{
    public class UserList
    {
        public List<User> listUsers = new List<User>(); // 用户列表

        // 封装字段
        internal List<User> ListUsers { get => listUsers; set => listUsers = value; }
    }
}
