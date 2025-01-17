﻿using Nodify.Shared;

namespace Nodify.Playground.Editor;

public class FlowNodeViewModel : NodeViewModel
{
    private string? _title;
    public string? Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public NodifyObservableCollection<ConnectorViewModel> Input { get; } = new NodifyObservableCollection<ConnectorViewModel>();
    public NodifyObservableCollection<ConnectorViewModel> Output { get; } = new NodifyObservableCollection<ConnectorViewModel>();

    public FlowNodeViewModel()
    {
        Input.WhenAdded(c => c.Node = this)
             .WhenRemoved(c => c.Disconnect());

        Output.WhenAdded(c => c.Node = this)
             .WhenRemoved(c => c.Disconnect());
    }

    public void Disconnect()
    {
        Input.Clear();
        Output.Clear();
    }
}
