using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace DianMing
{
    public partial class Form2 : Form
    {
        string[] namelist;
        string[] stanamelist;
        int[] selected = new int[7];
        int cnt=0;
        int last = 0;
        Random random = new Random();
        Thread thread;
        bool threadon = true;
        public Form2(string[] namel)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            stanamelist = namel;
        }

        private void initlabel(Label lb,string name)
        {
            lb.Location = new Point(145, 10);
            lb.Text = name;
            lb.Font = new Font("宋体",15);
            lb.Visible = true;
        }

        private string select()
        {
            int temp = (random.Next(namelist.Length)+last)%namelist.Length;
            while(selected.Contains(temp))
            {
                temp = (random.Next(namelist.Length) + last) % namelist.Length;
            }
            selected[cnt] = temp;
            cnt = (cnt + 1)%7;
            return namelist[temp];
        }
        private void rolldown(Label lb)
        {
            if(lb.Location.Y < 100)
            {
                lb.Location = new Point(lb.Location.X - 5, lb.Location.Y + 30);
                lb.Font = new Font("宋体", lb.Font.Size + 2);
            } 
            else if (lb.Location.Y < 190)
            {
                lb.Location = new Point(lb.Location.X + 5, lb.Location.Y + 30);
                lb.Font = new Font("宋体", lb.Font.Size - 2);
            }
            else
            {
                initlabel(lb, select());
            }
            if (lb.Location.Y == 100)
            {
                lb.ForeColor = Color.DeepSkyBlue;
            }
            else if(lb.Location.Y == 130)
            {
                lb.ForeColor= Color.Black;
            }
        }

        private void roll()
        {
            while(true)
            {
                foreach (Control ctrl in Controls)
                {
                    if(ctrl is Label)
                    {
                        rolldown((Label)ctrl);
                    }
                }
                Thread.Sleep(50);
                while(!threadon)
                {
                    Thread.Sleep(1000);
                }
            }
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            namelist = stanamelist;
            int cntt = 0;
            foreach(Control c in Controls)
            {
                if(c is Label)
                {
                    initlabel((Label)c, select());
                    for(int i = 0; i < cntt; i++)
                    {
                        rolldown((Label)c);
                    }
                    cntt++;
                }
            }
            thread = new Thread(roll);
            thread.Start();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            thread.Abort();
        }

        private void Form2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (threadon)
            {
                threadon = false;
                uiButton1.Text = "按任意键以开始";
            }   
            else
            {
                threadon = true;
                uiButton1.Text = "按任意键以停止";
            }
        }
    }
}
