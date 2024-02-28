using ShellLessonStep2.ViewModels;

namespace ShellLessonStep2.Views;

public partial class CatsView : ContentPage
{
	public CatsView(CatsViewModel cvm)
	{
		InitializeComponent();
		this.BindingContext = cvm;
	}
}