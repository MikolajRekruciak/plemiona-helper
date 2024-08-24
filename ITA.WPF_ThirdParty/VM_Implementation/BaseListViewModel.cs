using ITA.WPF_ReuseableUserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using WPF_ThirdParty.VM_Implementation.Filtering;

namespace WPF_ThirdParty.VM_Implementation
{
    public class BaseListViewModel<T> : BaseViewModel
    {
        protected Action _actionAfterCollectionChanged;
        public FilteringService FilterService;

        public BaseListViewModel(ObservableCollection<T> itemsSource,
            FilteringService filteringService = null)
        {
            if (filteringService == null)
                filteringService = new FilteringService();
            FilterService = filteringService;
            FilterService.SetPropertiesToFilterBy(typeof(T));

            ItemsSource = itemsSource;
        }

        //Wymaga ręcznego przypisana ItemssSource
        public BaseListViewModel()
        {
            FilterService = new FilteringService();
            FilterService.SetPropertiesToFilterBy(typeof(T));
        }

        private ObservableCollection<T> _itemsSource;
        public ObservableCollection<T> ItemsSource
        {
            get => _itemsSource;
            set
            {
                if (SetProperty(ref _itemsSource, value))
                {
                    _collectionView = CollectionViewSource.GetDefaultView(ItemsSource);
                    _collectionView.Filter = FilterService.FrameworkFilter;
                }
            }
        }

        protected ICollectionView _collectionView;

        private string _searchKeyword;
        public string SearchKeyword
        {
            get => _searchKeyword;
            set
            {
                if (SetProperty(ref _searchKeyword, value))
                {
                    FilterService.SetValueToFilterBy(value);
                    _collectionView.Refresh();
                    if (_actionAfterCollectionChanged != null)
                        _actionAfterCollectionChanged.Invoke();
                }
            }
        }

        public ICommand EditSearchKeywordWithVirtualKeyboardCommand => new RelayCommand(() =>
        {
            string newSearchKeyword = FullKeyboard.ShowKeyboard(SearchKeyword);
            if (newSearchKeyword != null)
                SearchKeyword = newSearchKeyword;
        });
    }
}
