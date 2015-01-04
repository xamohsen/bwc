using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BurrowsWheelerCompression.UI
{
    public partial class OpenFileForm : Form
    {
        public OpenFileForm()
        {
            InitializeComponent();
            txtFilePath.Text = CompressionHelper.FilePath;
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            Process.Start(txtFilePath.Text);
        }
    }
}
