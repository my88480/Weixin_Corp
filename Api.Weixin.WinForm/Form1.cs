using Api.Weixin.WinForm.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Api.Weixin.WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 获取TokenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WeiXinForm frm = new WeiXinForm();
            frm.ShowDialog();
        }
    }
}
