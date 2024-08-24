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
using WPF_ThirdParty.ExtensionMethods;
using WPF_ThirdParty.Windows;

namespace ITA.WPF_ReuseableUserControls
{
    /// <summary>
    /// Interaction logic for Keyboard.xaml
    /// </summary>
    public partial class FullKeyboard : UserControl
    {
        private ShowDialogWindow _showDialogWindow;
        public FullKeyboard(ShowDialogWindow showDialogWindow)
        {
            _showDialogWindow = showDialogWindow;
            InitializeComponent();

            showDialogWindow.SetWindowContent(this);

            Loaded += Keyboard_Loaded;
        }

        private void Keyboard_Loaded(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Focus();
            ValueTextBox.Select(ValueTextBox.Text.Length, 0);
        }

        public static string ret;

        public static string ShowKeyboard()
        {
            return ShowKeyboard(null);
        }

        public static string ShowKeyboard(string text)
        {
            ShowDialogWindow showDialogWindow = new ShowDialogWindow(
                new ShowDialogWindow.SizeOptions(
                    Math.Min(1000, SystemParameters.WorkArea.Width * 4 / 5),
                    Math.Min(600, SystemParameters.WorkArea.Height * 4 / 5)));

            FullKeyboard kb = new FullKeyboard(showDialogWindow);
            if (text != null)
                kb.ValueTextBox.Text = text;
            ret = null;

            showDialogWindow.ShowDialog();
            return ret;
        }

        private void Number1Button_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "1";
        }

        private void Number2Button_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "2";
        }

        private void Number3Button_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "3";
        }

        private void Number4Button_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "4";
        }

        private void Number5Button_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "5";
        }

        private void Number6Button_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "6";
        }

        private void Number7Button_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "7";
        }

        private void Number8Button_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "8";
        }

        private void Number9Button_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "9";
        }

        private void Number0Button_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "0";
        }

        private void LetterQButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "Q";

        }

        private void LetterWButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "W";

        }

        private void LetterRButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "R";

        }

        private void LetterEButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "E";

        }

        private void LetterTButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "T";

        }

        private void LetterYButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "Y";

        }

        private void LetterUButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "U";

        }

        private void LetterIButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "I";

        }

        private void LetterOButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "O";

        }

        private void LetterPButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "P";

        }

        private void LetterAButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "A";

        }

        private void LetterSButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "S";

        }

        private void LetterDButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "D";

        }

        private void LetterFButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "F";

        }

        private void LetterGButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "G";

        }

        private void LetterHButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "H";

        }

        private void LetterJButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "J";

        }

        private void LetterKButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "K";

        }

        private void LetterLButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "L";

        }

        private void LetterCommaButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += ",";

        }

        private void LetterZButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "Z";

        }

        private void LetterXButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "X";

        }

        private void LetterCButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "C";

        }

        private void LetterVButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "V";

        }

        private void LetterBButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "B";

        }

        private void LetterMButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "M";

        }

        private void LetterNButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "N";

        }

        private void LetterDotButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += ".";

        }

        private void LetterMinusButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "-";

        }

        private void LetterSlashButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += "/";

        }

        private void SpacebarButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text += " ";
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValueTextBox.Text.Length > 0)
                ValueTextBox.Text = ValueTextBox.Text.Substring(0, ValueTextBox.Text.Length - 1);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ValueTextBox.Text = "";
        }

        private void AnulujButton_Click(object sender, RoutedEventArgs e)
        {
            ret = null;
            _showDialogWindow.Close();
        }

        private void PotwierdzButton_Click(object sender, RoutedEventArgs e)
        {
            ret = ValueTextBox.Text;
            _showDialogWindow.Close();
        }

        private void ValueTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ret = ValueTextBox.Text;
                _showDialogWindow.Close();
            }
        }

        private void ValueTextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                ret = ValueTextBox.Text;
                _showDialogWindow.Close();
            }
            if (e.Key == Key.Escape)
            {
                e.Handled = true;
                ret = null;
                _showDialogWindow.Close();
            }

        }
    }
}
