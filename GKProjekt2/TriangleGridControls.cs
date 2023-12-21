using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace GKProjekt2
{
    public partial class TriangleGrid : Form
    {
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
            LightSource[2] = (double)lightHeight.Value / 5;
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
        bool innerChange=false;
        private void CPXCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                innerChange = true;
                int x, y;
                x = CPXCB.SelectedIndex;
                y = CPYCB.SelectedIndex;
                CPHeight.Value = (int)(ControlPoints[x, y] * 10);
                innerChange = false;
            }
            catch { innerChange = false; }
        }

        private void CPYCB_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                innerChange = true;
                int x, y;
                x = CPXCB.SelectedIndex;
                y = CPYCB.SelectedIndex;
                CPHeight.Value = (int)(ControlPoints[x, y] * 10);
                innerChange = false;
            }
            catch { innerChange = false; }
        }

        private void CPHeight_ValueChanged(object sender, EventArgs e)
        {
            if (!innerChange)
            {
                try
                {
                    int x, y;
                    x = CPXCB.SelectedIndex;
                    y = CPYCB.SelectedIndex;
                    double val = ((double)CPHeight.Value) / 10;
                    ControlPoints[x, y] = val;
                    Polygons.Clear();
                    CreateGrid();
                    BitmapFiller();
                }
                catch (Exception exc)
                {
                    debug.Text = exc.Message;
                }
            }
        }

        private void LoadVecMapBtn_Click(object sender, EventArgs e)
        {
            NormalMapDialog.InitialDirectory = System.Environment.CurrentDirectory + "\\NormalMaps";
            NormalMapDialog.Filter = "JPEG Images|*.jpg";

            if (NormalMapDialog.ShowDialog() == DialogResult.OK)
            {
                MapPath = NormalMapDialog.FileName;

                Bitmap sc = new Bitmap(Image.FromFile(MapPath), new Size(ActualHeight, ActualWidth));
                for (int i = 0; i < sc.Width; i++)
                {
                    for (int j = 0; j < sc.Height; j++)
                        NormalMap[i, j] = sc.GetPixel(i, j);
                }
            }
            BitmapFiller();
        }

        private void VecMapCheck_CheckedChanged(object sender, EventArgs e)
        {
            BitmapFiller();
        }
    }
}

