using GetDataFromRoadInfo.DAL;
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

namespace GetDataFromRoadInfo
{
    public partial class Scheduler : ServiceBase
    {
        //TODO: Add logging to eventlog
        string lastxml;
        Postgres postgre = new Postgres();

        HttpRequestManager requestManager = new HttpRequestManager();

        private Timer timer;


        public Scheduler()
        {
            InitializeComponent();
            eventLog = new EventLog();
            if (!EventLog.SourceExists("GetRoadInfoService"))
            {
                EventLog.CreateEventSource("GetRoadInfoService", "RoadInfoService");
            }
            eventLog.Source = "GetRoadInfoService";
            eventLog.Log = "RoadInfoService";
        }

        private void Timer_Tick(object sender, ElapsedEventArgs e)
        {
            try
            {
                Run();
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
            }
        }
        protected override void OnStart(string[] args)
        {
            eventLog.WriteEntry("Service is started");

            timer = new Timer();
            this.timer.Interval = 300000; // 5 minutes
            this.timer.Elapsed += new ElapsedEventHandler(this.Timer_Tick);
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            eventLog.WriteEntry("Service is stopped");

            timer.Enabled = false;
        }

        private async void Run()
        {
            string xml = await requestManager.GetData();

            if (!xml.Equals(lastxml))
            {
                lastxml = xml;
                await postgre.InsertXmlToDb(xml);

            }
        }

        public static void WriteToErrorLog(string msg, EventLogEntryType type)
        {
            eventLog.WriteEntry(msg, type);
        }
    }
}
