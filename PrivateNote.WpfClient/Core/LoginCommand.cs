using System;
using System.Windows.Input;

namespace PrivateNote.WpfClient.Core;

public class LoginCommand :ICommand
{
    private readonly Action<object?> _execute;
    private readonly Func<object?,bool> _canExecute;

    public LoginCommand(Action<object?> execute, Func<object?, bool> canExecute)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter) =>_canExecute(parameter);

    public void Execute(object? parameter) => _execute(parameter);

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}