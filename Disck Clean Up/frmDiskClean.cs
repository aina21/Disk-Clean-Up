using Disck_Clean_Up.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Disck_Clean_Up
{
    public partial class frmDiskClean : Form
    {
        public frmProgress progress; // یک نمونه از فرم progress

        public frmDiskClean()
        {
            InitializeComponent();
        }

       
        private void toolStripSearch_Click(object sender, EventArgs e)
        {
            progress = new frmProgress();
            listView1.Items.Clear();//لیست نمایش فایل یک بار پاک میکنه 
            
            //مسیر ریشه از کاربر میگیره
            folderBrowserDialog1.ShowDialog(); 
            string rootFolderPath = folderBrowserDialog1.SelectedPath;//این رشته ادرس ریشه را تو خودش داره

            //فرم پراگسس اجرا میکنیم
            progress.Show();
            this.progress.Update();// آپ دیت میکنیم که فرم درست نشون بده
            progress.GetFilesFromDirectory(rootFolderPath, listView1);// این تابع تعریف شده برای سرچ فایلا با فرمت tmp
            
            this.selectAllToolStripMenuItem_Click(sender, e);//اینجا میاد همه فایلا رو انتخاب میکنه 
            
        }



        private void toolStripCleaner_Click(object sender, EventArgs e)
        {
            //مسیج قبل از پاک کردن فایل
            string msg = "Are you sure want delete these files ? ";
            DialogResult msgResult = MessageBox.Show(null, msg, "Disk remover",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            //اگر کاربر مطمن بود فایلا رو از تو لیست پاک میکنه
            if (msgResult == DialogResult.OK)
            {
                foreach (ListViewItem item in listView1.Items)
                {
                    string location = item.SubItems[1].Text + "\\" + item.Text;//ادرس محلی که فایل ذخیره شده
                   //اگر فایل را کاربر انتخاب کرده بود
                    if (item.Checked == true)
                    {
                        File.Delete(@location);//فایل پاک میکنه
                        item.Remove();
                    }
                }
            }
            listView1.Refresh();

        }

        private void scanDiskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.toolStripSearch_Click(sender, e);
        }

        private void cleanFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
             this.toolStripCleaner_Click(sender, e); 
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //انتخاب همه فایلای لیست
            foreach (ListViewItem itm in listView1.Items)
            {
                itm.Checked = true;
            }
        }

        private void diseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //deselect all files
            foreach (ListViewItem itm in listView1.Items)
            {
                itm.Checked = false;
            }
        }

        private void reselectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //reselect
            foreach (ListViewItem itm in listView1.Items)
            {
                if (itm.Checked)
                    itm.Checked = false;
                else
                    itm.Checked = true;
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //اگر هیچ فایلی توی لیست نبود این ایتم غیر فعال میشود
             if (listView1.CheckedItems.Count > 0)
                 cleanFilesToolStripMenuItem.Enabled = true;
            else
                cleanFilesToolStripMenuItem.Enabled = false;
        }

        private void toolStripRestore_Click(object sender, EventArgs e)
        {
            //سیستم ریستور سیستم عامل را اجرا میکند
            ProcessStartInfo procInfo = new ProcessStartInfo();
            //فایل rstrui.exe را که سیستم ریستور سیستم عامل برابر با اسم پراسس میکنیم
            procInfo.FileName = ("rstrui.exe");
            Process.Start(procInfo);//پراسس اجرا میکنیم
        }

        private void frmDiskClean_Load(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {


        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
