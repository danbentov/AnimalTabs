
using ShellLessonStep2.Models;
using ShellLessonStep2.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShellLessonStep2.ViewModels
{
    public class MonkeysViewModels : ViewModelBase
    {
        private ObservableCollection<Animal> monkeys;
        public ObservableCollection<Animal> Monkeys
        {
            get
            {
                return this.monkeys;
            }
            set
            {
                this.monkeys = value;
                OnPropertyChanged();
            }
        }

        private AnimalService animalService;

        public MonkeysViewModels(AnimalService service)
        {
            this.animalService = service;
            Monkeys = new ObservableCollection<Animal>();
            Monkeys = (ObservableCollection<Animal>)this.animalService.GetMonkeys();
            IsRefreshing = false;
        }

        public ICommand DeleteCommand => new Command<Animal>(RemoveMonkey);

        void RemoveMonkey(Animal monkey)
        {
            if (Monkeys.Contains(monkey))
            {
                Monkeys.Remove(monkey);
            }
        }

        #region Refresh View
        public ICommand RefreshCommand => new Command(Refresh);
        private async void Refresh()
        {

            Monkeys = (ObservableCollection<Animal>)this.animalService.GetMonkeys();

            IsRefreshing = false;
        }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get
            {
                return this.isRefreshing;
            }
            set
            {
                this.isRefreshing = value;
                OnPropertyChanged();
            }
        }
        #endregion


        private object selectedMonkey;
        public object SelectedMonkey
        {
            get
            {
                return this.selectedMonkey;
            }
            set
            {
                this.selectedMonkey = value;
                OnPropertyChanged();
            }
        }

        public ICommand SingleSelectCommand => new Command(OnSingleSelectMonkey);

        async void OnSingleSelectMonkey()
        {
            if (SelectedMonkey != null)
            {
                var navParam = new Dictionary<string, object>()
            {
                { "selectedAnimal",SelectedMonkey}
            };
                //Add goto here to show details
                await Shell.Current.GoToAsync("animalDetails", navParam);

                SelectedMonkey = null;
            }
        }
    }
}
