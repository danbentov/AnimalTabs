
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
    public class CatsViewModel : ViewModelBase
    {
        private ObservableCollection<Animal> cats;
        public ObservableCollection<Animal> Cats
        {
            get
            {
                return this.cats;
            }
            set
            {
                this.cats = value;
                OnPropertyChanged();
            }
        }

        private AnimalService animalService;

        public CatsViewModel(AnimalService service)
        {
            this.animalService = service;
            Cats = new ObservableCollection<Animal>();
            Cats = (ObservableCollection<Animal>)this.animalService.GetCats();
            IsRefreshing = false;
        }
       

        public ICommand DeleteCommand => new Command<Animal>(RemoveCat);

        void RemoveCat(Animal cat)
        {
            if (Cats.Contains(cat))
            {
                Cats.Remove(cat);
            }
        }

        #region Refresh View
        public ICommand RefreshCommand => new Command(Refresh);
        private async void Refresh()
        {

            Cats = (ObservableCollection<Animal>)this.animalService.GetCats();

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


        private object selectedCat;
        public object SelectedCat
        {
            get
            {
                return this.selectedCat;
            }
            set
            {
                this.selectedCat = value;
                OnPropertyChanged();
            }
        }

        public ICommand SingleSelectCommand => new Command(OnSingleSelectCat);

        async void OnSingleSelectCat()
        {
            if (SelectedCat != null)
            {
                var navParam = new Dictionary<string, object>()
            {
                { "selectedAnimal",SelectedCat}
            };
                //Add goto here to show details
                await Shell.Current.GoToAsync("animalDetails", navParam);

                SelectedCat = null;
            }
        }
    }
}
