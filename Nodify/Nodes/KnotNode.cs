﻿using Nodify.Connections;
using System.Windows;
using System.Windows.Controls;

namespace Nodify.Nodes;

/// <summary>
/// Represents a control that owns a <see cref="Connector"/>.
/// </summary>
public class KnotNode : ContentControl
{
    static KnotNode() => DefaultStyleKeyProperty.OverrideMetadata(typeof(KnotNode), new FrameworkPropertyMetadata(typeof(KnotNode)));
}
