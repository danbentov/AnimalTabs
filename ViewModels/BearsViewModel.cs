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
    public class BearsViewModel : ViewModelBase
    {
        private ObservableCollection<Animal> bears;
        public ObservableCollection<Animal> Bears
        {
            get
            {
                return this.bears;
            }
            set
            {
                this.bears = value;
                OnPropertyChanged();
            }
        }

        private AnimalService animalService;

        public BearsViewModel(AnimalService service)
        {
            this.animalService = service;
            Bears = new ObservableCollection<Animal>();
            Bears = (ObservableCollection<Animal>)this.animalService.GetBears();
            IsRefreshing = false;
        }

        public ICommand DeleteCommand => new Command<Animal>(RemoveBear);

        void RemoveBear(Animal bear)
        {
            if (Bears.Contains(bear))
            {
                Bears.Remove(bear);
            }
        }

        #region Refresh View
        public ICommand RefreshCommand => new Command(Refresh);
        private async void Refresh()
        {

            Bears = (ObservableCollection<Animal>)this.animalService.GetBears();

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


        private object selectedBear;
        public object SelectedBear
        {
            get
            {
                return this.selectedBear;
            }
            set
            {
                this.selectedBear = value;
                OnPropertyChanged();
            }
        }

        public ICommand SingleSelectCommand => new Command(OnSingleSelectBear);

        async void OnSingleSelectBear()
        {
            if (SelectedBear != null)
            {
                var navParam = new Dictionary<string, object>()
            {
                { "selectedAnimal",SelectedBear}
            };
                //Add goto here to show details
                await Shell.Current.GoToAsync("animalDetails", navParam);

                SelectedBear = null;
            }
        }
    }
}
