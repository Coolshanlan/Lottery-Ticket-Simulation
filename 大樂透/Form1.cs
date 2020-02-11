using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 大樂透
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int People_Money = 20000;
        Int32 All_Money = 0;
        Label[] lb = new Label[2];
        TextBox[] tbx = new TextBox[6];
        List<List<int>> people_num = new List<List<int>>();
        private void Form1_Load(object sender, EventArgs e)
        {
            Random rd = new Random();
            int rdn = rd.Next(1, 22);
            All_Money = rdn* 100000000;
            label4.Text += rdn.ToString()+"億元";
            int t = 0;
            lb[0] = label3;
            lb[1] = label5;
            foreach (Control c in this.Controls) if ( c.Tag != null && c.Tag.ToString() == "People") tbx[t++] = (TextBox)c;
            label1.Text = "當前資金：" + People_Money + "元";
            label2.Text = "當期購買張數：" + people_num.Count + "張";
        }
        Random rd = new Random();
        private void button2_Click(object sender, EventArgs e)
        {
            for(int ii = 0; ii < int.Parse(textBox7.Text); ii++)
            {
                if (People_Money < 100) return;
                List<int> num = new List<int>();
                bool ok = true;
                do
                {
                    ok = true;
                    num.Clear();
                    for (int i = 0; i < 6; i++)
                    {
     
                        int rdn = rd.Next(1, 50);
                        if (num.FindAll(x => x == rdn).Count == 0) num.Add(rdn);
                        else i--;
                    }
                    foreach (var a in people_num)
                    {
                        int t = 0;
                        foreach (var aa in a)
                        {
                            foreach (var aaa in num)
                            {
                                if (aaa == aa) t++;
                            }
                        }
                        if (t == 6) ok = false;
                    }
                } while (!ok);
                for (int i = 0; i < 6; i++)
                {
                    tbx[i].Text = num[i].ToString("00");
                }
                button1.PerformClick();
            }
           
        }
        private void button1_Click(object sender, EventArgs e)
        {

            List<int> num = new List<int>();
            bool canadd = true;
            foreach (var a in tbx) if (a.Text.Trim() == "") canadd = false;
            if (canadd)
            {
                foreach (TextBox t in tbx) num.Add(int.Parse(t.Text));
                foreach (var a in people_num)
                {
                    int t = 0;
                    foreach (var aa in a)
                    {
                        foreach (var aaa in num)
                        {
                            if (aaa == aa) t++;
                        }
                    }
                    if (t == 6) canadd = false;
                }
                for (int i = 0; i < 6; i++)
                {
                    for (int ii = 0; ii < 6; ii++)
                    {
                        if (num[i] == num[ii] && i != ii) canadd = false;
                    }
                }

                if (canadd)
                {
                    if (People_Money >= 100)
                    {
                        //if (people_num.Count == 10)
                        //{
                        //    MessageBox.Show("目前只開放買10組歐~你這貪心鬼！");
                        //    return;
                        //}
                        People_Money -= 100;
                    }
                    else
                    {
                        MessageBox.Show("金錢不足");
                        return;
                    }
                    people_num.Add(num);
                    num.Sort();
                    label1.Text = "當前資金：" + People_Money + "元";
                    label2.Text = "當期購買張數：" + people_num.Count + "張";
                }
                else
                {
                    MessageBox.Show("輸入有誤！");
                    return;
                }
                if (people_num.Count <= 10)
                {
                    if (people_num.Count > 5)
                    {
                        lb[1].Text += "第" + people_num.Count + "組號碼：";
                        foreach (int a in num)
                        {
                            lb[1].Text += a.ToString("00") + "  ";
                        }
                        lb[1].Text += "\r\n" + "\r\n";
                    }
                    else
                    {
                        lb[0].Text += "第" + people_num.Count + "組號碼：";
                        foreach (int a in num)
                        {
                            lb[0].Text += a.ToString("00") + "  ";
                        }
                        lb[0].Text += "\r\n" + "\r\n";
                    }
                }
            }
            else
            {
                MessageBox.Show("輸入有誤！");
                return;
            }
        }
        GroupBox g;
        private void button3_Click(object sender, EventArgs e)
        {
            g = groupBox1;
            GameStart gm;
            gm = new GameStart(People_Money, people_num, g,All_Money);
            gm.ShowDialog();
            this.Controls.Add(g);
            people_num.Clear();
            foreach(Control c in g.Controls)
            {
                c.Text = "";
            }
            People_Money = gm.people_Money;
            label1.Text = "當前資金：" + People_Money + "元";
            label2.Text = "當期購買張數：" + people_num.Count + "張";
            button4.Text = "More";
            Random rd = new Random();
            int rdn = rd.Next(1, 22);
            All_Money = rdn * 100000000;
            label4.Text ="當期總獎金："+ rdn.ToString() + "億元";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            data dt = new data(people_num);
            dt.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (var a in tbx) a.Text = "";
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button2.PerformClick();
        }
    }
}
