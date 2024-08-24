using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPF_ThirdParty.Windows
{
    public abstract class BaseWindowWithChangableContent : Window, IChangableContentWindow
    {
        public abstract void SetWindowContent(UserControl userControl);

    }
}
