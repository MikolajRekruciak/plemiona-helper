using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_ThirdParty.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class FullScreenWindow : BaseWindowWithChangableContent
    {
        private event OnClosingWindow _onClosingWindow;
        public delegate void OnClosingWindow ();

        private FullScreenWindow() { }
        public FullScreenWindow(string windowTitle)
        {
            InitializeComponent();
            this.Title = windowTitle;
        }
        public FullScreenWindow(string windowTitle, OnClosingWindow onClosingEventHandler) : this(windowTitle)
        {
            Closing += FullScreenWindow_Closing;
            _onClosingWindow += onClosingEventHandler;
        }

        private void FullScreenWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _onClosingWindow();
        }

        public override void SetWindowContent(UserControl userControl)
        {
            WindowContentControl.Content = userControl;
        }
    }
}
