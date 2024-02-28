using ShellLessonStep2.ViewModels;

namespace ShellLessonStep2.Views;

public partial class MonkeysView : ContentPage
{
	public MonkeysView(MonkeysViewModels mvm)
	{
		InitializeComponent();
		this.BindingContext = mvm;
	}
}