namespace GKProjekt2
{
    partial class TriangleGrid
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            DrawingBox = new PictureBox();
            ShowGrid = new CheckBox();
            GridSizeBar = new TrackBar();
            debug = new Label();
            SizeLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DrawingBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GridSizeBar).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Margin = new Padding(0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(DrawingBox);
            splitContainer1.Panel1MinSize = 800;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = SystemColors.ActiveCaption;
            splitContainer1.Panel2.Controls.Add(ShowGrid);
            splitContainer1.Panel2.Controls.Add(GridSizeBar);
            splitContainer1.Panel2.Controls.Add(debug);
            splitContainer1.Panel2.Controls.Add(SizeLabel);
            splitContainer1.Size = new Size(1541, 935);
            splitContainer1.SplitterDistance = 971;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 0;
            // 
            // DrawingBox
            // 
            DrawingBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DrawingBox.Location = new Point(0, 0);
            DrawingBox.MaximumSize = new Size(914, 1067);
            DrawingBox.MinimumSize = new Size(914, 1067);
            DrawingBox.Name = "DrawingBox";
            DrawingBox.Size = new Size(914, 1067);
            DrawingBox.TabIndex = 0;
            DrawingBox.TabStop = false;
            DrawingBox.Paint += DrawingBox_Paint;
            // 
            // ShowGrid
            // 
            ShowGrid.AutoSize = true;
            ShowGrid.Checked = true;
            ShowGrid.CheckState = CheckState.Checked;
            ShowGrid.Location = new Point(8, 77);
            ShowGrid.Margin = new Padding(3, 4, 3, 4);
            ShowGrid.Name = "ShowGrid";
            ShowGrid.Size = new Size(129, 24);
            ShowGrid.TabIndex = 4;
            ShowGrid.Text = "wyswietl siatke";
            ShowGrid.UseVisualStyleBackColor = true;
            // 
            // GridSizeBar
            // 
            GridSizeBar.Location = new Point(3, 37);
            GridSizeBar.Margin = new Padding(3, 4, 3, 4);
            GridSizeBar.Maximum = 20;
            GridSizeBar.Minimum = 5;
            GridSizeBar.Name = "GridSizeBar";
            GridSizeBar.Size = new Size(119, 56);
            GridSizeBar.TabIndex = 3;
            GridSizeBar.Value = 10;
            GridSizeBar.ValueChanged += GridSizeBar_ValueChanged;
            // 
            // debug
            // 
            debug.AutoSize = true;
            debug.Location = new Point(273, 12);
            debug.Name = "debug";
            debug.Size = new Size(50, 20);
            debug.TabIndex = 2;
            debug.Text = "label1";
            // 
            // SizeLabel
            // 
            SizeLabel.AutoSize = true;
            SizeLabel.Location = new Point(3, 13);
            SizeLabel.Name = "SizeLabel";
            SizeLabel.Size = new Size(142, 20);
            SizeLabel.TabIndex = 1;
            SizeLabel.Text = "Wymiar siatki [5-20]";
            // 
            // TriangleGrid
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1541, 935);
            Controls.Add(splitContainer1);
            MaximizeBox = false;
            Name = "TriangleGrid";
            StartPosition = FormStartPosition.Manual;
            Text = "Siatka Trojkatow";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DrawingBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)GridSizeBar).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private PictureBox DrawingBox;
        private Label SizeLabel;
        private Label debug;
        private TrackBar GridSizeBar;
        private CheckBox ShowGrid;
    }
}