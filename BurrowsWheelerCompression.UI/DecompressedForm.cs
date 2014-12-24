using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace BurrowsWheelerCompression.UI
{
    public partial class DecompressedForm : Form
    {
        public DecompressedForm()
        {
            InitializeComponent();
        }

        private void btnOpenCompressedFile_Click(object sender, EventArgs e)
        {
            string fileName = txtDecompressedFilePath.Text;
            Process.Start(fileName);
        }

        private void DecompressedForm_Load(object sender, EventArgs e)
        {
            txtDecompressedFilePath.Text = CompressionHelper.DecompressedFilePath;
        }
    }
}
