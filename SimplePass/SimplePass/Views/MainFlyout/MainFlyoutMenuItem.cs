using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePass.Views.MainFlyout
{
    public class MainFlyoutMenuItem
    {
        public MainFlyoutMenuItem()
        {
            TargetType = typeof(MainFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}