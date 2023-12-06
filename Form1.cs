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
namespace DianMing
{
    public partial class Form1 : Form
    {
        Form2 form2;
        string[] namelist;
        public Form1()
        {
            InitializeComponent();
        }

        private string[] loaddata()
        {
            using (StreamReader sr = new StreamReader("data.txt"))
            {
                char[] chars = { '\r','\n' };
                string [] data = sr.ReadToEnd().Split(chars,StringSplitOptions.RemoveEmptyEntries);
                return data;
            }
        }
        private void uiButton1_Click(object sender, EventArgs e)
        {
            if (form2 == null||form2.IsDisposed)
            {
                form2 = new Form2(namelist);
                form2.Show();
            }
            else
            {
                form2.Focus();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            namelist = new string[40];
            try
            {
                if(File.Exists("data.txt"))
                {
                    namelist = loaddata();
                }
                for (int i = 0; i <namelist.Length; i++)
                {
                    if(namelist[i].Length == 2)
                    {
                        namelist[i] = " "+namelist[i];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n数据异常或未添加名单");
            }
            
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            string text = "在当前目录下创建data.txt文件\n输入名单并保存（每行一个名字）\n放心食用\n\n\t\t——by Eclipsky 2022";
            MessageBox.Show(text);
        }
    }
}
