using System;
using System.Windows.Input;

namespace BasicLoginSystem.Commands;

public class RelayCommand : ICommand
{
    private Action execute;
    private Func<bool> canExecute;

    public RelayCommand(Action execute, Func<bool> canExecute = null)
    {
        this.execute = execute;
        this.canExecute = canExecute ?? (() => true);
    }

    public bool CanExecute(object parameter)
    {
        return canExecute();
    }

    public void Execute(object parameter)
    {
        execute();
    }

    public event EventHandler CanExecuteChanged;

    public void OnCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}