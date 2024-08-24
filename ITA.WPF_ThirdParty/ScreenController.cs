using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPF_ThirdParty.Windows;

namespace WPF_ThirdParty
{
    public class ScreenController
    {
        protected readonly BaseWindowWithChangableContent _window;
        private static ScreenController _screenController;

        public ScreenController(BaseWindowWithChangableContent window)
        {
            _window = window;
            window.Closing += Window_Closing;
            window.Closed += Window_Closed;
            _screenController = this;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            VerifyIfChangesWereSaved();
            OnWindowsClosing();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

            Application.Current.Shutdown();
        }

        public static ScreenController Instance
        {
            get => _screenController;
        }

        public BaseWindowWithChangableContent Window
        {
            get => _window;
        }

        public virtual void SetWindowContent(UserControl userControl)
        {
            _window.SetWindowContent(userControl);
        }

        protected virtual void VerifyIfChangesWereSaved()
        {
        }

        protected virtual void OnWindowsClosing()
        {
        }

        public void CloseWindow()
        {
            _window.Close();
        }

        public void ShowWindow()
        {
            _window.Show();
        }

        public void ShowDialog()
        {
            _window.ShowDialog();
        }
    }
}
