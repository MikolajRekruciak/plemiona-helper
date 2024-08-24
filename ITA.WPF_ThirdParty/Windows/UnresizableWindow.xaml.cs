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
    public partial class UnresizableWindow : BaseWindowWithChangableContent
    {
        private UnresizableWindow() { }
        public UnresizableWindow(string windowTitle, double width, double height)
        {
            InitializeComponent();
            TitleTextBlock.Text = windowTitle;
            base.MaxWidth = base.MinWidth = width;
            base.MaxHeight = base.MinHeight = height;
        }

        private void TopBorderPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                base.DragMove();
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
