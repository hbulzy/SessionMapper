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
            this.lstats = new System.Windows.Forms.Label();
            this.bb = new System.Windows.Forms.Button();
            this.rrtb = new System.Windows.Forms.RichTextBox();
            this.tip = new System.Windows.Forms.TextBox();
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.lv = new System.Windows.Forms.ListView();
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
            // lstats
            // 
            this.lstats.AutoSize = true;
            this.lstats.Location = new System.Drawing.Point(12, 674);
            this.lstats.Name = "lstats";
            this.lstats.Size = new System.Drawing.Size(34, 13);
            this.lstats.TabIndex = 2;
            this.lstats.Text = "Stats:";
            // 
            // bb
            // 
            this.bb.Location = new System.Drawing.Point(220, 7);
            this.bb.Name = "bb";
            this.bb.Size = new System.Drawing.Size(141, 23);
            this.bb.TabIndex = 3;
            this.bb.Text = "Map One IP ==>";
            this.bb.UseVisualStyleBackColor = true;
            this.bb.Click += new System.EventHandler(this.bb_Click);
            // 
            // rrtb
            // 
            this.rrtb.Location = new System.Drawing.Point(499, 10);
            this.rrtb.Name = "rrtb";
            this.rrtb.Size = new System.Drawing.Size(216, 21);
            this.rrtb.TabIndex = 4;
            this.rrtb.Text = "";
            // 
            // tip
            // 
            this.tip.Location = new System.Drawing.Point(367, 10);
            this.tip.Name = "tip";
            this.tip.Size = new System.Drawing.Size(126, 20);
            this.tip.TabIndex = 5;
            // 
            // gmap
            // 
            this.gmap.Bearing = 0F;
            this.gmap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gmap.CanDragMap = true;
            this.gmap.GrayScaleMode = false;
            this.gmap.LevelsKeepInMemmory = 5;
            this.gmap.Location = new System.Drawing.Point(721, 14);
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
            this.gmap.Size = new System.Drawing.Size(915, 673);
            this.gmap.TabIndex = 6;
            this.gmap.Zoom = 0D;
            this.gmap.Load += new System.EventHandler(this.gmap_Load);
            // 
            // lv
            // 
            this.lv.Location = new System.Drawing.Point(12, 36);
            this.lv.Name = "lv";
            this.lv.Size = new System.Drawing.Size(703, 635);
            this.lv.TabIndex = 8;
            this.lv.UseCompatibleStateImageBehavior = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1648, 699);
            this.Controls.Add(this.lv);
            this.Controls.Add(this.gmap);
            this.Controls.Add(this.tip);
            this.Controls.Add(this.rrtb);
            this.Controls.Add(this.bb);
            this.Controls.Add(this.lstats);
            this.Controls.Add(this.bstart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
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
        private System.Windows.Forms.Label lstats;
        private System.Windows.Forms.Button bb;
        private System.Windows.Forms.RichTextBox rrtb;
        private System.Windows.Forms.TextBox tip;
        private GMap.NET.WindowsForms.GMapControl gmap;
        private System.Windows.Forms.ListView lv;
    }
}

