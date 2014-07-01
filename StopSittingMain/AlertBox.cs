using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StopSittingMain
{
    public partial class AlertBox : Form
    {
        //private AlertBox _box;

        //private AlertBox Box
        //{
        //    get
        //    {
        //        if (_box == null || _box.IsDisposed)
        //        {
        //            Box = new AlertBox(message);
        //        }
        //        return _box;
        //    }
        //    set
        //    {
        //        _box = value;
        //_box.FormClosed += (sender, e) =>
        //{
        //    watch.Reset();
        //    DisplayTime(TimeSpan.Zero);
        //};
        //    }
        //}

        public AlertBox(string message)
        {
            
            InitializeComponent();
            this.lblMessage.Text = message;
        }

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AlertBox_Load(object sender, EventArgs e)
        {
            //this.TopMost = true;
            //
            this.CenterToScreen();
            this.TopMost = true;
        }

        public void SetToScreenCenter()
        {
            this.CenterToScreen();
        }

        public void BringAttentionToBox()
        {
            this.WindowState = FormWindowState.Normal;
            this.SetToScreenCenter();
            this.TopMost = true;
        }

        public DialogResult BringAttentionAndShowDialog()
        {
            this.BringAttentionToBox();
            return this.ShowDialog();
        }
    }
}
