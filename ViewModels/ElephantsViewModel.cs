
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
    public class ElephantsViewModel : ViewModelBase
    {
        private ObservableCollection<Animal> elephants;
        public ObservableCollection<Animal> Elephants
        {
            get
            {
                return this.elephants;
            }
            set
            {
                this.elephants = value;
                OnPropertyChanged();
            }
        }

        private AnimalService animalService;

        public ElephantsViewModel(AnimalService service)
        {
            this.animalService = service;
            Elephants = new ObservableCollection<Animal>();
            Elephants = (ObservableCollection<Animal>)this.animalService.GetElephants();
            IsRefreshing = false;
        }

        public ICommand DeleteCommand => new Command<Animal>(RemoveElephants);

        void RemoveElephants(Animal elephant)
        {
            if (Elephants.Contains(elephant))
            {
                Elephants.Remove(elephant);
            }
        }

        #region Refresh View
        public ICommand RefreshCommand => new Command(Refresh);
        private async void Refresh()
        {

            Elephants = (ObservableCollection<Animal>)this.animalService.GetElephants();

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


        private object selectedElephant;
        public object SelectedElephant
        {
            get
            {
                return this.selectedElephant;
            }
            set
            {
                this.selectedElephant = value;
                OnPropertyChanged();
            }
        }

        public ICommand SingleSelectCommand => new Command(OnSingleSelectElephant);

        async void OnSingleSelectElephant()
        {
            if (SelectedElephant != null)
            {
                var navParam = new Dictionary<string, object>()
            {
                { "selectedAnimal",SelectedElephant}
            };
                //Add goto here to show details
                await Shell.Current.GoToAsync("animalDetails", navParam);

                SelectedElephant = null;
            }
        }
    }
}
