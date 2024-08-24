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
    public class OnStartup
    {
        public static void StartApplication(UserControl userControl, BaseWindowWithChangableContent mainWindow)
        {
            mainWindow.SetWindowContent(userControl);

            Application.Current.MainWindow = mainWindow;

            new ScreenController(mainWindow);

            ScreenController.Instance.ShowWindow();
        }
    }
}
