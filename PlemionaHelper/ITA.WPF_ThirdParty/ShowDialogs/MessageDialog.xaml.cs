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
    /// Interaction logic for MessageDialog.xaml
    /// </summary>
    public partial class MessageDialog : UserControl, INotifyPropertyChanged
    {
        private BaseWindowWithChangableContent _wind;
        public bool _returnValue = false;

        public string Text { get; set; }

        private string _trueButtonText;
        public string TrueButtonText
        {
            get { return _trueButtonText; }
            set { _trueButtonText = value; OnPropertyChanged("TrueButtonText"); }
        }

        private string _falseButtonText;
        public string FalseButtonText
        {
            get { return _falseButtonText; }
            set { _falseButtonText = value; OnPropertyChanged("FalseButtonText"); }
        }

        private string _okButtonText;
        public string OkButtonText
        {
            get { return _okButtonText; }
            set { _okButtonText = value; OnPropertyChanged("OkButtonText"); }
        }

        #region Ctor
        private MessageDialog(string text)
        {
            DataContext = this;
            Text = text;
            Loaded += ((sender, e) => InvisibleTextBox.Focus());
        }

        private MessageDialog(string question, string trueButtonText, string falseButtonText) : this(question)
        {
            TrueButtonText = trueButtonText;
            FalseButtonText = falseButtonText;
            InitializeComponent();
            _wind = new ShowDialogWindow();
            _wind.SetWindowContent(this);
            _wind.ShowDialog();
        }

        private MessageDialog(string message, string okButtonText) : this(message)
        {
            OkButtonText = okButtonText;
            InitializeComponent();
            _wind = new ShowDialogWindow();
            _wind.SetWindowContent(this);
            _wind.ShowDialog();
        }
        #endregion
        
        #region Static methods
        public static bool AskQuestion(string question, string trueButtonText, string falseButtonText)
        {
            var dialogBox = new MessageDialog(question, trueButtonText, falseButtonText);
            return dialogBox._returnValue;
        }

        public static bool AskQuestion(string question)
        {
            return AskQuestion(question, "TAK", "NIE");
        }

        public static void ShowMessage(string message)
        {
            ShowMessage(message, "OK");
        }

        public static void ShowMessage(string message, string okButtonText)
        {
            var dialogBox = new MessageDialog(message, okButtonText);
        }
        #endregion
        
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Clicks
        private void True()
        {
            _returnValue = true;
            _wind.Close();
        }

        private void False()
        {
            _returnValue = false;
            _wind.Close();
        }

        private void Ok()
        {
            _wind.Close();
        }

        private void TrueButton_Click(object sender, RoutedEventArgs e)
        {
            True();
        }

        private void FalseButton_Click(object sender, RoutedEventArgs e)
        {
            False();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Ok();
        }
        #endregion

        public ICommand ClickedEnterCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (OkButton.Visibility == Visibility.Visible && !String.IsNullOrEmpty(OkButton.Content?.ToString()))
                        Ok();
                    else if (TrueButton.Visibility == Visibility.Visible && !String.IsNullOrEmpty(TrueButton.Content?.ToString()))
                        True();
                });
            }
        }

        public ICommand ClickedEscapeCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (FalseButton.Visibility == Visibility.Visible && !String.IsNullOrEmpty(FalseButton.Content?.ToString()))
                    {
                        False();
                    }
                });
            }
        }

        private void InvisibleTextBox_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            e.Handled = true;
        }
    }
}
