using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKProjekt2
{
    public class ActiveEdgeNode
    {
        public int ymax;
        public int x;
        public double oneOverM;
        public ActiveEdgeNode(int ymax, int x, double oneOverM )
        {
            this.ymax = ymax;
            this.x = x;
            this.oneOverM = oneOverM;
        }
    }
    public class Edge
    {
        public Point Start;
        public Point End;
        public Edge(Point S,Point E)
        {
            Start = S;
            End = E;
        }
        public Edge(int x1,int y1,int x2,int y2)
        {
            Start = new Point(x1,y1);
            End = new Point(x2,y2);
        }
    }
    internal class Polygon
    {
        public List<Edge> edges;
        public List<Point> points;
        public Polygon()
        {
            edges=new List<Edge>();
            points=new List<Point>();
        }
        public void FillPolygon(DirectBitmap DrawingBitmap)
        {
            List<int> pointsIdx = new List<int>();
            for(int i=0; i<points.Count; i++)
            {
                pointsIdx.Add(i);
            }
            pointsIdx.Sort((i, j) =>
            {
                return points[i].Y.CompareTo(points[j].Y);
            });
            pointsIdx.Add(-1);
        }
    }
}
