using ShellLessonStep2.ViewModels;

namespace ShellLessonStep2.Views;

public partial class BearsView : ContentPage
{
	public BearsView(BearsViewModel bvm)
	{
		InitializeComponent();
		this.BindingContext = bvm;
	}
}