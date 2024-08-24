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
    public partial class BasicWindow : BaseWindowWithChangableContent
    {
        private BasicWindow() { }
        public BasicWindow(string windowTitle) : this(windowTitle, false)
        {
        }

        public BasicWindow(string windowTitle, bool showMaximized)
        {
            InitializeComponent();
            Title = windowTitle;
            //TopBorderPanel.MouseDown += TopBorderPanel_MouseDown;
            if (showMaximized)
                base.WindowState = WindowState.Maximized;
        }

        private void TopBorderPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                base.DragMove();

            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
                SwitchWindowStateButton_Click(sender, null);
        }

        private void SwitchWindowStateButton_Click(object sender, RoutedEventArgs e)
        {
            base.WindowState = base.WindowState == WindowState.Maximized ?
                WindowState.Normal :
                WindowState.Maximized;
        }

        private void MinimalizeButton_Click(object sender, RoutedEventArgs e)
        {
            base.WindowState = WindowState.Minimized;
        }

        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            base.Close();
        }

        public override void SetWindowContent(UserControl content)
        {
            WinowContentControl.Content = content;
        }
    }
}
