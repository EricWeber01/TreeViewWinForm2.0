using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Media;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreeViewWinForm2._0
{
    public partial class Form1 : Form
    {
        public string put = "";
        public Form1()
        {
            InitializeComponent();
        }

        public void dob(string fname, string name, TreeNode tr)
        {
            DirectoryInfo difo = new DirectoryInfo(fname);
            DirectoryInfo[] dir = difo.GetDirectories();
            FileInfo[] fil = difo.GetFiles();
            foreach (FileInfo v in fil)
            {
                tr.Nodes.Add(v.Name);
            }
            foreach (DirectoryInfo v in dir)
            {
                tr.Nodes.Add(v.Name);
                dob(v.FullName, v.Name, tr.LastNode);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            if (fb.ShowDialog() == DialogResult.OK)
            {
                put = fb.SelectedPath;
                DirectoryInfo dir = new DirectoryInfo(fb.SelectedPath);
                DirectoryInfo[] dfi = dir.GetDirectories();
                FileInfo[] fil = dir.GetFiles();
                foreach (FileInfo v in fil)
                {
                    treeView1.Nodes.Add(v.Name);
                }
                foreach (DirectoryInfo df in dfi)
                {
                    TreeNode td = new TreeNode(df.Name);
                    dob(df.FullName, df.Name, td);
                    treeView1.Nodes.Add(td);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("Title");
            listView1.Columns.Add("Type");
            listView1.Columns.Add("Size");
            listView1.Columns.Add("Date");
            imageList1 = new ImageList();
            imageList1.ImageSize = new Size(16, 16);
            imageList1.ColorDepth = ColorDepth.Depth24Bit;
            Bitmap dirLogo = new Bitmap(@"../ec06293b-e995-4015-ba13-3f2916bea285.png");
            imageList1.Images.Add("dir", dirLogo);
            listView1.SmallImageList = imageList1;
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            DirectoryInfo directory = new DirectoryInfo(put + "\\" + treeView1.SelectedNode.FullPath);
            foreach (FileInfo file in directory.GetFiles())
            {
                if (!imageList1.Images.ContainsKey(file.Extension))
                {
                    Icon icon = Icon.ExtractAssociatedIcon(file.FullName);
                    imageList1.Images.Add(file.Extension, icon);
                }
                ListViewItem item = listView1.Items.Add(file.Name, file.Extension);
                item.SubItems.Add(file.Extension);
                item.SubItems.Add(file.Length.ToString());
                item.SubItems.Add(file.LastWriteTime.ToString());
            }
            foreach (DirectoryInfo dir in directory.GetDirectories())
            {
                ListViewItem item = listView1.Items.Add(dir.Name, "dir");
                item.SubItems.Add("folder");
                item.SubItems.Add("---");
                item.SubItems.Add(dir.LastWriteTime.ToString());
            }
        }

        private void данныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.Details;
        }

        private void плитыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.Tile;
        }

        private void smallIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.SmallIcon;
        }
    }
}
