using AvaloniaApplication1.Localization;
using System.Windows.Input;
namespace AvaloniaApplication1.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string HelloWorld { get; } = "hello world...123";
    public string Title { get; } = "AboutView_Title";
    public LocalizationResourceManager Localization { get; }

    public ICommand ClickCommand { get; set; }
    public MainViewModel()
    {
        Localization = LocalizationResourceManager.Instance;

        ClickCommand = new RelayCommand(() =>
        {
            if (Localization.CurrentCulture != Localization.AvailableCultures[0])
            {
                Localization.CurrentCulture = Localization.AvailableCultures[0];
            }
            else
            {
                Localization.CurrentCulture = Localization.AvailableCultures[1];
            }
        });
    }
}
