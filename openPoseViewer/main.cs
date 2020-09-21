using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using openposeImageKey;
using System.Drawing.Imaging;

namespace openPoseViewer
{
    public partial class main : Form
    {
        private string path = "";
        private string fileName = "";
        private openposeImageFrame[] imageFrames = null;
        private int imageIndex;

        public main()
        {
            InitializeComponent();

            this.path = Application.StartupPath;
            this.imageIndex = 0;
            this.cbxType.SelectedIndex = 0;
        }

        private void main_Load(object sender, EventArgs e)
        {

        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Close openpose viewer?", "Openpose viewer", MessageBoxButtons.YesNo) != DialogResult.Yes)
                e.Cancel = true;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Load open pose key-point file";
            ofd.InitialDirectory = this.path;
            ofd.Filter = "Text file|*.txt|All files|*.*";
            if(ofd.ShowDialog()== DialogResult.OK)
            {
                this.path = Path.GetDirectoryName(ofd.FileName);
                this.fileName = Path.GetFileNameWithoutExtension(ofd.FileName);
                lblFile.Text = "(" + this.fileName + ") " + this.path;

                try
                {
                    if(cbxType.SelectedItem.ToString()=="coco")
                    {
                        this.imageFrames = openposeImageFrame.loadFromTextFile_coco(ofd.FileName);
                    }
                    else
                        this.imageFrames = openposeImageFrame.loadFromTextFile(ofd.FileName);

                    this.imageIndex = 0;
                }
                catch (Exception ex)
                {
                    imageFrames = null;
                    MessageBox.Show("Failed to reading openpose keypoint file");
                    return;
                }

                cbxImage.Items.Clear();
                if (this.imageFrames == null || this.imageFrames.Length < 1)
                    return;

                foreach(openposeImageFrame f in this.imageFrames)
                {
                    try
                    {
                        if (File.Exists(this.path + "\\" + f.fileName))
                            cbxImage.Items.Add(f.fileName);
                    }
                    catch { }
                }

                if(cbxImage.Items.Count>0)
                {
                    cbxImage.SelectedIndex = 0;
                    cbxImage.Focus();
                }
            }
        }

        private void cbxImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox c = (ComboBox)sender;
            foreach (openposeImageFrame f in this.imageFrames)
            {
                if (f.fileName == c.SelectedItem.ToString())
                {
                    this.picMain.Image = Image.FromFile(this.path + "\\" + f.fileName);
                    foreach(openposeKeyPoint[] kp in f.bodys)
                    {
                        if(cbxType.SelectedItem.ToString()=="coco")
                        {
                            imageLabeler.labelCoco(this.picMain.Image, kp);
                        }    
                        else
                            imageLabeler.labelBody25(this.picMain.Image, kp);
                    }
                    
                }
            }
        }

        private void btnBatch_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog sfd = new FolderBrowserDialog();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select batch folder to save results";

            ofd.InitialDirectory = this.path;
            ofd.FileName = "selected file will be ignored";
            ofd.CheckFileExists = false;
            ofd.CheckPathExists = true;
            ofd.ValidateNames = false;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string savePath = Path.GetDirectoryName(ofd.FileName);
                foreach (openposeImageFrame f in this.imageFrames)
                {
                    try
                    {
                        //generate exist images only
                        string pathName = this.path + "\\" + f.fileName;
                        if (File.Exists(pathName))
                        {
                            Image im = Image.FromFile(pathName);
                            foreach (openposeKeyPoint[] kp in f.bodys)
                            {
                                if (cbxType.SelectedItem.ToString() == "coco")
                                {
                                    imageLabeler.labelCoco(im, kp);
                                }
                                else
                                    imageLabeler.labelBody25(im, kp);
                            }
                            string saveName = savePath + "\\" + Path.GetFileNameWithoutExtension(f.fileName)+"_gen.png";
                            if(File.Exists(saveName))
                            {
                                MessageBox.Show(saveName + " is exist! abort batch process");
                                return;
                            }
                            im.Save(saveName, ImageFormat.Png);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to generate anotated image"+Environment.NewLine+ex.Message);
                        return;
                    }
                }
                MessageBox.Show("Anontation successful");
            }
        }
    }
}
