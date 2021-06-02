using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TestWindowsService
{
    public partial class Scheduler : ServiceBase
    {
        private Timer timer = null;
        public Scheduler()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer();
            this.timer.Interval = 30000; //make an interval on 30 seconds 
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Tick);
            timer.Enabled = true;
            Library.WriteErrorLog("Window service started");
        }

        private void timer_Tick(object sender, ElapsedEventArgs e)
        {
            Library.WriteErrorLog("Timer ticked and something has happend");
        }

        protected override void OnStop()
        {
            timer.Enabled = false;
            Library.WriteErrorLog("My window service stopped");
        }
    }
}
