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
    public partial class data : Form
    {
        public data(List<List<int>> p)
        {
            InitializeComponent();
            people_money = p;
        }
        List<List<int>> people_money;
        private void data_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("彩卷號碼");
            foreach(var a in people_money)
            {
                string s = "";
                foreach(var aa in a)
                {
                    s += aa.ToString("00") + "  ";
                }
                dt.Rows.Add(s);
            }
            dataGridView1.DataSource = dt;
        }
    }
}
