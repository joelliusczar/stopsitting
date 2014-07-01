using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StopSittingMain
{
    public partial class StopSittingForm : Form
    {
        private string message = "Time to stand up!";
        private Thread starter;
        private bool isThreadRunning = false;

        public StopSittingForm()
        {
            InitializeComponent();

            this.FormClosing += (sender, e) => this.isThreadRunning = false;
        }

        private void DisplayTime(TimeSpan t)
        {
            lblTimeDisplay.Text = t.Minutes + " : " + t.Seconds + " : " + t.Milliseconds;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.btnStart.Enabled = false;
            this.isThreadRunning = true;
            this.starter = new Thread(new ThreadStart(this.HandleTime));
            this.starter.Start();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = true;
            this.isThreadRunning = false;
            this.DisplayTime(TimeSpan.Zero);
        }

        private void numericUpDownMinutes_KeyPress(object sender, KeyPressEventArgs e)
        {}


        private void btnTest0_Click(object sender, EventArgs e)
        {
            Thread.Sleep(5000);
            AlertBox box = new AlertBox(message);
            
            box.ShowDialog();
            Thread.Sleep(5000);
            box.ShowDialog();
            
        }

        private void HandleTime()
        {
            Stopwatch threadStopwatch = new Stopwatch();
            TimeSpan t;
            threadStopwatch.Start();
            AlertBox box = new AlertBox(message);
            bool resetTime = false;
            IAsyncResult result = null;
            while (this.isThreadRunning)
            {
                
                if (result != null && result.IsCompleted && resetTime)
                {
                    threadStopwatch.Restart();
                    resetTime = false;
                    
                }
                t = threadStopwatch.Elapsed;

                if (true)
                {
                    this.InvokeDisplayTime(t);

                    if (t.Minutes >= ((int)numericUpDownMinutes.Value))
                    {
                        threadStopwatch.Restart();
                        if (result != null && !result.IsCompleted)
                        {
                            if (this.InvokeRequired)
                            {
                                this.Invoke(new MethodInvoker(() => this.ResetAlertBox(box)));
                            }
                            else
                            {
                                this.ResetAlertBox(box);
                            }
                        }

                        if (!resetTime)
                        {
                            result = this.BeginInvoke(new Func<AlertBox, DialogResult>(OpenAlertBox), box);
                            resetTime = true;
                        }



                    }
                }
            }

        }

        private DialogResult OpenAlertBox(AlertBox box)
        {
            return box.BringAttentionAndShowDialog();

        }

        private void ResetAlertBox(AlertBox box)
        {
            box.BringAttentionToBox();
        }

        private void OpenAlertBoxInNewThread(AlertBox box)
        {
            if (box.InvokeRequired)
            {
                box.Invoke(new MethodInvoker(() => box.BringAttentionAndShowDialog()));
            }
            else
            {
                box.BringAttentionAndShowDialog();
            }
        }

        private void InvokeDisplayTime(TimeSpan t)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(() => this.DisplayTime(t)));
                }
                else
                {
                    this.DisplayTime(t);
                }
            }
            catch (ObjectDisposedException disposedEx)
            {
                this.isThreadRunning = false;
                Console.WriteLine(disposedEx.Message);
            }

        }



    }
}
