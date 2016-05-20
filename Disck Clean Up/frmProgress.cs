using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Disck_Clean_Up
{
    public partial class frmProgress : Form
    {
        public frmProgress()
        {
            InitializeComponent();
        }

        //در این تابع دنبال فایل با فرمت tmp میگردیم و ریشه را از کاربر به عنوان پارامتر گرفتیم
        public void GetFilesFromDirectory(string rootFolderPath, ListView listView1)
        {
            
            Queue<string> pending = new Queue<string>();// یک صف برای ذخیره ریشه فایلا
            DirectoryInfo myDirectoryInfo = new DirectoryInfo(rootFolderPath);//ادرس ریشه را قرار میدیم
            FileInfo[] myFileInfo = myDirectoryInfo.GetFiles("*.tmp");//فرمت فایل را مشخص میکنیم
            pending.Enqueue(rootFolderPath);//ادرس ریشه اصلی را داخل صف میزاره
            string[] myStr;// برای ذخیره ادرس ریشه
            
            //این حلقه تا زمانی که تمام زیر شاخه ها را بگردد ادامه دارد
            while (pending.Count > 0)
            {
                //برای کنترل exception
                try
                {
               
                    rootFolderPath = pending.Dequeue();// زیر شاخه ها را از صف میگیره و دنبال فایل tmp در داخل ان ها میگردد
                    myDirectoryInfo = new DirectoryInfo(rootFolderPath);//زیر شاخه به عنوان ریشه در نظر گرفته میشود
                    myFileInfo = myDirectoryInfo.GetFiles("*.tmp");//تمام فایلایی که در این شاخه دارای فرمت tmp هستند

                    //باید فایلا را در لیست نمایش بدیم این حلقه به تعداد فایلا تکرار میشود
                    foreach (FileInfo file in myFileInfo)
                    {
                        this.Update();// فرم اپ دیت میکنیم 
                        label1.Text = file.DirectoryName + file.Name;// مسیری که برنامه قرار دارد را به کاربر نمایش میدهیم 

                        //فایل را در لیست قرار می دهیم
                        ListViewItem myItm = new ListViewItem();
                        myItm.Text = file.Name;// نام فایل
                        myItm.SubItems.Add(file.DirectoryName);// ادرس فایل
                        myItm.SubItems.Add(string.Format(new FileSizeFormatProvider(), "{0:fs}", file.Length));//سایز فایل
                        listView1.Items.Add(myItm);
                        listView1.Refresh();
                      
                    }
               
                    //مسیر زیر شاخه ای که الان به عنوان ریشه انتخاب شده 
                    myStr = Directory.GetDirectories(rootFolderPath);
                    
                    //  زیر شاخه های موجود در زیر شاخه فعلی را داخل صف قرار می دهیم 
                    for (int i = 0; i < myStr.Length; i++)
                    {

                        pending.Enqueue(myStr[i]);
                    }
                }
                    //اگر اجازه دسترسی نداشت ازش بگذره
                catch (UnauthorizedAccessException)
                {
                }
                
            }
            this.Close();// فرم میبندیم به فرم اصلی برمیگردیم
        }

        private void frmProgress_Load(object sender, EventArgs e)
        {

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
    }
}
