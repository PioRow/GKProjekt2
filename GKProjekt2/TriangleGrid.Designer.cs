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
            VecMapCheck = new CheckBox();
            LoadVecMapBtn = new Button();
            AnimationButton = new Button();
            label1 = new Label();
            CPModCB = new GroupBox();
            CPHeight = new TrackBar();
            CPYlabel = new Label();
            CPXLabel = new Label();
            controlPointvalLabel = new Label();
            CPXCB = new ComboBox();
            CPYCB = new ComboBox();
            SelectPicture = new Button();
            fillWPicture = new RadioButton();
            lightHeightLabel = new Label();
            lightHeight = new TrackBar();
            mLabel = new Label();
            mBar = new TrackBar();
            ksLabel = new Label();
            kdLabel = new Label();
            ksBar = new TrackBar();
            kdBar = new TrackBar();
            PlainColorFIll = new RadioButton();
            lightColorPick = new Button();
            ObjColorPick = new Button();
            ShowGrid = new CheckBox();
            GridSizeBar = new TrackBar();
            debug = new Label();
            SizeLabel = new Label();
            colorPickDIalog = new ColorDialog();
            lightColorPickDIal = new ColorDialog();
            selectPictureDialog = new OpenFileDialog();
            NormalMapDialog = new OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DrawingBox).BeginInit();
            CPModCB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CPHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lightHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ksBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kdBar).BeginInit();
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
            splitContainer1.Panel2.Controls.Add(VecMapCheck);
            splitContainer1.Panel2.Controls.Add(LoadVecMapBtn);
            splitContainer1.Panel2.Controls.Add(AnimationButton);
            splitContainer1.Panel2.Controls.Add(label1);
            splitContainer1.Panel2.Controls.Add(CPModCB);
            splitContainer1.Panel2.Controls.Add(SelectPicture);
            splitContainer1.Panel2.Controls.Add(fillWPicture);
            splitContainer1.Panel2.Controls.Add(lightHeightLabel);
            splitContainer1.Panel2.Controls.Add(lightHeight);
            splitContainer1.Panel2.Controls.Add(mLabel);
            splitContainer1.Panel2.Controls.Add(mBar);
            splitContainer1.Panel2.Controls.Add(ksLabel);
            splitContainer1.Panel2.Controls.Add(kdLabel);
            splitContainer1.Panel2.Controls.Add(ksBar);
            splitContainer1.Panel2.Controls.Add(kdBar);
            splitContainer1.Panel2.Controls.Add(PlainColorFIll);
            splitContainer1.Panel2.Controls.Add(lightColorPick);
            splitContainer1.Panel2.Controls.Add(ObjColorPick);
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
            // VecMapCheck
            // 
            VecMapCheck.AutoSize = true;
            VecMapCheck.Location = new Point(13, 535);
            VecMapCheck.Name = "VecMapCheck";
            VecMapCheck.Size = new Size(223, 24);
            VecMapCheck.TabIndex = 27;
            VecMapCheck.Text = "modyfikuj o wczytany wektor";
            VecMapCheck.UseVisualStyleBackColor = true;
            VecMapCheck.CheckedChanged += VecMapCheck_CheckedChanged;
            // 
            // LoadVecMapBtn
            // 
            LoadVecMapBtn.Location = new Point(40, 565);
            LoadVecMapBtn.Name = "LoadVecMapBtn";
            LoadVecMapBtn.Size = new Size(113, 93);
            LoadVecMapBtn.TabIndex = 26;
            LoadVecMapBtn.Text = "Wczytaj Mape Wektorow";
            LoadVecMapBtn.UseVisualStyleBackColor = true;
            LoadVecMapBtn.Click += LoadVecMapBtn_Click;
            // 
            // AnimationButton
            // 
            AnimationButton.Location = new Point(9, 429);
            AnimationButton.Name = "AnimationButton";
            AnimationButton.Size = new Size(126, 65);
            AnimationButton.TabIndex = 25;
            AnimationButton.Text = "Rozpocznij animacje";
            AnimationButton.UseVisualStyleBackColor = true;
            AnimationButton.Click += AnimationButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 24;
            label1.Text = "label1";
            // 
            // CPModCB
            // 
            CPModCB.Controls.Add(CPHeight);
            CPModCB.Controls.Add(CPYlabel);
            CPModCB.Controls.Add(CPXLabel);
            CPModCB.Controls.Add(controlPointvalLabel);
            CPModCB.Controls.Add(CPXCB);
            CPModCB.Controls.Add(CPYCB);
            CPModCB.Location = new Point(292, 330);
            CPModCB.Name = "CPModCB";
            CPModCB.Size = new Size(250, 125);
            CPModCB.TabIndex = 23;
            CPModCB.TabStop = false;
            CPModCB.Text = "modyfikacja pkt kontrolnych";
            // 
            // CPHeight
            // 
            CPHeight.Location = new Point(93, 60);
            CPHeight.Name = "CPHeight";
            CPHeight.Size = new Size(130, 56);
            CPHeight.TabIndex = 25;
            CPHeight.ValueChanged += CPHeight_ValueChanged;
            // 
            // CPYlabel
            // 
            CPYlabel.AutoSize = true;
            CPYlabel.Location = new Point(111, 32);
            CPYlabel.Name = "CPYlabel";
            CPYlabel.Size = new Size(19, 20);
            CPYlabel.TabIndex = 24;
            CPYlabel.Text = "y:";
            // 
            // CPXLabel
            // 
            CPXLabel.AutoSize = true;
            CPXLabel.Location = new Point(5, 29);
            CPXLabel.Name = "CPXLabel";
            CPXLabel.Size = new Size(19, 20);
            CPXLabel.TabIndex = 23;
            CPXLabel.Text = "x:";
            // 
            // controlPointvalLabel
            // 
            controlPointvalLabel.AutoSize = true;
            controlPointvalLabel.Location = new Point(27, 73);
            controlPointvalLabel.Name = "controlPointvalLabel";
            controlPointvalLabel.Size = new Size(60, 20);
            controlPointvalLabel.TabIndex = 22;
            controlPointvalLabel.Text = "wartosc";
            // 
            // CPXCB
            // 
            CPXCB.FormattingEnabled = true;
            CPXCB.Items.AddRange(new object[] { "0", "1", "2", "3" });
            CPXCB.Location = new Point(27, 26);
            CPXCB.Name = "CPXCB";
            CPXCB.Size = new Size(42, 28);
            CPXCB.TabIndex = 18;
            CPXCB.SelectedIndexChanged += CPXCB_SelectedIndexChanged;
            // 
            // CPYCB
            // 
            CPYCB.FormattingEnabled = true;
            CPYCB.Items.AddRange(new object[] { "0", "1", "2", "3" });
            CPYCB.Location = new Point(136, 26);
            CPYCB.Name = "CPYCB";
            CPYCB.Size = new Size(42, 28);
            CPYCB.TabIndex = 19;
            CPYCB.SelectedIndexChanged += CPYCB_SelectedIndexChanged;
            // 
            // SelectPicture
            // 
            SelectPicture.Location = new Point(9, 252);
            SelectPicture.Name = "SelectPicture";
            SelectPicture.Size = new Size(128, 74);
            SelectPicture.TabIndex = 17;
            SelectPicture.Text = "wybierz obraz do wypełnienia";
            SelectPicture.UseVisualStyleBackColor = true;
            SelectPicture.Click += SelectPicture_Click;
            // 
            // fillWPicture
            // 
            fillWPicture.AutoSize = true;
            fillWPicture.Location = new Point(5, 222);
            fillWPicture.Name = "fillWPicture";
            fillWPicture.Size = new Size(148, 24);
            fillWPicture.TabIndex = 16;
            fillWPicture.TabStop = true;
            fillWPicture.Text = "wypełnij obrazem";
            fillWPicture.UseVisualStyleBackColor = true;
            // 
            // lightHeightLabel
            // 
            lightHeightLabel.AutoSize = true;
            lightHeightLabel.Location = new Point(3, 344);
            lightHeightLabel.Name = "lightHeightLabel";
            lightHeightLabel.Size = new Size(167, 20);
            lightHeightLabel.TabIndex = 15;
            lightHeightLabel.Text = "wysokosc zrodla swiatla";
            // 
            // lightHeight
            // 
            lightHeight.Location = new Point(9, 367);
            lightHeight.Maximum = 20;
            lightHeight.Minimum = 10;
            lightHeight.Name = "lightHeight";
            lightHeight.Size = new Size(130, 56);
            lightHeight.TabIndex = 14;
            lightHeight.Value = 10;
            lightHeight.ValueChanged += lightHeight_ValueChanged;
            // 
            // mLabel
            // 
            mLabel.AutoSize = true;
            mLabel.Location = new Point(473, 14);
            mLabel.Name = "mLabel";
            mLabel.Size = new Size(22, 20);
            mLabel.TabIndex = 13;
            mLabel.Text = "m";
            // 
            // mBar
            // 
            mBar.Location = new Point(473, 37);
            mBar.Maximum = 100;
            mBar.Minimum = 1;
            mBar.Name = "mBar";
            mBar.Orientation = Orientation.Vertical;
            mBar.Size = new Size(56, 274);
            mBar.TabIndex = 12;
            mBar.Value = 1;
            mBar.ValueChanged += mBar_ValueChanged;
            // 
            // ksLabel
            // 
            ksLabel.AutoSize = true;
            ksLabel.Location = new Point(412, 14);
            ksLabel.Name = "ksLabel";
            ksLabel.Size = new Size(22, 20);
            ksLabel.TabIndex = 11;
            ksLabel.Text = "ks";
            // 
            // kdLabel
            // 
            kdLabel.AutoSize = true;
            kdLabel.Location = new Point(339, 13);
            kdLabel.Name = "kdLabel";
            kdLabel.Size = new Size(28, 20);
            kdLabel.TabIndex = 10;
            kdLabel.Text = "kd:";
            // 
            // ksBar
            // 
            ksBar.Location = new Point(412, 37);
            ksBar.Maximum = 100;
            ksBar.Name = "ksBar";
            ksBar.Orientation = Orientation.Vertical;
            ksBar.Size = new Size(56, 274);
            ksBar.TabIndex = 9;
            ksBar.Value = 50;
            ksBar.ValueChanged += trackBar1_ValueChanged;
            // 
            // kdBar
            // 
            kdBar.Location = new Point(339, 37);
            kdBar.Maximum = 100;
            kdBar.Name = "kdBar";
            kdBar.Orientation = Orientation.Vertical;
            kdBar.Size = new Size(56, 274);
            kdBar.TabIndex = 8;
            kdBar.Value = 50;
            kdBar.ValueChanged += kdBar_ValueChanged;
            // 
            // PlainColorFIll
            // 
            PlainColorFIll.AutoSize = true;
            PlainColorFIll.Checked = true;
            PlainColorFIll.Location = new Point(9, 108);
            PlainColorFIll.Name = "PlainColorFIll";
            PlainColorFIll.Size = new Size(144, 24);
            PlainColorFIll.TabIndex = 7;
            PlainColorFIll.TabStop = true;
            PlainColorFIll.Text = "wypelnij kolorem";
            PlainColorFIll.UseVisualStyleBackColor = true;
            PlainColorFIll.CheckedChanged += PlainColorFIll_CheckedChanged;
            // 
            // lightColorPick
            // 
            lightColorPick.Location = new Point(87, 130);
            lightColorPick.Name = "lightColorPick";
            lightColorPick.Size = new Size(78, 79);
            lightColorPick.TabIndex = 6;
            lightColorPick.Text = "wybierz\r\nkolor\r\nswiatla\r\n";
            lightColorPick.UseVisualStyleBackColor = true;
            lightColorPick.Click += lightColorPick_Click;
            // 
            // ObjColorPick
            // 
            ObjColorPick.Location = new Point(8, 130);
            ObjColorPick.Name = "ObjColorPick";
            ObjColorPick.Size = new Size(73, 79);
            ObjColorPick.TabIndex = 5;
            ObjColorPick.Text = "wybierz\r\nkolor\r\nobiektu\r\n";
            ObjColorPick.TextAlign = ContentAlignment.TopLeft;
            ObjColorPick.UseVisualStyleBackColor = true;
            ObjColorPick.Click += ColorPick_Click;
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
            ShowGrid.CheckedChanged += ShowGrid_CheckedChanged;
            // 
            // GridSizeBar
            // 
            GridSizeBar.Location = new Point(3, 37);
            GridSizeBar.Margin = new Padding(3, 4, 3, 4);
            GridSizeBar.Maximum = 20;
            GridSizeBar.Minimum = 1;
            GridSizeBar.Name = "GridSizeBar";
            GridSizeBar.Size = new Size(119, 56);
            GridSizeBar.TabIndex = 3;
            GridSizeBar.Value = 10;
            GridSizeBar.ValueChanged += GridSizeBar_ValueChanged;
            // 
            // debug
            // 
            debug.AutoSize = true;
            debug.Location = new Point(192, 13);
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
            SizeLabel.Text = "Wymiar siatki [1-20]";
            // 
            // selectPictureDialog
            // 
            selectPictureDialog.FileName = "selectPictureDialog";
            // 
            // NormalMapDialog
            // 
            NormalMapDialog.FileName = "openFileDialog1";
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
            CPModCB.ResumeLayout(false);
            CPModCB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)CPHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)lightHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)mBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)ksBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)kdBar).EndInit();
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
        private Button ObjColorPick;
        private ColorDialog colorPickDIalog;
        private Button lightColorPick;
        private ColorDialog lightColorPickDIal;
        private RadioButton PlainColorFIll;
        private Label mLabel;
        private TrackBar mBar;
        private Label ksLabel;
        private Label kdLabel;
        private TrackBar ksBar;
        private TrackBar kdBar;
        private Label lightHeightLabel;
        private TrackBar lightHeight;
        private Button SelectPicture;
        private RadioButton fillWPicture;
        private OpenFileDialog selectPictureDialog;
        private GroupBox CPModCB;
        private ComboBox CPXCB;
        private Button ConfirmCPButton;
        private ComboBox CPYCB;
        private TextBox CPVal;
        private Label label1;
        private Label CPYlabel;
        private Label CPXLabel;
        private Label controlPointvalLabel;
        private Button AnimationButton;
        private TrackBar CPHeight;
        private CheckBox VecMapCheck;
        private Button LoadVecMapBtn;
        private OpenFileDialog NormalMapDialog;
    }
}