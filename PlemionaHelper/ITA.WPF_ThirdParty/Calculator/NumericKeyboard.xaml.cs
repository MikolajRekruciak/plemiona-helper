using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
using WPF_ThirdParty.ExtensionMethods;
using WPF_ThirdParty.VM_Implementation;
using WPF_ThirdParty.Windows;

namespace WPF_ThirdParty.Calculator
{
    /// <summary>
    /// Interaction logic for NumericKeyboard.xaml
    /// </summary>
    public partial class NumericKeyboard : UserControl, INotifyPropertyChanged
    {
        private decimal? _returnValue;
        private CultureInfo _cultureInfo; // cultureInfo with . as group separator and , as decimal separator
        private ShowDialogWindow _showDialogWindow;

        public NumericKeyboard(ShowDialogWindow showDialogWindow, string inputAsString = null)
        {
            _showDialogWindow = showDialogWindow;

            InitializeComponent();

            TextBoxValue = inputAsString;

            _cultureInfo = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            _cultureInfo.NumberFormat.NumberDecimalSeparator = ",";
            _cultureInfo.NumberFormat.NumberGroupSeparator = ".";

            DataContext = this;

            Loaded += (sender, e) =>
            {
                ValueTextBox.Focus();
                ValueTextBox.SelectAllText();
            };

        }

        private string _textBoxValue;
        public string TextBoxValue
        {
            get => _textBoxValue;
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    _returnValue = null;
                    _textBoxValue = "";
                    OnPropertyChanged(nameof(TextBoxValue));
                }
                else if (Decimal.TryParse(value, NumberStyles.Any, _cultureInfo, out decimal result))
                {
                    _returnValue = result;
                    _textBoxValue = value;
                    OnPropertyChanged(nameof(TextBoxValue));
                }
            }
        }



        public static bool ShowKeyboard(decimal input, out decimal output)
        {
            ShowDialogWindow showDialogWindow = new ShowDialogWindow();
            NumericKeyboard numericKeyboard = new NumericKeyboard(
                showDialogWindow, 
                input != 0 ? input.ToString() : null);
            showDialogWindow.SetWindowContent(numericKeyboard);
            showDialogWindow.ShowDialog();

            if (numericKeyboard._returnValue != null)
            {
                output = (decimal)numericKeyboard._returnValue;
                return true;
            }
            else
            {
                output = 0;
                return false;
            }
        }


        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(TextBoxValue) &&
                ValueTextBox.SelectionLength == ValueTextBox.Text.Length)
            {
                TextBoxValue = (sender as Button).Content.ToString();
            }
            else
            {
                TextBoxValue += (sender as Button).Content;
            }
        }

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            TextBoxValue = TextBoxValue.Any()
                ? TextBoxValue.Remove(TextBoxValue.Length - 1)
                : TextBoxValue;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            TextBoxValue = "";
        }

        public ICommand EnterKeyCommand => new RelayCommand(() =>
        {
            if (String.IsNullOrEmpty(TextBoxValue))
                TextBoxValue = "0";
            _showDialogWindow.Close();
        });

        public ICommand EscapeKeyCommand => new RelayCommand(() =>
        {
            _returnValue = null;
            _showDialogWindow.Close();
        });

    }
}
