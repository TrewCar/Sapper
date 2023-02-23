using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

class OpenNeighbor
{
    public OpenNeighbor(Buttom[,] buttom)
    {
        this.buttom = buttom;
        CreateGraph();
    }

    private void CreateGraph()
    {
        for (int i = 0; i < buttom.GetLength(0); i++)
        {
            for (int j = 0; j < buttom.GetLength(1); j++)
            {
                graph[new Point(i, j)] = GetPoint(new Point(i, j));
            }
        }
    }
    private bool ProvOutRangeArray(Point point, Point NowPosition) => point.X + NowPosition.X < 0 || point.X + NowPosition.X >= buttom.GetLength(0) || point.Y + NowPosition.Y < 0 || point.Y + NowPosition.Y >= buttom.GetLength(1);
    private List<Point> GetPoint(Point NowPosition)
    {
        List<Point> points = new List<Point>();
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (ProvOutRangeArray(new Point(i, j), NowPosition))
                    continue;
                if (buttom[NowPosition.X + i, NowPosition.Y + j].NumberNeighbour == 0)
                    points.Add(new Point(NowPosition.X + i, NowPosition.Y + j));
            }
        }
        return points;
    }
    private Buttom[,] buttom;
    private Dictionary<Point, List<Point>> graph = new Dictionary<Point, List<Point>>();

    public List<Point> FindPath(Point StartPoint)
    {
        List<Point> visited = new List<Point>();

        Queue<Point> queue = new Queue<Point>();

        Point start = StartPoint;
        queue.Enqueue(start);
        visited.Add(start);
        Point curNode = start;

        while (queue.Count > 0)
        {
            curNode = queue.Dequeue();
            List<Point> Next = graph[curNode];

            foreach (var item in Next)
            {
                if (!visited.Contains(item))
                {
                    queue.Enqueue(item);
                    visited.Add(curNode);
                }
            }
        }
        return visited;
    }
}

