namespace Bitmap32Test
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.bCopy = new System.Windows.Forms.Button();
            this.bScale = new System.Windows.Forms.Button();
            this.bRotate = new System.Windows.Forms.Button();
            this.bFlipX = new System.Windows.Forms.Button();
            this.bFlipY = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 71);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(6);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(1230, 658);
            this.splitContainer1.SplitterDistance = 610;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 0;
            // 
            // bCopy
            // 
            this.bCopy.Location = new System.Drawing.Point(24, 15);
            this.bCopy.Margin = new System.Windows.Forms.Padding(6);
            this.bCopy.Name = "bCopy";
            this.bCopy.Size = new System.Drawing.Size(150, 44);
            this.bCopy.TabIndex = 2;
            this.bCopy.Text = "Copy";
            this.bCopy.UseVisualStyleBackColor = true;
            this.bCopy.Click += new System.EventHandler(this.bCopy_Click);
            // 
            // bScale
            // 
            this.bScale.Location = new System.Drawing.Point(186, 15);
            this.bScale.Margin = new System.Windows.Forms.Padding(6);
            this.bScale.Name = "bScale";
            this.bScale.Size = new System.Drawing.Size(150, 44);
            this.bScale.TabIndex = 3;
            this.bScale.Text = "Scale";
            this.bScale.UseVisualStyleBackColor = true;
            this.bScale.Click += new System.EventHandler(this.bScale_Click);
            // 
            // bRotate
            // 
            this.bRotate.Location = new System.Drawing.Point(348, 15);
            this.bRotate.Margin = new System.Windows.Forms.Padding(6);
            this.bRotate.Name = "bRotate";
            this.bRotate.Size = new System.Drawing.Size(150, 44);
            this.bRotate.TabIndex = 4;
            this.bRotate.Text = "Rotate";
            this.bRotate.UseVisualStyleBackColor = true;
            this.bRotate.Click += new System.EventHandler(this.bRotate_Click);
            // 
            // bFlipX
            // 
            this.bFlipX.Location = new System.Drawing.Point(510, 15);
            this.bFlipX.Margin = new System.Windows.Forms.Padding(6);
            this.bFlipX.Name = "bFlipX";
            this.bFlipX.Size = new System.Drawing.Size(150, 44);
            this.bFlipX.TabIndex = 5;
            this.bFlipX.Text = "Flip X";
            this.bFlipX.UseVisualStyleBackColor = true;
            this.bFlipX.Click += new System.EventHandler(this.bFlipX_Click);
            // 
            // bFlipY
            // 
            this.bFlipY.Location = new System.Drawing.Point(672, 15);
            this.bFlipY.Margin = new System.Windows.Forms.Padding(6);
            this.bFlipY.Name = "bFlipY";
            this.bFlipY.Size = new System.Drawing.Size(150, 44);
            this.bFlipY.TabIndex = 6;
            this.bFlipY.Text = "Flip Y";
            this.bFlipY.UseVisualStyleBackColor = true;
            this.bFlipY.Click += new System.EventHandler(this.bFlipY_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 733);
            this.Controls.Add(this.bFlipY);
            this.Controls.Add(this.bFlipX);
            this.Controls.Add(this.bRotate);
            this.Controls.Add(this.bScale);
            this.Controls.Add(this.bCopy);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button bCopy;
        private System.Windows.Forms.Button bScale;
        private System.Windows.Forms.Button bRotate;
        private System.Windows.Forms.Button bFlipX;
        private System.Windows.Forms.Button bFlipY;
    }
}

