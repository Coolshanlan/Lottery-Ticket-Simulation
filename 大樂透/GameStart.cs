using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 大樂透
{
    public partial class GameStart : Form
    {
        GroupBox g;
        public GameStart(int people_Money , List<List<int>> people_num,GroupBox g, Int32 All_money)
        {
            InitializeComponent();
            this.g = g;
            this.people_Money = people_Money;
            this.people_num = people_num;
            this.Controls.Add(g);
            this.All_Money = All_money;
        }
        List<List<int>> people_num;
        public int people_Money;
        Int32 All_Money;
        int lb1time = 0;
        Button[] tbx = new Button[7];
        private void timer1_Tick(object sender, EventArgs e)
        {
            lb1time++;
            if(anstimes != 6)
            label1.Text = anstimes+1 + "號球開獎中";
            else label1.Text = "特別號 " + "開獎中";
            for (int i = 0; i < lb1time % 4; i++)
            {
                label1.Text += "。";
            }
        }
        int[] ansnum = new int[6];
        private void GameStart_Load(object sender, EventArgs e)
        {
            int t = 0;
            foreach(Control c in this.Controls)
                if(c.Tag != null && c.Tag.ToString() == "tbx")
                {
                    tbx[t] = new Button();
                    tbx[t++] = (Button)c;
                }
        }
        int anstimes = 0;
        bool stop = false;
        private void numchange_Tick(object sender, EventArgs e)
        {
            if (anstimes == 7)
            {
                Thread.Sleep(1000);
                anstimes--;
                numchange.Enabled = false;
                numstop.Enabled = false;
                balltxt.Enabled = false;
                label1.Location = new Point(label1.Location.X-75,label1.Location.Y);
                label1.Text = "當期中獎號碼為：";
                Array.Sort(ansnum);
                foreach (int a in ansnum)
                {
                    label1.Text += a.ToString("00") + "  ";
                }
                label1.Text += special.ToString("00");
                foreach (var a in tbx) a.FlatAppearance.BorderColor = Color.Lime;
                tbx[6].FlatAppearance.BorderColor = Color.Orange;
                check();
                return;
            }

            Random rd = new Random();
            for (int i = anstimes; i < 7; i++)
            {
                int rdn = rd.Next(1, 50);
                tbx[i].Text = rdn.ToString("00");
            }
            if (stop)
            {
                bool ansok = true;
                do
                {
                    stop = false;
                    ansok = true;
                    rd = new Random();
                    int rdn = rd.Next(1, 50);
                    foreach (int a in ansnum)
                    {
                        if (a == rdn) ansok = false;
                    }
                    if (ansok)
                    {
                        if (anstimes != 6)
                            ansnum[anstimes] = rdn;
                        else special = rdn;
                        tbx[anstimes].Text = rdn.ToString("00");
                        tbx[anstimes++].FlatAppearance.BorderColor = Color.Red;
                        if (anstimes != 6)
                            label1.Text = anstimes + 1 + "號球開獎中";
                        else label1.Text = "特別號 " + "開獎中";
                    }
                } while (!ansok);
                if (anstimes == 7)
                 label1.Text = "開獎完成";
            }
        }
        List<Label> yes = new List<Label>();
        int special = 0;
        void check()
        {
            foreach(List<int> p in people_num)
            {
                int count = 0;
                foreach( int pp in p)
                    foreach(int ans in ansnum)
                        if( pp == ans)
                            count++;
                if (count == 2 && p.FindAll(x => x == special).Count == 1) addlb(p, count, true, 400);
                else if (count == 3)
                {
                    if(p.FindAll(x => x == special).Count == 1)addlb(p, count, true, 1000);
                    else addlb(p, count, false ,400);
                }
                else if (count == 4)
                {
                    if (p.FindAll(x => x == special).Count == 1) addlb(p, count, true, (int)(All_Money * 0.05));
                    else addlb(p, count, false, (int)(All_Money * 0.04));
                }
                else if (count == 5)
                {
                    if (p.FindAll(x => x == special).Count == 1) addlb(p, count, true, (int)(All_Money * 0.1));
                    else addlb(p, count, false, (int)(All_Money * 0.08));
                }
                else if (count == 5)
                {
                    addlb(p, count, false, (int)(All_Money * 0.73));
                }
            }
            if(yes.Count == 0)
            {
                yes.Add(new Label());
                yes[yes.Count - 1].AutoSize = true;
                yes[yes.Count - 1].Location = new Point(this.g.Location.X + this.g.Size.Width + 20, this.g.Location.Y + (yes.Count - 1) * 30);
                yes[yes.Count - 1].ForeColor = Color.Blue;
                yes[yes.Count - 1].Font = new Font("微軟正黑體", 12);
                if(people_Money == 0)
                {
                    people_Money += 500;
                    yes[yes.Count - 1].Text = "好可黏歐，" + people_num.Count + "張全共辜了呢\r\n\r\n幫你QQ拉  別難過\r\n\r\n好啦再給你500元，別再哭哭了歐\r\n\r\n也別再共辜了歐 呵呵~~~~~~";
                }
                else yes[yes.Count - 1].Text = "好可黏歐，" + people_num.Count + "張全共辜了呢\r\n\r\n幫你QQ拉 \r\n\r\n 別難過拉  你還有"+people_Money+"元 \r\n\r\n加油加油歐~~呵呵";
                this.Controls.Add(yes[yes.Count - 1]);
            }
        }
        void addlb(List<int> peoplenum ,int count , bool special , int money)
        {
            people_Money += money;
            yes.Add(new Label());
            yes[yes.Count - 1].AutoSize = true;
            yes[yes.Count - 1].Location = new Point(this.g.Location.X + this.g.Size.Width + 20, this.g.Location.Y + (yes.Count - 1) * 30);
            foreach (int a in peoplenum) yes[yes.Count - 1].Text += a.ToString("00") + "  ";
            yes[yes.Count - 1].Text += "   中" + count + "碼";
            yes[yes.Count - 1].ForeColor = Color.Red;
            yes[yes.Count - 1].Font = new Font("微軟正黑體",12);
            if (special) yes[yes.Count - 1].Text += "  及 特別號";
            yes[yes.Count - 1].Text +="  共"+money +"元";
            this.Controls.Add(yes[yes.Count-1]);
        }
        private void numstop_Tick(object sender, EventArgs e)
        {
            stop = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button7.Enabled = false;
            numstop.Enabled = true;
            balltxt.Enabled = true;
        }
    }
}
