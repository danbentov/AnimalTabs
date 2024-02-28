using ShellLessonStep2.ViewModels;

namespace ShellLessonStep2.Views;

public partial class DogsView : ContentPage
{
	public DogsView(DogsViewModel dvm)
	{
		InitializeComponent();
		this.BindingContext = dvm;
	}
}