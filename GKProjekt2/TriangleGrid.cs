namespace GKProjekt2
{
    public partial class TriangleGrid : Form
    {
        private List<Polygon> Polygons;
        private DirectBitmap DrawingBitMap;
        public TriangleGrid()
        {
            InitializeComponent();
            DrawingBitMap = new DirectBitmap(DrawingBox.Width, DrawingBox.Height);
            Polygons = new List<Polygon>();
            CreateGrid();
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
                foreach (var point in pol.points)
                    DrawingBitMap.SetPixel(point.X, point.Y, Color.Red);
            }
        }
        private void FIllWithColor()
        {
            Parallel.ForEach(Polygons, Polygon => { Polygon.FillPolygon(DrawingBitMap); });
        }
        private void CreateGrid()
        {
            int TrianglesPerSide = GridSizeBar.Value;
            int TriangleArmLength = DrawingBox.Width / TrianglesPerSide;
            for (int j = 0; j < TrianglesPerSide; j++)
            {
                for (int i = 0; i < TrianglesPerSide; i++)
                {
                    CreateUpperTriangle(i, j, TriangleArmLength);
                    CreateLowerTriangle(i, j, TriangleArmLength);

                }
            }
        }
        private void CreateLowerTriangle(int i, int j, int armLength)
        {
            Polygon T1 = new Polygon();
            T1.points.Add(new Point(i * armLength + armLength, j * armLength + armLength));
            T1.points.Add(new Point(i * armLength + armLength, j * armLength));
            T1.points.Add(new Point(i * armLength, j * armLength + armLength));
            for (int k = 0; k < 3; k++)
            {
                T1.edges.Add(new Edge(T1.points[k], T1.points[(k + 1) % 3]));
            }
            Polygons.Add(T1);
        }
        private void CreateUpperTriangle(int i, int j, int armLength)
        {
            Polygon T1 = new Polygon();
            T1.points.Add(new Point(i * armLength, j * armLength));
            T1.points.Add(new Point(i * armLength + armLength, j * armLength));
            T1.points.Add(new Point(i * armLength, j * armLength + armLength));

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
            if (ShowGrid.Checked)
            {
                DrawingBitMap.Dispose();
                DrawingBitMap = new DirectBitmap(DrawingBox.Width, DrawingBox.Height);
                PutTriangles();
                DrawingBox.Invalidate();
            }
        }
    }
}