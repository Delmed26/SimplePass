using System;
using System.Collections.Generic;
using System.Text;

namespace SimplePass.Models
{
    public class User
    {
        public String email { get; set; }
        public String password { get; set; }

        public User()
        {
            this.email = String.Empty;
            this.password = String.Empty;
        }
    }
}
