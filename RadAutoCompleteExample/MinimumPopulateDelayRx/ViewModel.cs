using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Telerik.Windows.Controls;
using System.Reactive.Linq;
using ReactiveUI;

namespace MinimumPopulateDelay
{
    public class ViewModel : ReactiveObject
    {
        private ObservableCollection<Item> _items;
        private ObservableCollection<int> delays;
        private int selectedDelay;
        
        private ObservableAsPropertyHelper<ObservableCollection<Item>> _searchResults;
        private string _searchText;
        private ICommand _executeSearchCommand;
        private bool _isBusy;

        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                if (_searchText != value && !(_searchText == null || value == ""))
                {
                    IsBusy = true;
                }
                else
                {
                    IsBusy = false;
                }

                _searchText = value;
                raisePropertyChanged("SearchText");
            }
        }

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                raisePropertyChanged("IsBusy");
            }
        }


        public ViewModel()
        {
            //GetItems();
            this.delays = new ObservableCollection<int>()
            {
                1, 2, 3, 4, 5
            };
            this.selectedDelay = this.delays[1];

            var executeSearchCommand = new ReactiveCommand();
            var results = executeSearchCommand.RegisterAsyncFunction(s => { return ExecuteSearch(s as string); });
            _executeSearchCommand = executeSearchCommand;

            this.ObservableForProperty<ViewModel, string>("SearchText")
                .Throttle(TimeSpan.FromMilliseconds(800))
                .Select(x => x.Value)
                .DistinctUntilChanged()
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Subscribe(_executeSearchCommand.Execute);

            _searchResults = new ObservableAsPropertyHelper<ObservableCollection<Item>>(results, _ => raisePropertyChanged("SearchResults"));

        }

        public int SelectedDelay
        {
            get
            {
                return this.selectedDelay;
            }

            set
            {
                if (this.selectedDelay != value)
                {
                    this.selectedDelay = value;
                    raisePropertyChanged("SelectedDelay");
                }
            }
        }

        public ObservableCollection<int> Delays
        {
            get
            {
                return this.delays;
            }

            set
            {
                if (this.delays != value)
                {
                    this.delays = value;
                    raisePropertyChanged("Delays");
                }
            }
        }

        public ObservableCollection<Item> SearchResults
        {
            get
            {
                return _searchResults.Value;
            }
        }

        private ObservableCollection<Item> GetItems(int size)
        {
            var result = new ObservableCollection<Item>();

            for (int i = 0; i < size; i++)
            {
                result.Add(new Item() { Name = string.Format("Item {0}", i), Number = i });
            }

            return result;
        }


        private ObservableCollection<Item> ExecuteSearch(string searchText)
        {
            //var q = from s in _repository where s.ToLower().StartsWith(searchText.ToLower()) select s;
            //var results = new ObservableCollection<string>(q);
            return GetItems(20);
        }
    }
}
