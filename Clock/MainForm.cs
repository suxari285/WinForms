using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Clock
{
    public partial class MainForm : Form
    {
        ColorDialog backgroundColorDialog;
        ColorDialog foregroundColorDialog;
        FontDialog fontDialog;
        public MainForm()
        {
            InitializeComponent();
            AllocConsole ();

            this.Location = new Point
                (
                    Screen.PrimaryScreen.Bounds.Right- this.Width-50,
                    Screen.PrimaryScreen.Bounds.Bottom / 20
                );
            backgroundColorDialog = new ColorDialog ();
            foregroundColorDialog = new ColorDialog ();
            fontDialog = new FontDialog (this);
            SetVisibility (false);
        }
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();
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
            notifyIcon.Text = LabelTime.Text;
        }

        private void cbShowDate_CheckedChanged(object sender, EventArgs e)=> tsmiShowDate.Checked = cbShowDate.Checked;

        private void LabelTime_Click(object sender, EventArgs e)
        {

        }

        private void btnHideContols_Click(object sender, EventArgs e) => SetVisibility(tsmiShowControls.Checked = false);
        
        private void LabelTime_DoubleClick(object sender, EventArgs e) => SetVisibility(tsmiShowControls.Checked = true);

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void tsmiExit_Click(object sender, EventArgs e) => this.Close();

        private void tsmiTopmost_CheckedChanged(object sender, EventArgs e) => this.TopMost = tsmiTopmost.Checked;

        private void tsmiShowControls_CheckedChanged(object sender, EventArgs e) => SetVisibility(tsmiShowControls.Checked);

        private void tsmiShowDate_CheckedChanged(object sender, EventArgs e) => cbShowDate.Checked = tsmiShowDate.Checked;

        private void tsmiShowWeekday_CheckedChanged(object sender, EventArgs e)=>cbShowWeekday.Checked = tsmiShowWeekday.Checked;

        private void cbShowWeekday_CheckedChanged(object sender, EventArgs e)=> tsmiShowWeekday.Checked = cbShowWeekday.Checked;

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (this.TopMost==false)
            {
                this.TopMost = true;
                this.TopMost = false;
            }
        }

        private void tsmiBackgroundColor_Click(object sender, EventArgs e)
        {
            if(backgroundColorDialog.ShowDialog() == DialogResult.OK)
                LabelTime.BackColor = backgroundColorDialog.Color;
        }

        private void tsmiForegroundColor_Click(object sender, EventArgs e)
        {
            if (foregroundColorDialog.ShowDialog() == DialogResult.OK)
                LabelTime.ForeColor = foregroundColorDialog.Color;
        }

        private void tsmiFont_Click(object sender, EventArgs e)
        {
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                LabelTime.Font = fontDialog.Font;
            }
        }

        private void tsmiAutorun_CheckedChanged(object sender, EventArgs e)
        {
            string key_name = "Clock_P_418";
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (tsmiAutorun.Checked) registryKey.SetValue(key_name, Application.ExecutablePath);
            else registryKey.DeleteValue(key_name,false);
            registryKey.Dispose();
        }
    }
}
