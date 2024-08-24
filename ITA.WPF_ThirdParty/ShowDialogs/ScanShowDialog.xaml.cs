using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using WPF_ThirdParty.VM_Implementation;
using WPF_ThirdParty.Windows;

namespace WPF_ThirdParty.ShowDialogs
{
    /// <summary>
    /// Interaction logic for ScanShowDialog.xaml
    /// </summary>
    public partial class ScanShowDialog : UserControl, INotifyPropertyChanged
    {
        public string HeaderText { get; set; }
        public string ReturnValue { get; set; } //Its binded to textbox

        private ShowDialogWindow _showDialogWindow;

        private ScanShowDialog(ShowDialogWindow showDialogWindow)
        {
            InitializeComponent();
            DataContext = this;
            Loaded += ((sender, e) => InsertedValueTextBox.Focus());
            _showDialogWindow = showDialogWindow;
        }

        public ICommand EnterKeyCommand => new RelayCommand(() =>
        {
            _showDialogWindow.Close();
        }, () => !String.IsNullOrEmpty(ReturnValue));

        public ICommand EscapeKeyCommand => new RelayCommand(() =>
        {
            ReturnValue = null;
            _showDialogWindow.Close();
        });

        public static string ShowDialog(string headerText, string defaultContent = null)
        {
            ShowDialogWindow showDialogWindow = new ShowDialogWindow();
            ScanShowDialog scanShowDialog = new ScanShowDialog(showDialogWindow)
            {
                HeaderText = headerText,
                ReturnValue = defaultContent,
            };
            showDialogWindow.SetWindowContent(scanShowDialog);
            showDialogWindow.ShowDialog();
            return scanShowDialog.ReturnValue;
        }



        private void AnulujButton_Click(object sender, RoutedEventArgs e)
        {
            ReturnValue = null;
            _showDialogWindow.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
