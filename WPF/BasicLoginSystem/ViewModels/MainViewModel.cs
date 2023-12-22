using System.ComponentModel;
using System.Runtime.CompilerServices;
using BasicLoginSystem.Services;

namespace BasicLoginSystem.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly UserSessionService userSessionService;
    private object currentViewModel;

    public MainViewModel(UserSessionService userSessionService)
    {
        // Domyślnie pokazujemy LoginViewModel
        this.userSessionService = userSessionService;
        CurrentViewModel = new LoginViewModel(ZalogujUzytkownika, userSessionService);
    }

    public string UserName => userSessionService.UserName;

    public object CurrentViewModel
    {
        get => currentViewModel;
        set
        {
            currentViewModel = value;
            OnPropertyChanged();
        }
    }

    // Implementacja INotifyPropertyChanged...
    public event PropertyChangedEventHandler? PropertyChanged;

    private void ZalogujUzytkownika()
    {
        // Po zalogowaniu, przełącz na MainViewModel
        CurrentViewModel = this;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}