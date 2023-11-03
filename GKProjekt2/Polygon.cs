using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GKProjekt2
{
    
    public class ActiveEdgeNode
    {
        public int ymax;
        public int x;
        public double oneOverM;
        public bool isHorizontal;
        public Point Start;
        public Point End;
        public ActiveEdgeNode(int ymax, int x, double oneOverM, Point start, Point end)
        {
            this.ymax = ymax;
            this.x = x;
            this.oneOverM = oneOverM;
            Start = start;
            End = end;
        }
    }
    public class Edge
    {
        public Point Start;
        public Point End;
        public int ymax;
        public int ymin;
        public Edge(Point S,Point E)
        {
            Start = S;
            End = E;
            if(S.Y>E.Y)
            {
                ymax = S.Y;
                ymin = E.Y;
            }
            else
            {
                ymax = E.Y;
                ymin = S.Y;
            }
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
            NormalVecotrs = new Vector3[3];
        }
        public Vector3[] NormalVecotrs;
       
        public void FillPolygonWIthLightedColor(DirectBitmap DrawingBitmap, Color ObjectColor)
        {
            double det1overR = points[0].X * (points[1].Y - points[2].Y) + points[0].X * (points[1].Y - points[2].Y) + points[0].X * (points[1].Y - points[2].Y);
            int N = points.Count;//ilosc punktow
            List<int> pointsIdx = new List<int>();
            List<int> CurrScanLine = new List<int>();
            List<ActiveEdgeNode> AET = new List<ActiveEdgeNode>();
            for (int i = 0; i < points.Count; i++)
                pointsIdx.Add(i);
            pointsIdx.Sort((i, j) =>
            {
                return points[i].Y.CompareTo(points[j].Y);
            });
            int ymin = points[pointsIdx[0]].Y;
            int ymax = points[pointsIdx[points.Count - 1]].Y;
            int currIdx = 0;
            for (int y = ymin; y <= ymax; y++)
            {
                while (currIdx < N && y == points[pointsIdx[currIdx]].Y)
                {
                    CurrScanLine.Add(pointsIdx[currIdx]);
                    currIdx++;
                }
                foreach (int i in CurrScanLine)
                {
                    if (points[i].Y < points[(i + 1) % N].Y)
                    {
                        double overM = ((double)points[i].X - points[(i + 1) % N].X) / (double)((double)points[i].Y - points[(i + 1) % N].Y);
                        AET.Add(new ActiveEdgeNode(points[(i + 1) % N].Y, points[i].X, overM, points[i], points[(i + 1) % N]));
                    }
                    if (points[i].Y < points[(N + i - 1) % N].Y)
                    {
                        double overM = ((double)points[i].X - points[(N + i - 1) % N].X) / (double)((double)points[i].Y - points[(N + i - 1) % N].Y);
                        AET.Add(new ActiveEdgeNode(points[(N + i - 1) % N].Y, points[i].X, overM, points[i], points[(N + i - 1) % N]));
                    }
                    if (points[i].Y > points[(i + 1) % N].Y)
                    {
                        AET.Remove(AET.Find(No => No.End == points[i] && No.Start == points[(i + 1) % N]));
                    }
                    if (points[i].Y > points[(N + i - 1) % N].Y)
                    {
                        AET.Remove(AET.Find(No => No.End == points[i] && No.Start == points[(N + i - 1) % N]));
                    }
                }
                AET.Sort((e1, e2) => e1.x.CompareTo(e2.x));
                for (int j = 0; j < AET.Count - 1; j += 2)
                {
                    for (int curX = AET[j].x; curX <= AET[j + 1].x; curX++)
                    {
                        double[] lambdas = new double[3]
                        {
                            (points[1].X*points[2].Y-points[2].X*points[1].Y+curX*(points[1].Y-points[2].Y)+y*(points[2].X-points[1].X))/det1overR,
                            (points[2].X*points[0].Y-points[0].X*points[2].Y+curX*(points[2].Y-points[0].Y)+y*(points[0].X-points[2].X))/det1overR,
                            (points[0].X*points[1].Y-points[1].X*points[0].Y+curX*(points[0].Y-points[1].Y)+y*(points[1].X-points[0].X))/det1overR,
                        };
                        DrawingBitmap.SetPixel(curX, y, Color.FromArgb(ObjectColor.A, ObjectColor.R,ObjectColor.G,ObjectColor.B));
                    }
                }
                for (int id = 0; id < AET.Count; id++)
                {
                    AET[id].x += (int)AET[id].oneOverM;
                }
                CurrScanLine.Clear();
            }
        }
    }
}
