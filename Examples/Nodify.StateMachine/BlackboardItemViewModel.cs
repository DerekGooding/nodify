using Nodify.Shared;
using System;

namespace Nodify.StateMachine;

public class BlackboardItemViewModel : ObservableObject
{
    private string? _name;
    public string? Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    private Type? _type;
    public Type? Type
    {
        get => _type;
        set => SetProperty(ref _type, value);
    }

    private NodifyObservableCollection<BlackboardKeyViewModel> _input = new();
    public NodifyObservableCollection<BlackboardKeyViewModel> Input
    {
        get => _input;
        set
        {
            value ??= new NodifyObservableCollection<BlackboardKeyViewModel>();

            SetProperty(ref _input!, value);
        }
    }

    private NodifyObservableCollection<BlackboardKeyViewModel> _output = new();
    public NodifyObservableCollection<BlackboardKeyViewModel> Output
    {
        get => _output;
        set
        {
            value ??= new NodifyObservableCollection<BlackboardKeyViewModel>();

            SetProperty(ref _output!, value);
        }
    }
}