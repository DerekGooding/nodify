using System.Collections.Generic;
using System.Windows;

namespace Nodify.Calculator;

public static class OperationsExtensions
{
    public static Rect GetBoundingBox(this IEnumerable<OperationViewModel> nodes, double padding = 0, int gridCellSize = 15)
    {
        double minX = double.MaxValue;
        double minY = double.MaxValue;

        double maxX = double.MinValue;
        double maxY = double.MinValue;

        const int width = 200; //node.Width
        const int height = 100; //node.Height

        foreach (var node in nodes)
        {
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
}