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
    public partial class ShowDialogWindow : BaseWindowWithChangableContent
    {
        public class SizeOptions
        {
            public double Width;
            public double Height;
            private SizeOptions() { }
            public SizeOptions(double width, double height)
            {
                Width = width;
                Height = height;
            }
        }

        public ShowDialogWindow()
        {
            InitializeComponent();
        }

        public ShowDialogWindow(SizeOptions sizeOptions) : this()
        {
            base.MaxWidth = base.MinWidth = sizeOptions.Width;
            base.MaxHeight = base.MinHeight = sizeOptions.Height;
            base.Left = (SystemParameters.WorkArea.Width - sizeOptions.Width) / 2;
            base.Top = (SystemParameters.WorkArea.Height - sizeOptions.Height) / 2;
        }

        public override void SetWindowContent(UserControl content)
        {
            WindowContentControl.Content = content;
        }
    }
}
