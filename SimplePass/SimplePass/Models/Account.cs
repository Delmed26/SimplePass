using Plugin.CloudFirestore.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimplePass.Models
{
    public class Account : User
    {
        public String site { get; set; }
        [Ignored]
        public string id { get; internal set; }

        public Account()
        {
            this.id = String.Empty;
            this.site = String.Empty;
            this.email = String.Empty;
            this.password = String.Empty;
        }

    }
}
