using System.ComponentModel;
using System.Runtime.CompilerServices;
using BasicLoginSystem.Services;

namespace BasicLoginSystem.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private object currentViewModel;
    private readonly UserSessionService userSessionService;
    public string UserName => userSessionService.UserName;

    public object CurrentViewModel
    {
        get => currentViewModel;
        set
        {
            currentViewModel = value;
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }

    public MainViewModel(UserSessionService userSessionService)
    {
        // Domyślnie pokazujemy LoginViewModel
        this.userSessionService = userSessionService;
        CurrentViewModel = new LoginViewModel(ZalogujUzytkownika, userSessionService);
    }

    private void ZalogujUzytkownika()
    {
        // Po zalogowaniu, przełącz na MainViewModel
        CurrentViewModel = this;
    }

    // Implementacja INotifyPropertyChanged...
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
