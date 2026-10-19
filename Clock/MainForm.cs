using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clock
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        void SetVisibility(bool visible)
        {
            cbShowDate.Visible     = visible;
            cbShowWeekday.Visible  = visible;
            btnHideContols.Visible = visible;
            this.ShowInTaskbar     = visible;
            this.FormBorderStyle   = visible ? FormBorderStyle.FixedToolWindow : FormBorderStyle.None;
            this.TransparencyKey   = visible ? Color.Empty :this.BackColor;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            LabelTime.Text = DateTime.Now.ToString("HH:mm:ss");
            if (cbShowDate.Checked)
                LabelTime.Text += $"\n{DateTime.Now.ToString("yyyy.MM.dd")}";
            if (cbShowWeekday.Checked)
                LabelTime.Text += $"\n{DateTime.Now.DayOfWeek}";
        }

        private void cbShowDate_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void LabelTime_Click(object sender, EventArgs e)
        {

        }

        private void btnHideContols_Click(object sender, EventArgs e) => SetVisibility(false);
        
        private void LabelTime_DoubleClick(object sender, EventArgs e) => SetVisibility(true);

    }
}
