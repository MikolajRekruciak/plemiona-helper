using PlemionaHelper.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_ThirdParty;
using WPF_ThirdParty.VM_Implementation;

namespace PlemionaHelper.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
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

        public ICommand RefreshCommand => new RelayCommand(() =>
        {
            foreach (var wioska in Wioski)
            {
                wioska.Refresh();
            }
        });
    }
}
