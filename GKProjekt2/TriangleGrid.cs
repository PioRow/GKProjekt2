using System.Media;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace GKProjekt2
{
    public partial class TriangleGrid : Form
    {
        private List<Polygon> Polygons;
        private DirectBitmap DrawingBitMap;
        private Color[,] Source;
        private Color[,] NormalMap;
        private double[,] ControlPoints;
        private string FilePath;
        private string MapPath;
        double[] LightSource;
        double kd = 0.5;
        double ks = 0.5;
        int m = 1;
        int ActualWidth;
        int ActualHeight;
        bool animRunning = false;
        Color ObjectColor;
        Color LightColor;
        Vector V;
        float alfa = 0;
        float beta = 0;
        System.Windows.Forms.Timer timer;
        double currAngle;
        Matrix4x4 M;
        public TriangleGrid()
        {
            ObjectColor = Color.FromArgb(255, 255, 255);
            InitializeComponent();
            DrawingBitMap = new DirectBitmap(DrawingBox.Width, DrawingBox.Height);
            LightColor = Color.FromArgb(255, 255, 255);
            Polygons = new List<Polygon>();
            ControlPoints = new double[4, 4];
            V = new Vector(0, 0, 1);
            LightSource = new double[3] { 0.5, 0.5, 1 };
            CreateGrid();
            Source = new Color[ActualWidth + 1, ActualHeight + 1];
            NormalMap = new Color[ActualWidth + 1, ActualHeight + 1];
            // FIllWithColor();
            PutTriangles();
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 33;
            timer.Tick += RunAnimation;

        }
        private void PutTriangles()
        {

            foreach (var pol in Polygons)
            {
                pol.DrawEdges(DrawingBitMap, Color.FromArgb(0, 0, 0), LightColor,
                    kd, ks, m, LightSource, ActualHeight, ActualHeight, V, ActualWidth, ActualHeight, M);
            }
            DrawingBox.Refresh();
        }
        private void FIllWithColor()
        {
            if (PlainColorFIll.Checked)
            {
                Parallel.ForEach(Polygons, Polygon =>
                {
                    Polygon.FillPolygonWIthLightedColor(DrawingBitMap, ObjectColor, LightColor,
                    kd, ks, m, LightSource, ActualHeight, ActualHeight, V, NormalMap, VecMapCheck.Checked, ActualWidth, ActualHeight, M);
                });
            }
            if (fillWPicture.Checked)
            {
                Parallel.ForEach(Polygons, Polygon =>
                {
                    Polygon.FillPolygonWIthLightedImage(DrawingBitMap, Source, LightColor,
                    kd, ks, m, LightSource, ActualHeight, ActualHeight, V, NormalMap, VecMapCheck.Checked, ActualWidth, ActualHeight, M);
                });
            }
        }
        private void CreateGrid()
        {
            int TrianglesPerSide = GridSizeBar.Value;
            int TriangleArmLength = DrawingBox.Width / TrianglesPerSide;
            ActualHeight = ActualWidth = TriangleArmLength * TrianglesPerSide;
            M = Matrix4x4.Identity * Matrix4x4.CreateTranslation(-ActualWidth / 2, -ActualHeight / 2, 0) *
                Matrix4x4.CreateFromYawPitchRoll(alfa, beta, 0)
                * Matrix4x4.CreateFromYawPitchRoll(0, alfa, beta) *
                Matrix4x4.CreateTranslation(ActualWidth / 2, ActualHeight / 2, 0);
            for (int j = 0; j < TrianglesPerSide; j++)
            {
                for (int i = 0; i < TrianglesPerSide; i++)
                {
                    CreateUpperTriangle(i, j, TriangleArmLength);
                    CreateLowerTriangle(i, j, TriangleArmLength);
                }
            }
        }
        private int factorial(int n)
        {
            int res = 1;
            for (int i = 1; i <= n; i++)
                res *= i;
            return res;
        }
        private double B(int i, int n, double t)
        {
            return factorial(n) / (factorial(i) * factorial(n - i)) * Math.Pow(t, i) * Math.Pow(1 - t, n - i);
        }
        private double Zxy(double x, double y)
        {
            double res = 0.0;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    res += ControlPoints[i, j] * B(i, 3, x) * B(j, 3, y);
            return res;
        }
        private double FZxy(double x, double y)
        {
            return Math.Sin(Math.PI * x / 2 + Math.PI * y / 2);
        }
        private double DF_du(double u, double v)
        {
            return Math.Cos(Math.PI * u / 2 + Math.PI * v / 2) * Math.PI / 2;
        }
        private double DF_dv(double u, double v)
        {
            return Math.Cos(Math.PI * u / 2 + Math.PI * v / 2) * Math.PI / 2;
        }
        private double Dz_du(double u, double v)
        {
            double res = 0.0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 4; j++)
                    res += (ControlPoints[i + 1, j] - ControlPoints[i, j]) * B(i, 2, u) * B(j, 3, v);
            return 3 * res;
        }
        private double Dz_dv(double u, double v)
        {
            double res = 0.0;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 3; j++)
                    res += (ControlPoints[i, j + 1] - ControlPoints[i, j]) * B(i, 3, u) * B(j, 2, v);
            return 3 * res;
        }
        private void CreateLowerTriangle(int i, int j, int armLength)
        {
            if (BezierSurface.Checked)
            {
                double x, y;
                Vector Pu, Pv, N;
                Polygon T1 = new Polygon();
                T1.points.Add(new Point(i * armLength + armLength, j * armLength + armLength));
                x = ((double)(i * armLength + armLength)) / ActualWidth;
                y = ((double)(j * armLength + armLength)) / ActualHeight;
                Pu = new Vector(1, 0, Dz_du(x, y));
                Pv = new Vector(0, 1, Dz_dv(x, y));
                N = Pu.crossProduct(Pv);
                N.Normalize();
                T1.NormalVecotrs[0] = N;
                T1.Zs[0] = Zxy(x, y);
                T1.ScaledPoints[0] = new ScaledPoint(x, y);
                T1.points.Add(new Point(i * armLength + armLength, j * armLength));
                x = ((double)(i * armLength + armLength)) / ActualWidth;
                y = ((double)(j * armLength)) / ActualHeight;
                Pu = new Vector(1, 0, Dz_du(x, y));
                Pv = new Vector(0, 1, Dz_dv(x, y));
                N = Pu.crossProduct(Pv);
                N.Normalize();
                T1.NormalVecotrs[1] = N;
                T1.Zs[1] = Zxy(x, y);
                T1.ScaledPoints[1] = new ScaledPoint(x, y);
                T1.points.Add(new Point(i * armLength, j * armLength + armLength));
                x = ((double)(i * armLength)) / ActualWidth;
                y = ((double)(j * armLength + armLength)) / ActualHeight;
                Pu = new Vector(1, 0, Dz_du(x, y));
                Pv = new Vector(0, 1, Dz_dv(x, y));
                N = Pu.crossProduct(Pv);
                N.Normalize();
                T1.NormalVecotrs[2] = N;
                T1.Zs[2] = Zxy(x, y);
                T1.ScaledPoints[2] = new ScaledPoint(x, y);
                for (int k = 0; k < 3; k++)
                {
                    T1.edges.Add(new Edge(T1.points[k], T1.points[(k + 1) % 3]));
                }
                Polygons.Add(T1);
            }
            if (functionalS.Checked)
            {
                double x, y;
                Vector Pu, Pv, N;
                Polygon T1 = new Polygon();
                T1.points.Add(new Point(i * armLength + armLength, j * armLength + armLength));
                x = ((double)(i * armLength + armLength)) / ActualWidth;
                y = ((double)(j * armLength + armLength)) / ActualHeight;
                Pu = new Vector(1, 0, DF_du(x, y));
                Pv = new Vector(0, 1, DF_dv(x, y));
                N = Pu.crossProduct(Pv);
                N.Normalize();
                T1.NormalVecotrs[0] = N;
                T1.Zs[0] = FZxy(x, y);
                T1.ScaledPoints[0] = new ScaledPoint(x, y);
                T1.points.Add(new Point(i * armLength + armLength, j * armLength));
                x = ((double)(i * armLength + armLength)) / ActualWidth;
                y = ((double)(j * armLength)) / ActualHeight;
                Pu = new Vector(1, 0, DF_du(x, y));
                Pv = new Vector(0, 1, DF_dv(x, y));
                N = Pu.crossProduct(Pv);
                N.Normalize();
                T1.NormalVecotrs[1] = N;
                T1.Zs[1] = FZxy(x, y);
                T1.ScaledPoints[1] = new ScaledPoint(x, y);
                T1.points.Add(new Point(i * armLength, j * armLength + armLength));
                x = ((double)(i * armLength)) / ActualWidth;
                y = ((double)(j * armLength + armLength)) / ActualHeight;
                Pu = new Vector(1, 0, DF_du(x, y));
                Pv = new Vector(0, 1, DF_dv(x, y));
                N = Pu.crossProduct(Pv);
                N.Normalize();
                T1.NormalVecotrs[2] = N;
                T1.Zs[2] = FZxy(x, y);
                T1.ScaledPoints[2] = new ScaledPoint(x, y);
                for (int k = 0; k < 3; k++)
                {
                    T1.edges.Add(new Edge(T1.points[k], T1.points[(k + 1) % 3]));
                }
                Polygons.Add(T1);
            }
        }
        private void CreateUpperTriangle(int i, int j, int armLength)
        {
            if (BezierSurface.Checked)
            {
                Vector Pu, Pv, N;
                double x, y;
                Polygon T1 = new Polygon();
                T1.points.Add(new Point(i * armLength, j * armLength));
                x = ((double)(i * armLength)) / ActualWidth;
                y = ((double)(j * armLength)) / ActualHeight;
                Pu = new Vector(1, 0, Dz_du(x, y));
                Pv = new Vector(0, 1, Dz_dv(x, y));
                N = Pu.crossProduct(Pv);
                N.Normalize();
                T1.NormalVecotrs[0] = N;
                T1.Zs[0] = Zxy(x, y);
                T1.ScaledPoints[0] = new ScaledPoint(x, y);
                T1.points.Add(new Point(i * armLength + armLength, j * armLength));
                x = ((double)(i * armLength + armLength)) / ActualWidth;
                y = ((double)(j * armLength)) / ActualHeight;
                Pu = new Vector(1, 0, Dz_du(x, y));
                Pv = new Vector(0, 1, Dz_dv(x, y));
                N = Pu.crossProduct(Pv);
                N.Normalize();
                T1.NormalVecotrs[1] = N;
                T1.Zs[1] = Zxy(x, y);
                T1.ScaledPoints[1] = new ScaledPoint(x, y);
                T1.points.Add(new Point(i * armLength, j * armLength + armLength));
                x = ((double)(i * armLength)) / ActualWidth;
                y = ((double)(j * armLength + armLength)) / ActualHeight;
                Pu = new Vector(1, 0, Dz_du(x, y));
                Pv = new Vector(0, 1, Dz_dv(x, y));
                N = Pu.crossProduct(Pv);
                N.Normalize();
                T1.NormalVecotrs[2] = N;
                T1.Zs[2] = Zxy(x, y);
                T1.ScaledPoints[2] = new ScaledPoint(x, y);
                for (int k = 0; k < 3; k++)
                {
                    T1.edges.Add(new Edge(T1.points[k], T1.points[(k + 1) % 3]));
                }
                Polygons.Add(T1);

            }
            if (functionalS.Checked)
            {
                Vector Pu, Pv, N;
                double x, y;
                Polygon T1 = new Polygon();
                T1.points.Add(new Point(i * armLength, j * armLength));
                x = ((double)(i * armLength)) / ActualWidth;
                y = ((double)(j * armLength)) / ActualHeight;
                Pu = new Vector(1, 0, DF_du(x, y));
                Pv = new Vector(0, 1, DF_dv(x, y));
                N = Pu.crossProduct(Pv);
                N.Normalize();
                T1.NormalVecotrs[0] = N;
                T1.Zs[0] = FZxy(x, y);
                T1.ScaledPoints[0] = new ScaledPoint(x, y);
                T1.points.Add(new Point(i * armLength + armLength, j * armLength));
                x = ((double)(i * armLength + armLength)) / ActualWidth;
                y = ((double)(j * armLength)) / ActualHeight;
                Pu = new Vector(1, 0, DF_du(x, y));
                Pv = new Vector(0, 1, DF_dv(x, y));
                N = Pu.crossProduct(Pv);
                N.Normalize();
                T1.NormalVecotrs[1] = N;
                T1.Zs[1] = FZxy(x, y);
                T1.ScaledPoints[1] = new ScaledPoint(x, y);
                T1.points.Add(new Point(i * armLength, j * armLength + armLength));
                x = ((double)(i * armLength)) / ActualWidth;
                y = ((double)(j * armLength + armLength)) / ActualHeight;
                Pu = new Vector(1, 0, DF_du(x, y));
                Pv = new Vector(0, 1, DF_dv(x, y));
                N = Pu.crossProduct(Pv);
                N.Normalize();
                T1.NormalVecotrs[2] = N;
                T1.Zs[2] = FZxy(x, y);
                T1.ScaledPoints[2] = new ScaledPoint(x, y);
                for (int k = 0; k < 3; k++)
                {
                    T1.edges.Add(new Edge(T1.points[k], T1.points[(k + 1) % 3]));
                }
                Polygons.Add(T1);
            }
        }

        private void BitmapFiller()
        {
            Graphics.FromImage(DrawingBitMap.Bitmap).Clear(Color.White);
            if (FillSurface.Checked)
                FIllWithColor();
            if (ShowGrid.Checked)
            {
                PutTriangles();
            }
            DrawingBox.Image = DrawingBitMap.Bitmap;
        }
        private void ColorPick_Click(object sender, EventArgs e)
        {
            if (colorPickDIalog.ShowDialog() == DialogResult.OK)
            {
                ObjectColor = colorPickDIalog.Color;
            }
            BitmapFiller();

        }



        private void RunAnimation(object sender, EventArgs e)
        {
            double alfa = (Math.PI * 2) / 24;
            LightSource[0] = 0.5 + 0.25 * Math.Cos(currAngle);
            LightSource[1] = 0.5 + 0.25 * Math.Sin(currAngle);
            currAngle += alfa;
            BitmapFiller();

        }
        private void AnimationButton_Click(object sender, EventArgs e)
        {
            animRunning = !animRunning;

            if (animRunning)
            {


                currAngle = 0.0;
                timer.Start();
            }
            else
            {
                timer.Stop();
                LightSource[0] = LightSource[1] = 0.5;
                BitmapFiller();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void BezierSurface_CheckedChanged(object sender, EventArgs e)
        {
            Polygons.Clear();
            CreateGrid();
            Graphics.FromImage(DrawingBitMap.Bitmap).Clear(Color.White);
            Source = new Color[ActualWidth + 1, ActualHeight + 1];
            if (fillWPicture.Checked)
            {
                Bitmap sc = new Bitmap(Image.FromFile(FilePath), new Size(ActualHeight, ActualWidth));
                for (int i = 0; i < sc.Width; i++)
                {
                    for (int j = 0; j < sc.Height; j++)
                        Source[i, j] = sc.GetPixel(i, j);
                }
            }
            BitmapFiller();
        }

        private void functionalS_CheckedChanged(object sender, EventArgs e)
        {
            Polygons.Clear();
            CreateGrid();
            Graphics.FromImage(DrawingBitMap.Bitmap).Clear(Color.White);
            Source = new Color[ActualWidth + 1, ActualHeight + 1];
            if (fillWPicture.Checked)
            {
                Bitmap sc = new Bitmap(Image.FromFile(FilePath), new Size(ActualHeight, ActualWidth));
                for (int i = 0; i < sc.Width; i++)
                {
                    for (int j = 0; j < sc.Height; j++)
                        Source[i, j] = sc.GetPixel(i, j);
                }
            }
            BitmapFiller();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AlphaBar_ValueChanged(object sender, EventArgs e)
        {
            alfa = (float)(alfaBar.Value * 10 * Math.PI / 360);
            debug.Text = alfa.ToString();
            Polygons.Clear();
            CreateGrid();
            Graphics.FromImage(DrawingBitMap.Bitmap).Clear(Color.White);
            Source = new Color[ActualWidth + 1, ActualHeight + 1];
            if (fillWPicture.Checked)
            {
                Bitmap sc = new Bitmap(Image.FromFile(FilePath), new Size(ActualHeight, ActualWidth));
                for (int i = 0; i < sc.Width; i++)
                {
                    for (int j = 0; j < sc.Height; j++)
                        Source[i, j] = sc.GetPixel(i, j);
                }
            }
            BitmapFiller();
        }

        private void bethaBar_ValueChanged(object sender, EventArgs e)
        {
            beta = (float)(bethaBar.Value * 10 * Math.PI / 360);
            Polygons.Clear();
            CreateGrid();
            Graphics.FromImage(DrawingBitMap.Bitmap).Clear(Color.White);
            Source = new Color[ActualWidth + 1, ActualHeight + 1];
            if (fillWPicture.Checked)
            {
                Bitmap sc = new Bitmap(Image.FromFile(FilePath), new Size(ActualHeight, ActualWidth));
                for (int i = 0; i < sc.Width; i++)
                {
                    for (int j = 0; j < sc.Height; j++)
                        Source[i, j] = sc.GetPixel(i, j);
                }
            }
            BitmapFiller();
        }
    }
}