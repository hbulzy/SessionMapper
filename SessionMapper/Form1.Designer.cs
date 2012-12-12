namespace SessionMapper
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bstart = new System.Windows.Forms.Button();
            this.rtb = new System.Windows.Forms.RichTextBox();
            this.rtbTl = new System.Windows.Forms.RichTextBox();
            this.rtbUl = new System.Windows.Forms.RichTextBox();
            this.lstats = new System.Windows.Forms.Label();
            this.bb = new System.Windows.Forms.Button();
            this.rrtb = new System.Windows.Forms.RichTextBox();
            this.tip = new System.Windows.Forms.TextBox();
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // bstart
            // 
            this.bstart.Location = new System.Drawing.Point(12, 7);
            this.bstart.Name = "bstart";
            this.bstart.Size = new System.Drawing.Size(202, 23);
            this.bstart.TabIndex = 0;
            this.bstart.Text = "Start";
            this.bstart.UseVisualStyleBackColor = true;
            this.bstart.Click += new System.EventHandler(this.bstart_Click);
            // 
            // rtb
            // 
            this.rtb.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb.Location = new System.Drawing.Point(279, 42);
            this.rtb.Name = "rtb";
            this.rtb.Size = new System.Drawing.Size(276, 591);
            this.rtb.TabIndex = 1;
            this.rtb.Text = "";
            this.rtb.TextChanged += new System.EventHandler(this.rtb_TextChanged);
            // 
            // rtbTl
            // 
            this.rtbTl.Location = new System.Drawing.Point(12, 42);
            this.rtbTl.Name = "rtbTl";
            this.rtbTl.Size = new System.Drawing.Size(127, 591);
            this.rtbTl.TabIndex = 1;
            this.rtbTl.Text = "";
            // 
            // rtbUl
            // 
            this.rtbUl.Location = new System.Drawing.Point(145, 42);
            this.rtbUl.Name = "rtbUl";
            this.rtbUl.Size = new System.Drawing.Size(128, 591);
            this.rtbUl.TabIndex = 1;
            this.rtbUl.Text = "";
            // 
            // lstats
            // 
            this.lstats.AutoSize = true;
            this.lstats.Location = new System.Drawing.Point(12, 639);
            this.lstats.Name = "lstats";
            this.lstats.Size = new System.Drawing.Size(34, 13);
            this.lstats.TabIndex = 2;
            this.lstats.Text = "Stats:";
            // 
            // bb
            // 
            this.bb.Location = new System.Drawing.Point(220, 7);
            this.bb.Name = "bb";
            this.bb.Size = new System.Drawing.Size(86, 23);
            this.bb.TabIndex = 3;
            this.bb.Text = "Get Location";
            this.bb.UseVisualStyleBackColor = true;
            this.bb.Click += new System.EventHandler(this.bb_Click);
            // 
            // rrtb
            // 
            this.rrtb.Location = new System.Drawing.Point(410, 10);
            this.rrtb.Name = "rrtb";
            this.rrtb.Size = new System.Drawing.Size(145, 21);
            this.rrtb.TabIndex = 4;
            this.rrtb.Text = "";
            // 
            // tip
            // 
            this.tip.Location = new System.Drawing.Point(321, 10);
            this.tip.Name = "tip";
            this.tip.Size = new System.Drawing.Size(83, 20);
            this.tip.TabIndex = 5;
            // 
            // gmap
            // 
            this.gmap.Bearing = 0F;
            this.gmap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gmap.CanDragMap = true;
            this.gmap.GrayScaleMode = false;
            this.gmap.LevelsKeepInMemmory = 5;
            this.gmap.Location = new System.Drawing.Point(561, 14);
            this.gmap.MarkersEnabled = true;
            this.gmap.MaxZoom = 2;
            this.gmap.MinZoom = 2;
            this.gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmap.Name = "gmap";
            this.gmap.NegativeMode = false;
            this.gmap.PolygonsEnabled = true;
            this.gmap.RetryLoadTile = 0;
            this.gmap.RoutesEnabled = true;
            this.gmap.ShowTileGridLines = false;
            this.gmap.Size = new System.Drawing.Size(1044, 635);
            this.gmap.TabIndex = 6;
            this.gmap.Zoom = 0D;
            this.gmap.Load += new System.EventHandler(this.gmap_Load);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1617, 661);
            this.Controls.Add(this.gmap);
            this.Controls.Add(this.tip);
            this.Controls.Add(this.rrtb);
            this.Controls.Add(this.bb);
            this.Controls.Add(this.lstats);
            this.Controls.Add(this.rtbUl);
            this.Controls.Add(this.rtbTl);
            this.Controls.Add(this.rtb);
            this.Controls.Add(this.bstart);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bstart;
        private System.Windows.Forms.RichTextBox rtb;
        private System.Windows.Forms.RichTextBox rtbTl;
        private System.Windows.Forms.RichTextBox rtbUl;
        private System.Windows.Forms.Label lstats;
        private System.Windows.Forms.Button bb;
        private System.Windows.Forms.RichTextBox rrtb;
        private System.Windows.Forms.TextBox tip;
        private GMap.NET.WindowsForms.GMapControl gmap;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

