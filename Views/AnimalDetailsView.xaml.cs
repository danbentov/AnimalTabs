using ShellLessonStep2.ViewModels;

namespace ShellLessonStep2.Views;

public partial class AnimalDetailsView : ContentPage
{
	public AnimalDetailsView(AnimalDetailsViewModel advm)
	{
		InitializeComponent();
		this.BindingContext = advm;
	}
}