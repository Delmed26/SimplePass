using System;
using System.Collections.Generic;
using System.Text;

namespace SimplePass.Models
{
    public class Account : User
    {
        public String site { get; set; }

        public Account()
        {
            this.site = String.Empty;
            this.email = String.Empty;
            this.password = String.Empty;
        }

    }
}
