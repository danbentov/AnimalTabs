using ShellLessonStep2.Views;
using System.Windows.Input;

namespace ShellLessonStep2;

public partial class AppShell : Shell
{
	public AppShell()
	{
        this.BindingContext = this;
        InfoCommand = new Command(OnInfoClicked);
        InitializeComponent();
		RegisterRoutings();
	}

    private void RegisterRoutings()
	{
		Routing.RegisterRoute("modalPage", typeof(ModalPage));
        Routing.RegisterRoute("animalDetails", typeof(AnimalDetailsView));
    }

    public ICommand InfoCommand { get; set; }
	private void OnInfoClicked()
	{
		DisplayAlert("Info", "This is a demo for using Shell!", "Ok");
	}
}
