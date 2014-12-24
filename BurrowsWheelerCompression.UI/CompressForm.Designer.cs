namespace BurrowsWheelerCompression.UI
{
    partial class CompressForm
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
            this.btnDecompressFile = new System.Windows.Forms.Button();
            this.txtCompressedFilePath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOpenCompressedFile
            // 
            this.btnOpenCompressedFile.Location = new System.Drawing.Point(130, 112);
            this.btnOpenCompressedFile.Name = "btnOpenCompressedFile";
            this.btnOpenCompressedFile.Size = new System.Drawing.Size(99, 23);
            this.btnOpenCompressedFile.TabIndex = 1;
            this.btnOpenCompressedFile.Text = "Open File";
            this.btnOpenCompressedFile.UseVisualStyleBackColor = true;
            this.btnOpenCompressedFile.Click += new System.EventHandler(this.btnCompressedFile_Click);
            // 
            // btnDecompressFile
            // 
            this.btnDecompressFile.Location = new System.Drawing.Point(280, 112);
            this.btnDecompressFile.Name = "btnDecompressFile";
            this.btnDecompressFile.Size = new System.Drawing.Size(113, 23);
            this.btnDecompressFile.TabIndex = 2;
            this.btnDecompressFile.Text = "Decompress File";
            this.btnDecompressFile.UseVisualStyleBackColor = true;
            this.btnDecompressFile.Click += new System.EventHandler(this.btnDecompressFile_Click);
            // 
            // txtCompressedFilePath
            // 
            this.txtCompressedFilePath.Enabled = false;
            this.txtCompressedFilePath.Location = new System.Drawing.Point(13, 38);
            this.txtCompressedFilePath.Multiline = true;
            this.txtCompressedFilePath.Name = "txtCompressedFilePath";
            this.txtCompressedFilePath.Size = new System.Drawing.Size(491, 41);
            this.txtCompressedFilePath.TabIndex = 5;
            // 
            // CompressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 177);
            this.Controls.Add(this.txtCompressedFilePath);
            this.Controls.Add(this.btnDecompressFile);
            this.Controls.Add(this.btnOpenCompressedFile);
            this.Name = "CompressForm";
            this.Text = "CompressForm";
            this.Load += new System.EventHandler(this.CompressForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenCompressedFile;
        private System.Windows.Forms.Button btnDecompressFile;
        private System.Windows.Forms.TextBox txtCompressedFilePath;
    }
}