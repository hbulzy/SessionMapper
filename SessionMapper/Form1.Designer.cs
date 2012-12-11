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
            this.SuspendLayout();
            // 
            // bstart
            // 
            this.bstart.Location = new System.Drawing.Point(13, 13);
            this.bstart.Name = "bstart";
            this.bstart.Size = new System.Drawing.Size(202, 23);
            this.bstart.TabIndex = 0;
            this.bstart.Text = "Start";
            this.bstart.UseVisualStyleBackColor = true;
            this.bstart.Click += new System.EventHandler(this.bstart_Click);
            // 
            // rtb
            // 
            this.rtb.Location = new System.Drawing.Point(12, 42);
            this.rtb.Name = "rtb";
            this.rtb.Size = new System.Drawing.Size(448, 607);
            this.rtb.TabIndex = 1;
            // 
            // rtbTl
            // 
            this.rtbTl.Location = new System.Drawing.Point(466, 42);
            this.rtbTl.Name = "rtbTl";
            this.rtbTl.Size = new System.Drawing.Size(202, 607);
            this.rtbTl.TabIndex = 1;
            // 
            // rtbUl
            // 
            this.rtbUl.Location = new System.Drawing.Point(674, 42);
            this.rtbUl.Name = "rtbUl";
            this.rtbUl.Size = new System.Drawing.Size(202, 607);
            this.rtbUl.TabIndex = 1;
            // 
            // lstats
            // 
            this.lstats.AutoSize = true;
            this.lstats.Location = new System.Drawing.Point(231, 13);
            this.lstats.Name = "lstats";
            this.lstats.Size = new System.Drawing.Size(34, 13);
            this.lstats.TabIndex = 2;
            this.lstats.Text = "Stats:";
            // 
            // bb
            // 
            this.bb.Location = new System.Drawing.Point(944, 42);
            this.bb.Name = "bb";
            this.bb.Size = new System.Drawing.Size(75, 57);
            this.bb.TabIndex = 3;
            this.bb.Text = "Get Location";
            this.bb.UseVisualStyleBackColor = true;
            this.bb.Click += new System.EventHandler(this.bb_Click);
            // 
            // rrtb
            // 
            this.rrtb.Location = new System.Drawing.Point(907, 189);
            this.rrtb.Name = "rrtb";
            this.rrtb.Size = new System.Drawing.Size(140, 170);
            this.rrtb.TabIndex = 4;
            // 
            // tip
            // 
            this.tip.Location = new System.Drawing.Point(930, 138);
            this.tip.Name = "tip";
            this.tip.Size = new System.Drawing.Size(100, 20);
            this.tip.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1059, 661);
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
    }
}

