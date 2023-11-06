using System.Numerics;

namespace GKProjekt2
{
    public partial class TriangleGrid : Form
    {
        private List<Polygon> Polygons;
        private DirectBitmap DrawingBitMap;
        private Color[,] Source;
        private double[,] ControlPoints;
        private string FilePath;
        double[] LightSource;
        double kd = 0.5;
        double ks = 0.5;
        int m = 1;
        int ActualWidth;
        int ActualHeight;
        Color ObjectColor;
        Color LightColor;
        Vector V;
        public TriangleGrid()
        {
            ObjectColor = Color.FromArgb(255, 255, 255);
            InitializeComponent();
            DrawingBitMap = new DirectBitmap(DrawingBox.Width, DrawingBox.Height);
            LightColor = Color.FromArgb(255, 255, 255);
            Polygons = new List<Polygon>();
            ControlPoints = new double[4, 4];
           
            V = new Vector(0, 0, 1);
            LightSource = new double[3] { 0.5, 0.5, 0.5 };
            CreateGrid();
            Source = new Color[ActualWidth + 1, ActualHeight + 1];
            FIllWithColor();
            PutTriangles();

        }
        private void PutTriangles()
        {
            Graphics g = Graphics.FromImage(DrawingBitMap.Bitmap);
            foreach (var pol in Polygons)
            {
                using (Pen p = new Pen(Color.Black, 1))
                {
                    foreach (var e in pol.edges)
                        g.DrawLine(p, e.Start, e.End);
                }
            }
        }
        private void FIllWithColor()
        {
            if (PlainColorFIll.Checked)
            {
                Parallel.ForEach(Polygons, Polygon =>
                {
                    Polygon.FillPolygonWIthLightedColor(DrawingBitMap, ObjectColor, LightColor,
                    kd, ks, m, LightSource, ActualHeight, ActualHeight, V);
                });
            }
            if (fillWPicture.Checked)
            {
                Parallel.ForEach(Polygons, Polygon =>
                {
                    Polygon.FillPolygonWIthLightedImage(DrawingBitMap, Source, LightColor,
                    kd, ks, m, LightSource, ActualHeight, ActualHeight, V);
                });
            }
        }
        private void CreateGrid()
        {
            int TrianglesPerSide = GridSizeBar.Value;
            int TriangleArmLength = DrawingBox.Width / TrianglesPerSide;
            ActualHeight = ActualWidth = TriangleArmLength * TrianglesPerSide;
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
        private double Dz_du(double u, double v)
        {
            double res = 0.0;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    res += ControlPoints[i, j] * (B(i, 3, u - 1) - B(i, 3, u)) * B(j, 3, v);
            return res;
        }
        private double Dz_dv(double u, double v)
        {
            double res = 0.0;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    res += ControlPoints[i, j] * B(i, 3, u) * (B(j, 3, v - 1) - B(j, 3, v));
            return res;
        }
        private void CreateLowerTriangle(int i, int j, int armLength)
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
        private void CreateUpperTriangle(int i, int j, int armLength)
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
        private void DrawingBox_Paint(object sender, PaintEventArgs e)
        {

            e.Graphics.DrawImage(DrawingBitMap.Bitmap, 0, 0);

        }

        private void GridSizeBar_ValueChanged(object sender, EventArgs e)
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
        private void BitmapFiller()
        {
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

        private void ShowGrid_CheckedChanged(object sender, EventArgs e)
        {
            BitmapFiller();
        }

        private void lightColorPick_Click(object sender, EventArgs e)
        {
            if (lightColorPickDIal.ShowDialog() == DialogResult.OK)
            {
                LightColor = lightColorPickDIal.Color;
            }
            BitmapFiller();
        }

        private void kdBar_ValueChanged(object sender, EventArgs e)
        {
            kd = (double)kdBar.Value / 100;
            BitmapFiller();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            ks = (double)ksBar.Value / 100;
            BitmapFiller();
        }

        private void mBar_ValueChanged(object sender, EventArgs e)
        {
            m = mBar.Value;
            BitmapFiller();
        }

        private void lightHeight_ValueChanged(object sender, EventArgs e)
        {
            LightSource[2] = (double)lightHeight.Value / 10;
            BitmapFiller();
        }

        private void PlainColorFIll_CheckedChanged(object sender, EventArgs e)
        {
            BitmapFiller();
        }

        private void SelectPicture_Click(object sender, EventArgs e)
        {
            selectPictureDialog.InitialDirectory = System.Environment.CurrentDirectory + "\\Pictures";
            selectPictureDialog.Filter = "JPEG Images|*.jpg";
            if (selectPictureDialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = selectPictureDialog.FileName;
                Bitmap sc = new Bitmap(Image.FromFile(FilePath), new Size(ActualHeight, ActualWidth));
                for (int i = 0; i < sc.Width; i++)
                {
                    for (int j = 0; j < sc.Height; j++)
                        Source[i, j] = sc.GetPixel(i, j);
                }
            }
            BitmapFiller();
        }

        private void ConfirmCPButton_Click(object sender, EventArgs e)
        {
            try
            {
                int x, y;
                x = CPXCB.SelectedIndex;
                y = CPYCB.SelectedIndex;
                double val = double.Parse(CPVal.Text);
                ControlPoints[x,y]= val;
                Polygons.Clear();
                CreateGrid();
                BitmapFiller();
             }
            catch(Exception exc) {
                debug.Text=exc.Message;
            }
            
        }
    }
}