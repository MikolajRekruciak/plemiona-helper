using PlemionaHelper.Services;
using PlemionaHelper.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace PlemionaHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = MainViewModel.Instance;
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            IO_Service.SaveCurrentState(MainViewModel.Instance.Wioski.Select(q => q.Wioska).ToList());
        }

        private void LoadRaportButton_Click(object sender, RoutedEventArgs e)
        {
            Wioska ret = ReadRaportService.ReadRaport(
                @"https://pl203.plemiona.pl/public_report/c2456c4d9dfd9d55b4007283bf89a760");

            MainViewModel.Instance.Wioski.Add(new WioskaViewModel(ret));
        }
    }
}
