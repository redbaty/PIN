using System;
using System.IO;
using System.Windows.Forms;
using PIN.Core.Packages;

namespace PIN.Core.Forms
{

    public partial class iCreator : Form
    {
        public iCreator()
        {
            InitializeComponent();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text)) return;
            if (string.IsNullOrEmpty(txtX64.Text)) return;
            if (string.IsNullOrEmpty(txtX86.Text)) return;


            IAP cIap = new IAP(txtName.Text,txtVersion.Text,txtUrl.Text,txtArgs.Text,txtX86.Text,txtX64.Text,Convert.ToInt32(txtPrior.Value), Path.GetDirectoryName(txtX86.Text) + @"\");
            cIap.Debug();
        }

        private void btnFindpath_Click(object sender, EventArgs e)
        {
            txtX86.Text = GetPath();
        }

        private string GetPath()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "Executable files (*.exe)|*.exe",
                FilterIndex = 2,
                RestoreDirectory = true
            };


            return openFileDialog1.ShowDialog() == DialogResult.OK ? openFileDialog1.FileName : "invalid";
        }

        private void btnFindpath2_Click(object sender, EventArgs e)
        {
            txtX64.Text = GetPath();
        }

        private void btnURL_Click(object sender, EventArgs e)
        {

        }
    }
}
