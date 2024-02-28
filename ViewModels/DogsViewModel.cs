
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
    public class DogsViewModel : ViewModelBase
    {
        private ObservableCollection<Animal> dogs;
        public ObservableCollection<Animal> Dogs
        {
            get
            {
                return this.dogs;
            }
            set
            {
                this.dogs = value;
                OnPropertyChanged();
            }
        }

        private AnimalService animalService;

        public DogsViewModel(AnimalService service)
        {
            this.animalService = service;
            Dogs = new ObservableCollection<Animal>();
            IsRefreshing = false;
            ReadDogs();
        }
        private async void ReadDogs()
        {
            AnimalService service = new AnimalService();
            List<Animal> list = await service.GetDogs();
            this.Dogs = new ObservableCollection<Animal>(list);
        }

        public ICommand DeleteCommand => new Command<Animal>(RemoveDog);

        void RemoveDog(Animal dog)
        {
            if (Dogs.Contains(dog))
            {
                Dogs.Remove(dog);
            }
        }

        #region Refresh View
        public ICommand RefreshCommand => new Command(Refresh);
        private async void Refresh()
        {

            ReadDogs();

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


        private object selectedDog;
        public object SelectedDog
        {
            get
            {
                return this.selectedDog;
            }
            set
            {
                this.selectedDog = value;
                OnPropertyChanged();
            }
        }

        public ICommand SingleSelectCommand => new Command(OnSingleSelectDog);

        async void OnSingleSelectDog()
        {
            if (SelectedDog != null)
            {
                var navParam = new Dictionary<string, object>()
            {
                { "selectedAnimal",SelectedDog}
            };
                //Add goto here to show details
                await Shell.Current.GoToAsync("animalDetails", navParam);

                SelectedDog = null;
            }
        }
    }
}
