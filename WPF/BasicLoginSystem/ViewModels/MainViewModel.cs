using System.ComponentModel;
using System.Runtime.CompilerServices;
using BasicLoginSystem.Services;

namespace BasicLoginSystem.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly UserSessionService _userSessionService;
    private object _currentViewModel;

    public MainViewModel(UserSessionService userSessionService)
    {
        // Domyślnie pokazujemy LoginViewModel
        this._userSessionService = userSessionService;
        CurrentViewModel = new LoginViewModel(ZalogujUzytkownika, userSessionService);
    }

    public string UserName => _userSessionService.UserName;

    public object CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
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