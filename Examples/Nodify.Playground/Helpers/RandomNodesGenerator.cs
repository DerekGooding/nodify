using Nodify.Playground.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Nodify.Playground.Helpers;

public struct NodesGeneratorSettings
{
    private static readonly Random _rand = new();

    public NodesGeneratorSettings(uint count)
    {
        GridSnap = 15;
        MinNodesCount = MaxNodesCount = count;
        MinInputCount = MinOutputCount = 0;
        MaxInputCount = MaxOutputCount = 7;

        ConnectorNameGenerator = (s, i) => $"{new string('C', (int)i % 5)} {i}";
        NodeNameGenerator = (s, i) => $"Node {i}";
        NodeLocationGenerator = (s, i) =>
        {
            static double EaseOut(double percent, double increment, double start, double end, double total)
                => -end * (increment /= total) * (increment - 2) + start;

            int xDistanceBetweenNodes = _rand.Next(150, 350);
            int yDistanceBetweenNodes = _rand.Next(200, 350);
            int randSignX = _rand.Next(0, 100) > 50 ? 1 : -1;
            int randSignY = _rand.Next(0, 100) > 50 ? 1 : -1;
            double gridOffsetX = i * xDistanceBetweenNodes;
            double gridOffsetY = i * yDistanceBetweenNodes;

            double x = gridOffsetX * Math.Sin(xDistanceBetweenNodes * randSignX / (i + 1));
            double y = gridOffsetY * Math.Sin(yDistanceBetweenNodes * randSignY / (i + 1));
            double easeX = x * EaseOut(i / count, i, 1, 0.01, count);
            double easeY = y * EaseOut(i / count, i, 1, 0.01, count);

            x = s.Snap((int)easeX);
            y = s.Snap((int)easeY);

            return new Point(x, y);
        };
    }

    public uint GridSnap;
    public uint MinNodesCount;
    public uint MaxNodesCount;
    public uint MinInputCount;
    public uint MaxInputCount;
    public uint MinOutputCount;
    public uint MaxOutputCount;

    public Func<NodesGeneratorSettings, uint, string?> ConnectorNameGenerator;
    public Func<NodesGeneratorSettings, uint, string?> NodeNameGenerator;
    public Func<NodesGeneratorSettings, uint, Point> NodeLocationGenerator;

    public readonly int Snap(int x)
        => x / (int)GridSnap * (int)GridSnap;
}

public static class RandomNodesGenerator
{
    private static readonly Random _rand = new();

    public static List<T> GenerateNodes<T>(NodesGeneratorSettings settings)
        where T : FlowNodeViewModel, new()
    {
        var nodes = new List<T>();
        int count = _rand.Next((int)settings.MinNodesCount, (int)settings.MaxNodesCount + 1);

        for (uint i = 0; i < count; i++)
        {
            var node = new T
            {
                Title = settings.NodeNameGenerator(settings, i),
                Location = settings.NodeLocationGenerator(settings, i)
            };

            nodes.Add(node);
            node.Input.AddRange(GenerateConnectors(settings, _rand.Next((int)settings.MinInputCount, (int)settings.MaxInputCount + 1)));
            node.Output.AddRange(GenerateConnectors(settings, _rand.Next((int)settings.MinOutputCount, (int)settings.MaxOutputCount + 1)));
        }

        return nodes;
    }

    public static List<ConnectionViewModel> GenerateConnections(IList<NodeViewModel> nodes)
    {
        HashSet<NodeViewModel> visited = new(nodes.Count);
        List<ConnectionViewModel> connections = new(nodes.Count);

        for (uint i = 0; i < nodes.Count; i++)
        {
            var n1 = nodes[_rand.Next(0, nodes.Count)];
            var n2 = nodes[_rand.Next(0, nodes.Count)];

            if (n1 == n2 && !(visited.Add(n1) && visited.Add(n2)))
            {
                continue;
            }

            List<ConnectorViewModel> input = n1 is FlowNodeViewModel flow ? flow.Input.ToList() :
                                             n1 is KnotNodeViewModel knot ? new List<ConnectorViewModel> { knot.Connector } : new List<ConnectorViewModel>();

            List<ConnectorViewModel> output = n2 is FlowNodeViewModel flow2 ? flow2.Output.ToList() :
                                              n2 is KnotNodeViewModel knot2 ? new List<ConnectorViewModel> { knot2.Connector } : new List<ConnectorViewModel>();

            connections.AddRange(ConnectPins(input, output));
        }

        return connections;
    }

    public static List<ConnectionViewModel> ConnectPins(IList<ConnectorViewModel> source, IList<ConnectorViewModel> target)
    {
        Dictionary<ConnectorViewModel, List<ConnectorViewModel>> connections = new();
        List<ConnectionViewModel> result = new();

        for (int di = 0; di < target.Count; di++)
        {
            if (source.Count > 1 && target.Count > 1 && _rand.Next() % 2 == 0)
            {
                continue;
            }

            var outP = target[di];

            if (!connections.TryGetValue(outP, out var outConns))
            {
                var newList = new List<ConnectorViewModel>();
                connections.Add(outP, newList);
                outConns = newList;
            }

            int conNum = _rand.Next(0, source.Count + 1);
            for (uint ci = 0; ci < conNum; ci++)
            {
                var inP = source[_rand.Next(0, conNum)];

                if (!connections.TryGetValue(inP, out var inConns))
                {
                    var newList = new List<ConnectorViewModel>();
                    connections.Add(inP, newList);
                    inConns = newList;
                }

                if (!connections[inP].Contains(outP) && !connections[outP].Contains(inP))
                {
                    bool isInput = inP.Flow == ConnectorFlow.Input;

                    var connection = new ConnectionViewModel
                    {
                        Input = isInput ? inP : outP,
                        Output = isInput ? outP : inP
                    };
                    result.Add(connection);

                    inConns.Add(outP);
                    outConns.Add(inP);
                }
            }
        }

        return result;
    }

    public static List<ConnectorViewModel> GenerateConnectors(NodesGeneratorSettings settings, int count)
    {
        var list = new List<ConnectorViewModel>(count);

        for (uint i = 0; i < count; i++)
        {
            int shapeVal = _rand.Next() % 3;
            var connector = new ConnectorViewModel
            {
                Title = settings.ConnectorNameGenerator(settings, i),
                Shape = PlaygroundSettings.Instance.UseCustomConnectors ? (ConnectorShape)shapeVal : ConnectorShape.Circle
            };

            list.Add(connector);
        }

        return list;
    }
}
