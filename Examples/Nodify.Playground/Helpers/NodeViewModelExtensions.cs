﻿using Nodify.Playground.Editor;
using System.Collections.Generic;
using System.Windows;

namespace Nodify.Playground.Helpers;

public static class NodeViewModelExtensions
{
    public static Rect GetBoundingBox(this IList<NodeViewModel> nodes, double padding = 0, int gridCellSize = 15)
    {
        double minX = double.MaxValue;
        double minY = double.MaxValue;

        double maxX = double.MinValue;
        double maxY = double.MinValue;

        for (int i = 0; i < nodes.Count; i++)
        {
            var node = nodes[i];
            int width = 200;    //node.Width
            int height = 200;   //node.Height

            if (node.Location.X < minX)
            {
                minX = node.Location.X;
            }

            if (node.Location.Y < minY)
            {
                minY = node.Location.Y;
            }

            double sizeX = node.Location.X + width;
            if (sizeX > maxX)
            {
                maxX = sizeX;
            }

            double sizeY = node.Location.Y + height;
            if (sizeY > maxY)
            {
                maxY = sizeY;
            }
        }

        var result = new Rect(minX - padding, minY - padding, maxX - minX + padding * 2, maxY - minY + padding * 2);
        result.X = (int)result.X / gridCellSize * gridCellSize;
        result.Y = (int)result.Y / gridCellSize * gridCellSize;
        return result;
    }

    public static void AddRange<T>(this ICollection<T> col, IEnumerable<T> items)
    {
        if (items is IList<T> itemsCol)
        {
            for (int i = 0; i < itemsCol.Count; i++)
            {
                col.Add(itemsCol[i]);
            }
        }
        else if (items is T[] itemsArr)
        {
            for (int i = 0; i < itemsArr.Length; i++)
            {
                col.Add(itemsArr[i]);
            }
        }
        else
        {
            foreach (var item in items)
            {
                col.Add(item);
            }
        }
    }
}
