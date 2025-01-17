﻿using Nodify.Connections;
using System.Windows;
using System.Windows.Controls;

namespace Nodify.Nodes;

/// <summary>
/// Represents the default control for the <see cref="Node.OutputConnectorTemplate"/>.
/// </summary>
public class NodeOutput : Connector
{
    #region Dependency Properties

    public static readonly DependencyProperty HeaderProperty = HeaderedContentControl.HeaderProperty.AddOwner(typeof(NodeOutput));
    public static readonly DependencyProperty HeaderTemplateProperty = HeaderedContentControl.HeaderTemplateProperty.AddOwner(typeof(NodeOutput));
    public static readonly DependencyProperty ConnectorTemplateProperty = NodeInput.ConnectorTemplateProperty.AddOwner(typeof(NodeOutput));

    /// <summary>
    /// Gets of sets the data used for the control's header.
    /// </summary>
    public object Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    /// <summary>
    /// Gets or sets the template used to display the content of the control's header.
    /// </summary>
    public DataTemplate HeaderTemplate
    {
        get => (DataTemplate)GetValue(HeaderTemplateProperty);
        set => SetValue(HeaderTemplateProperty, value);
    }

    /// <summary>
    /// Gets or sets the template used to display the connecting point of this <see cref="Connector"/>.
    /// </summary>
    public ControlTemplate ConnectorTemplate
    {
        get => (ControlTemplate)GetValue(ConnectorTemplateProperty);
        set => SetValue(ConnectorTemplateProperty, value);
    }

    #endregion

    static NodeOutput() => DefaultStyleKeyProperty.OverrideMetadata(typeof(NodeOutput), new FrameworkPropertyMetadata(typeof(NodeOutput)));
}
