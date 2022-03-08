using GetDataFromRoadInfo.DAL;
using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;

namespace GetDataFromRoadInfo
{
    public partial class Scheduler : ServiceBase
    {
        //TODO: Add logging to eventlog
        Postgres postgre = new Postgres();

        HttpRequestManager requestManager = new HttpRequestManager();

        private Timer timer;

        static EventLog log;
        public Scheduler()
        {
            InitializeComponent();
            this.AutoLog = false;
            if (!EventLog.SourceExists("GetRoadInfoService"))
            {
                EventLog.CreateEventSource("GetRoadInfoService", "RoadInfoService");
            }
            eventLog.Source = "GetRoadInfoService";
            eventLog.Log = "RoadInfoService";
            log = eventLog;

        }

        private void Timer_Tick(object sender, ElapsedEventArgs e)
        {
            log.WriteEntry("Service is ticked");

            try
            {
                Run();
            }
            catch (Exception ex)
            {
                log.WriteEntry(ex.ToString(), EventLogEntryType.Error);
            }
        }
        protected override void OnStart(string[] args)
        {
            log.WriteEntry("Service is started");

            timer = new Timer();
            //this.timer.Interval = 20000;
            this.timer.Interval = 300000; // 5 minutes
            this.timer.Elapsed += new ElapsedEventHandler(this.Timer_Tick);
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            log.WriteEntry("Service is stopped");

            timer.Enabled = false;
        }

        private async void Run()
        {
            await postgre.InsertXmlToDb(await requestManager.GetData());
        }

        public static void WriteToErrorLog(string msg, EventLogEntryType type)
        {
            log.WriteEntry(msg, type);
        }
    }
}
