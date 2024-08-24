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
using System.Windows.Shapes;

namespace PlemionaHelper.ShowDialogs
{
    /// <summary>
    /// Interaction logic for GetWioskaIdShowDialog.xaml
    /// </summary>
    public partial class GetWioskaIdShowDialog : Window
    {
        private int ret;

        public GetWioskaIdShowDialog()
        {
            InitializeComponent();
            Loaded += GetWioskaIdShowDialog_Loaded;
        }

        private void GetWioskaIdShowDialog_Loaded(object sender, RoutedEventArgs e)
        {
            InputTextBox.Focus();
        }

        public static int GetId()
        {
            GetWioskaIdShowDialog wind = new GetWioskaIdShowDialog();
            wind.ShowDialog();
            return wind.ret;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(InputTextBox.Text, out ret))
                Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            ret = 0;
            Close();
        }
    }
}
