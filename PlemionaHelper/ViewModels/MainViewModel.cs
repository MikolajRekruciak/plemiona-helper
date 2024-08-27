using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PlemionaHelper.Services;
using PlemionaHelper.ShowDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WPF_ThirdParty;
using WPF_ThirdParty.VM_Implementation;

namespace PlemionaHelper.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            botTask = new Task(() =>
            {
                while (true)
                {
                    foreach (var wioska in Wioski.Where(q => q.AttackInterval > TimeSpan.FromSeconds(1)))
                    {
                        if (wioska.DataWyslaniaOstatniegoAtaku == (DateTime?)null ||
                            (DateTime.Now - (DateTime)wioska.DataWyslaniaOstatniegoAtaku) >=
                            wioska.AttackInterval)
                        {
                            WyslijAtak(wioska);
                        }

                        wioska.OnPropertyChanged(nameof(wioska.NextAttackInterval));
                    }

                    Thread.Sleep(1000);
                }
            });
        }

        private IWebElement TextBoxZwiad()
        {
            int i = 0;
            while (i < 20)
            {
                try
                {
                    return driver.FindElement(By.Id("unit_input_spy")); 
                }
                catch (Exception ex)
                {
                    PlaySound_OchronaBotowa();
                    Thread.Sleep(2000);
                }
            }

            return null;
        }

        private MediaPlayer mediaPlayer = new MediaPlayer();
        private void PlaySound_OchronaBotowa()
        {
            string relativePath = "Sounds/Ochrona_botowa.mp3";
            string absolutePath = System.IO.Path.GetFullPath(relativePath);
            mediaPlayer.Open(new Uri(absolutePath, UriKind.Absolute));
            mediaPlayer.Play();
        }

        public ICommand SoundTestCommand => new RelayCommand(() =>
        {
            PlaySound_OchronaBotowa();
        });

        private IWebElement Sumbminit()
        {
                try
                {
                    return driver.FindElement(By.Id("troop_confirm_submit"));
                }
                catch (Exception ex)
                {
                    return null;
                }
        }

        private void WyslijAtak(WioskaViewModel wioska)
        {
            // Otwórz stronę
            driver.Navigate().GoToUrl($"https://pl203.plemiona.pl/game.php?village={Settings.Wioska_Id}&screen=place&target={wioska.Wioska.Id}");

            // Znajdź pole tekstowe i wprowadź tekst
            IWebElement textBoxZwiad = TextBoxZwiad();
            if (textBoxZwiad == null)
                return;
            textBoxZwiad.SendKeys("1");

            // Znajdź pole tekstowe i wprowadź tekst
            IWebElement textBoxLekka = driver.FindElement(By.Id("unit_input_light"));
            textBoxLekka.SendKeys($"{wioska.HorsesAssigned}");

            // Znajdź przycisk i kliknij
            IWebElement button = driver.FindElement(By.Id("target_attack"));
            button.Click();

            // Poczekaj na załadowanie nowej strony
            System.Threading.Thread.Sleep(800); // Możesz użyć bardziej zaawansowanych metod oczekiwania

            // Znajdź przycisk potwierdzenia i kliknij
            IWebElement confirmButton = Sumbminit();
            if (confirmButton != null)
                confirmButton.Click(); // Jak nie mamy ludzików żeby wysłać, to po prostu skipujemy ;/

            wioska.DataWyslaniaOstatniegoAtaku = DateTime.Now;
        }

        public ObservableCollection<WioskaViewModel> Wioski { get; set; }

        private static MainViewModel _instance;

        public static MainViewModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MainViewModel
                    {
                        Wioski = new ObservableCollection<WioskaViewModel>(
                            IO_Service.GetSavedState().Select(q => new WioskaViewModel(q))),
                    };

                return _instance;
            }
        }

        Task botTask;

        public ICommand StartBotting => new RelayCommand(() =>
        {
            if (botTask.Status != TaskStatus.Created)
                return;

            botTask.Start();
        });

        public ICommand RemoveWioska => new RelayCommand<WioskaViewModel>((wioska) =>
        {
            Wioski.Remove(wioska);
        });

        public ICommand RefreshCommand => new RelayCommand(() =>
        {
            foreach (var wioska in Wioski)
            {
                //wioska.Refresh();
            }
        });

        public ICommand LoadReportCommand => new RelayCommand(() =>
        {
            string reportUrl = Clipboard.GetText();
            if (String.IsNullOrEmpty(reportUrl) ||
                !Uri.IsWellFormedUriString(reportUrl, UriKind.Absolute))
            {
                MessageBox.Show($@"Copy valid plemiona scout report url.");
                return;
            }


            Wioska ret = ReadRaportService.ReadRaport(reportUrl);
            if (ret == null)
                return;

            int id = GetWioskaIdShowDialog.GetId();
            if (id == 0)
                return;

            ret.Id = id;

            var wioskaDoAktualizacji = Wioski.FirstOrDefault(q => q.Wioska.Id == id);
            if (wioskaDoAktualizacji != null)
                Wioski.Remove(wioskaDoAktualizacji);

            MainViewModel.Instance.Wioski.Add(new WioskaViewModel(ret));
        });

        IWebDriver driver;

        public ICommand LaunchPlemionaCommand => new RelayCommand(() =>
        {
            // Ustawienia przeglądarki
            driver = new ChromeDriver();

            // Otwórz stronę
            driver.Navigate().GoToUrl("https://pl203.plemiona.pl/game.php?village=43404&screen=place&target=45285");

        });

        public ICommand SendAttackCommand => new RelayCommand(() =>
        {
            //// Ustawienia przeglądarki
            //IWebDriver driver = new ChromeDriver();

            // Zamknij przeglądarkę
            //driver.Quit();
        });
    }
}
