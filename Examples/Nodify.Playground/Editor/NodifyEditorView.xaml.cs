﻿using System.Windows.Controls;

namespace Nodify.Playground;

public partial class NodifyEditorView : UserControl
{
    public NodifyEditor EditorInstance => Editor;

    public NodifyEditorView() => InitializeComponent();
}
