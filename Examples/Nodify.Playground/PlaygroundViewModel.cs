﻿using Nodify.Playground.Editor;
using Nodify.Playground.Helpers;
using Nodify.Shared;
using System;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Nodify.Playground
{
    public class PlaygroundViewModel : ObservableObject
    {
        public NodifyEditorViewModel GraphViewModel { get; } = new NodifyEditorViewModel();

        public PlaygroundViewModel()
        {
            GenerateRandomNodesCommand = new DelegateCommand(GenerateRandomNodes);
            PerformanceTestCommand = new DelegateCommand(PerformanceTest);
            ToggleConnectionsCommand = new DelegateCommand(ToggleConnections);
            ResetCommand = new DelegateCommand(ResetGraph);

            BindingOperations.EnableCollectionSynchronization(GraphViewModel.Nodes, GraphViewModel.Nodes);
            BindingOperations.EnableCollectionSynchronization(GraphViewModel.Connections, GraphViewModel.Connections);
        }

        public ICommand GenerateRandomNodesCommand { get; }
        public ICommand PerformanceTestCommand { get; }
        public ICommand ToggleConnectionsCommand { get; }
        public ICommand ResetCommand { get; }
        public static PlaygroundSettings Settings => PlaygroundSettings.Instance;

        private void ResetGraph()
        {
            GraphViewModel.Nodes.Clear();
            EditorSettings.Instance.Location = new System.Windows.Point(0, 0);
            EditorSettings.Instance.Zoom = 1.0d;
        }

        private async void GenerateRandomNodes()
        {
            var nodes = RandomNodesGenerator.GenerateNodes<FlowNodeViewModel>(new NodesGeneratorSettings(Settings.MinNodes)
            {
                MinNodesCount = Settings.MinNodes,
                MaxNodesCount = Settings.MaxNodes,
                MinInputCount = Settings.MinConnectors,
                MaxInputCount = Settings.MaxConnectors,
                MinOutputCount = Settings.MinConnectors,
                MaxOutputCount = Settings.MaxConnectors,
                GridSnap = EditorSettings.Instance.GridSpacing
            });

            GraphViewModel.Nodes.Clear();
            await CopyToAsync(nodes, GraphViewModel.Nodes);

            if (Settings.ShouldConnectNodes)
            {
                await ConnectNodes();
            }
        }

        private async void ToggleConnections()
        {
            if (Settings.ShouldConnectNodes)
            {
                await ConnectNodes();
            }
            else
            {
                GraphViewModel.Connections.Clear();
            }
        }

        private async void PerformanceTest()
        {
            uint count = Settings.PerformanceTestNodes;
            int distance = 500;
            int size = (int)count / (int)Math.Sqrt(count);

            var nodes = RandomNodesGenerator.GenerateNodes<FlowNodeViewModel>(new NodesGeneratorSettings(count)
            {
                NodeLocationGenerator = (s, i) => new System.Windows.Point(i % size * distance, i / size * distance),
                MinInputCount = Settings.MinConnectors,
                MaxInputCount = Settings.MaxConnectors,
                MinOutputCount = Settings.MinConnectors,
                MaxOutputCount = Settings.MaxConnectors,
                GridSnap = EditorSettings.Instance.GridSpacing
            });

            GraphViewModel.Nodes.Clear();
            await CopyToAsync(nodes, GraphViewModel.Nodes);

            if (Settings.ShouldConnectNodes)
            {
                await ConnectNodes();
            }
        }

        private async Task ConnectNodes()
        {
            var connections = RandomNodesGenerator.GenerateConnections(GraphViewModel.Nodes);

            if (Settings.AsyncLoading)
            {
                await Task.Run(() =>
                {
                    for (int i = 0; i < connections.Count; i++)
                    {
                        var con = connections[i];
                        GraphSchema.TryAddConnection(con.Input, con.Output);
                    }
                });
            }
            else
            {
                for (int i = 0; i < connections.Count; i++)
                {
                    var con = connections[i];
                    GraphSchema.TryAddConnection(con.Input, con.Output);
                }
            }
        }

        private static async Task CopyToAsync(IList source, IList target)
        {
            if (Settings.AsyncLoading)
            {
                await Task.Run(() =>
                {
                    for (int i = 0; i < source.Count; i++)
                    {
                        target.Add(source[i]);
                    }
                });
            }
            else
            {
                for (int i = 0; i < source.Count; i++)
                {
                    target.Add(source[i]);
                }
            }
        }
    }
}
