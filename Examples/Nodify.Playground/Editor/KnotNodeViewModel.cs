﻿namespace Nodify.Playground.Editor;

public class KnotNodeViewModel : NodeViewModel
{
    private ConnectorViewModel _connector = default!;
    public ConnectorViewModel Connector
    {
        get => _connector;
        set
        {
            if (SetProperty(ref _connector, value))
            {
                _connector.Node = this;
            }
        }
    }

    public ConnectorFlow Flow { get; set; }
}
