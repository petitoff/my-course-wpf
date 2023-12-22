using System;
using System.ComponentModel;
using System.Windows.Input;
using BasicLoginSystem.Commands;
using BasicLoginSystem.Services;

namespace BasicLoginSystem.ViewModels;

public class LoginViewModel
{
    private string imie;
    private readonly Action onLoginSuccess;
    private readonly UserSessionService userSessionService;

    public string Imie
    {
        get => imie;
        set
        {
            imie = value;
            OnPropertyChanged(nameof(Imie));
        }
    }

    public ICommand LoginCommand { get; }

    public LoginViewModel(Action onLoginSuccess, UserSessionService userSessionService)
    {
        LoginCommand = new RelayCommand(Zaloguj);
        this.onLoginSuccess = onLoginSuccess;
        this.userSessionService = userSessionService;
    }

    private void Zaloguj()
    {
        // Logika logowania
        userSessionService.UserName = Imie;
        // Przejście do MainView
        onLoginSuccess?.Invoke();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}