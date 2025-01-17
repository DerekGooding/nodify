﻿using System;
using System.Windows.Input;

namespace Nodify.Shared;

public interface INodifyCommand : ICommand
{
    void RaiseCanExecuteChanged();
}

public class DelegateCommand : INodifyCommand
{
    private readonly Action _action;
    private readonly Func<bool>? _condition;

    public event EventHandler? CanExecuteChanged;

    public DelegateCommand(Action action, Func<bool>? executeCondition = default)
    {
        _action = action ?? throw new ArgumentNullException(nameof(action));
        _condition = executeCondition;
    }

    public bool CanExecute(object? parameter)
        => _condition?.Invoke() ?? true;

    public void Execute(object? parameter)
        => _action();

    public void RaiseCanExecuteChanged()
        => CanExecuteChanged?.Invoke(this, new EventArgs());
}

public class DelegateCommand<T> : INodifyCommand
{
    private readonly Action<T> _action;
    private readonly Func<T, bool>? _condition;

    public event EventHandler? CanExecuteChanged;

    public DelegateCommand(Action<T> action, Func<T, bool>? executeCondition = default)
    {
        _action = action ?? throw new ArgumentNullException(nameof(action));
        _condition = executeCondition;
    }

    public bool CanExecute(object? parameter) 
        => parameter is T value ? 
        _condition?.Invoke(value) ?? true : 
        _condition?.Invoke(default!) ?? true;

    public void Execute(object? parameter)
    {
        switch (parameter)
        {
            case T value:
                _action(value);
                break;
            default:
                _action(default!);
                break;
        }
    }

    public void RaiseCanExecuteChanged()
        => CanExecuteChanged?.Invoke(this, new EventArgs());
}
