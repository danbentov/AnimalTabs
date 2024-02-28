using ShellLessonStep2.ViewModels;

namespace ShellLessonStep2.Views;

public partial class ElephantsView : ContentPage
{
	public ElephantsView(ElephantsViewModel evm)
	{
		InitializeComponent();
		this.BindingContext = evm;
	}
}