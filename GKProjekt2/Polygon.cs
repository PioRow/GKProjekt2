using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GKProjekt2
{
    public class ScaledPoint
    {
        public double X;
        public double Y;
        public ScaledPoint(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
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
    internal class Vector
    {
        public double X;
        public double Y;
        public double Z;
        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public void Normalize()
        {
            double norm = Math.Sqrt(X * X + Y * Y + Z * Z);
            X /= norm;
            Y /= norm;
            Z /= norm;
        }
        public double dotProduct(Vector v2)
        {
            return (X * v2.X) + Y * v2.Y + Z * v2.Z;
        }
        public Vector crossProduct(Vector v2) {
            return new Vector(Y * v2.Z - v2.Y * Z, v2.X * Z - X * v2.Z, X * v2.Y - Y * v2.X);
        }
        public static Vector operator + (Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }
        public static Vector operator*(double lambda, Vector v1)
        {
            return new Vector(lambda * v1.X, lambda * v1.Y, lambda * v1.Z);
        }
        public static Vector operator-(Vector v1,Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
    }
    internal class Polygon
    {
        public List<Edge> edges;
        public List<Point> points;
        public ScaledPoint[] ScaledPoints;
        public double[] Zs;
        public Polygon()
        {
            edges=new List<Edge>();
            points=new List<Point>();
            ScaledPoints = new ScaledPoint[3];
            NormalVecotrs = new Vector[3];
            Zs = new double[3];
        }
        public Vector[] NormalVecotrs;
        public void FillPolygonWIthLightedImage(DirectBitmap DrawingBitmap, Color[,] Source, Color LightColor, double kd, double ks, int m, double[] LS,
            int AW, int AH, Vector V, Color[,]NormalMap,bool addTexture)
        {
            double det1overR = ScaledPoints[0].X * (ScaledPoints[1].Y - ScaledPoints[2].Y) + ScaledPoints[1].X * (ScaledPoints[2].Y - ScaledPoints[0].Y) + ScaledPoints[2].X * (ScaledPoints[0].Y - ScaledPoints[1].Y);
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
                        double Xp = (double)curX / AW;
                        double Yp = (double)y / AH;
                        double[] lambdas = new double[3]
                        {
                            (ScaledPoints[1].X*ScaledPoints[2].Y-ScaledPoints[2].X*ScaledPoints[1].Y+Xp*(ScaledPoints[1].Y-ScaledPoints[2].Y)+Yp*(ScaledPoints[2].X-ScaledPoints[1].X))/det1overR,
                            (ScaledPoints[2].X*ScaledPoints[0].Y-ScaledPoints[0].X*ScaledPoints[2].Y+Xp*(ScaledPoints[2].Y-ScaledPoints[0].Y)+Yp*(ScaledPoints[0].X-ScaledPoints[2].X))/det1overR,
                            (ScaledPoints[0].X*ScaledPoints[1].Y-ScaledPoints[1].X*ScaledPoints[0].Y+Xp*(ScaledPoints[0].Y-ScaledPoints[1].Y)+Yp*(ScaledPoints[1].X-ScaledPoints[0].X))/det1overR,
                        };
                        double Zp = (lambdas[0] * Zs[0]) + (lambdas[1] * Zs[1]) + (lambdas[2] * Zs[2]);
                        Vector Np = ((lambdas[0] * NormalVecotrs[0]) + (lambdas[1] * NormalVecotrs[1]) + (lambdas[2] * NormalVecotrs[2]));
                        Vector L = new Vector(LS[0] - Xp, LS[1] - Yp, LS[2] - Zp);
                        Np.Normalize();
                        L.Normalize();
                        Vector Rv = ((2 * Np.dotProduct(L)) * Np) - L;
                        Rv.Normalize();
                        double cosmVR = (V.dotProduct(Rv));
                        cosmVR = cosmVR > 0 ? cosmVR : 0;
                        cosmVR = Math.Pow(cosmVR, m);
                        double cosNL = Np.dotProduct(L);
                        cosNL = cosNL > 0 ? cosNL : 0;
                        Color SC = Source[curX, y];

                        double R = kd * ((double)LightColor.R / 255) * ((double)SC.R / 255)* cosNL 
                            + ks * ((double)LightColor.G / 255) * ((double)SC.G / 255)*cosmVR;
                        double G = kd * ((double)LightColor.G / 255) * ((double)SC.G / 255)* cosNL 
                            +cosmVR* ks * ((double)LightColor.G / 255) * ((double)SC.G / 255); ;
                        double B = kd * ((double)LightColor.B / 255) * ((double)SC.B / 255)* cosNL 
                            +cosmVR* ks * ((double)LightColor.B / 255) * ((double)SC.B / 255);
                        R = R > 1 ? 1 : R;
                        G = G > 1 ? 1 : G;
                        B = B > 1 ? 1 : B;
                        DrawingBitmap.SetPixel(curX, y, Color.FromArgb((byte)(R * 255), (byte)(G * 255), (byte)(B * 255)));
                    }
                }
                for (int id = 0; id < AET.Count; id++)
                {
                    AET[id].x += (int)AET[id].oneOverM;
                }
                CurrScanLine.Clear();
            }
        }
    
            public void FillPolygonWIthLightedColor(DirectBitmap DrawingBitmap, Color ObjectColor,Color LightColor,double kd,double ks,int m, double[]LS,
            int AW,int AH,Vector V, Color[,] NormalMap, bool addTexture)
        {
            double kdR = kd * ((double)LightColor.R / 255) * ((double)ObjectColor.R / 255);
            double kdG = kd * ((double)LightColor.G / 255) * ((double)ObjectColor.G / 255);
            double kdB = kd * ((double)LightColor.B / 255) * ((double)ObjectColor.B / 255);
            double ksR= ks * ((double)LightColor.R / 255) * ((double)ObjectColor.R / 255);
            double ksG= ks * ((double)LightColor.G / 255) * ((double)ObjectColor.G / 255);
            double ksB = ks * ((double)LightColor.B / 255) * ((double)ObjectColor.B / 255);
            double det1overR = ScaledPoints[0].X * (ScaledPoints[1].Y - ScaledPoints[2].Y) + ScaledPoints[1].X * (ScaledPoints[2].Y - ScaledPoints[0].Y) + ScaledPoints[2].X * (ScaledPoints[0].Y - ScaledPoints[1].Y);
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
                        double Xp = (double)curX / AW;
                        double Yp = (double)y / AH;
                        double[] lambdas = new double[3]
                        {
                            (ScaledPoints[1].X*ScaledPoints[2].Y-ScaledPoints[2].X*ScaledPoints[1].Y+Xp*(ScaledPoints[1].Y-ScaledPoints[2].Y)+Yp*(ScaledPoints[2].X-ScaledPoints[1].X))/det1overR,
                            (ScaledPoints[2].X*ScaledPoints[0].Y-ScaledPoints[0].X*ScaledPoints[2].Y+Xp*(ScaledPoints[2].Y-ScaledPoints[0].Y)+Yp*(ScaledPoints[0].X-ScaledPoints[2].X))/det1overR,
                            (ScaledPoints[0].X*ScaledPoints[1].Y-ScaledPoints[1].X*ScaledPoints[0].Y+Xp*(ScaledPoints[0].Y-ScaledPoints[1].Y)+Yp*(ScaledPoints[1].X-ScaledPoints[0].X))/det1overR,
                        };
                        double Zp = (lambdas[0] * Zs[0]) + (lambdas[1] * Zs[1]) + (lambdas[2] * Zs[2]);
                        Vector Np = ((lambdas[0] * NormalVecotrs[0]) + (lambdas[1] * NormalVecotrs[1]) + (lambdas[2] * NormalVecotrs[2]));
                        Vector L=new Vector(LS[0] - Xp, LS[1]-Yp, LS[2] - Zp);
                        Np.Normalize();
                        if (addTexture)
                        {
                            Color Texture = NormalMap[curX, y];
                            Vector Bt = Np.crossProduct(new Vector(0, 0, 1));
                            Vector Tt = Bt.crossProduct(Np);

                        }
                        
                        L.Normalize();
                        Vector Rv = ((2 * Np.dotProduct(L)) * Np) - L;
                        Rv.Normalize();
                        double cosmVR = V.dotProduct(Rv);
                        cosmVR= cosmVR>0?cosmVR:0;
                        cosmVR = Math.Pow(cosmVR, m);
                        double cosNL = Np.dotProduct(L);
                        cosNL = cosNL > 0 ? cosNL : 0;

                        double R = kdR * cosNL+
                            ksR * cosmVR; ;
                        double G = kdG * cosNL + ksG * cosmVR;
                        double B = kdB* cosNL + ksB * cosmVR;
                        R = R > 1 ? 1 : R;
                        G=G > 1 ? 1 : G;  
                        B = B > 1 ? 1 : B;
                        DrawingBitmap.SetPixel(curX, y, Color.FromArgb((byte)(R*255), (byte)(G*255 ), (byte)(B*255 )));
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
