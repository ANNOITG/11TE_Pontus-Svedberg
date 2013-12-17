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
using System.Diagnostics;


namespace FileExplorer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            PopulateTreeView();
        }

        private void PopulateTreeView()
        {
            TreeNode rootNode;

            DirectoryInfo info = new DirectoryInfo(@"C:\");
            if (info.Exists)
            {
                rootNode = new TreeNode(info.Name);
                rootNode.Tag = info;
                GetDirectories(info.GetDirectories(), rootNode);
                treeView1.Nodes.Add(rootNode);
            }
        }

        private void GetDirectories(DirectoryInfo[] subDirs,
                        TreeNode nodeToAddTo)
        {

            TreeNode aNode;

            foreach (DirectoryInfo subDir in subDirs)
            {
                try
                {
                    aNode = new TreeNode(subDir.Name, 0, 0);
                    aNode.Tag = subDir;
                    aNode.ImageKey = "folder";

                    nodeToAddTo.Nodes.Add(aNode);
                }
                catch
                {
                    int i = 0;
                }
            }
        }

        void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                e.Node.Nodes.Clear();

                TreeNode newSelected = e.Node;
                listView1.Items.Clear();
                DirectoryInfo nodeDirInfo = (DirectoryInfo)newSelected.Tag;
                ListViewItem.ListViewSubItem[] subItems;
                ListViewItem item = null;

                TreeNode rootNode;

                DirectoryInfo info = nodeDirInfo;
                if (info.Exists)
                {
                    rootNode = e.Node;
                    rootNode.Tag = info;
                    GetDirectories(info.GetDirectories(), rootNode);
                }

                foreach (DirectoryInfo dir in nodeDirInfo.GetDirectories())
                {
                    item = new ListViewItem(dir.Name, 0);
                    subItems = new ListViewItem.ListViewSubItem[]
        {new ListViewItem.ListViewSubItem(item, "Directory"),
            new ListViewItem.ListViewSubItem(item, 
                                    dir.LastAccessTime.ToShortDateString())};
                    item.SubItems.AddRange(subItems);
                    listView1.Items.Add(item);
                }
                foreach (FileInfo file in nodeDirInfo.GetFiles())
                {
                    item = new ListViewItem(file.Name, 1);
                    subItems = new ListViewItem.ListViewSubItem[]
        {new ListViewItem.ListViewSubItem(item, "File"),
            new ListViewItem.ListViewSubItem(item,
                                    file.LastAccessTime.ToShortDateString())};

                    item.SubItems.AddRange(subItems);
                    listView1.Items.Add(item);
                }

                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                //this.treeView1.NodeMouseClick +=
                //    new TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            }
            catch
            {
                MessageBox.Show("");
            }
        }

        class myTreeNode : TreeNode
        {
            public string FilePath;

            public myTreeNode(string fp)
            {
                FilePath = fp;
                this.Text = fp.Substring(fp.LastIndexOf("\\"));
            }
        }

        protected void treeView1_AfterSelect(object sender,
            System.Windows.Forms.TreeViewEventArgs e)
        {
            myTreeNode myNode = (myTreeNode)e.Node;
            MessageBox.Show("Node selected is " + myNode.FilePath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            treeView1.Nodes.Add(new myTreeNode(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + @"\TextFile.txt"));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            ListViewItem foundItem = listView1.FindItemWithText(searchBox.Text, false, 0, true);
            if (foundItem != null)
            {
                listView1.TopItem = foundItem;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {


            //System.Diagnostics.Process.Start();
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                ListView Temp = (ListView)sender;
                foreach (ListViewItem item in Temp.SelectedItems)
                {
                    psi.FileName = item.Text;
                }

                Process p = new Process();
                p.StartInfo = psi;
                p.Start();
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
        }
    }
}
