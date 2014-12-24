namespace BurrowsWheelerCompression.UI
{
    partial class DecompressedForm
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
            this.btnOpenCompressedFile = new System.Windows.Forms.Button();
            this.txtDecompressedFilePath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOpenCompressedFile
            // 
            this.btnOpenCompressedFile.Location = new System.Drawing.Point(203, 116);
            this.btnOpenCompressedFile.Name = "btnOpenCompressedFile";
            this.btnOpenCompressedFile.Size = new System.Drawing.Size(99, 23);
            this.btnOpenCompressedFile.TabIndex = 3;
            this.btnOpenCompressedFile.Text = "Open File";
            this.btnOpenCompressedFile.UseVisualStyleBackColor = true;
            this.btnOpenCompressedFile.Click += new System.EventHandler(this.btnOpenCompressedFile_Click);
            // 
            // txtDecompressedFilePath
            // 
            this.txtDecompressedFilePath.Enabled = false;
            this.txtDecompressedFilePath.Location = new System.Drawing.Point(13, 38);
            this.txtDecompressedFilePath.Multiline = true;
            this.txtDecompressedFilePath.Name = "txtDecompressedFilePath";
            this.txtDecompressedFilePath.Size = new System.Drawing.Size(491, 41);
            this.txtDecompressedFilePath.TabIndex = 4;
            // 
            // DecompressedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 177);
            this.Controls.Add(this.txtDecompressedFilePath);
            this.Controls.Add(this.btnOpenCompressedFile);
            this.Name = "DecompressedForm";
            this.Text = "DecompressedForm";
            this.Load += new System.EventHandler(this.DecompressedForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenCompressedFile;
        private System.Windows.Forms.TextBox txtDecompressedFilePath;
    }
}