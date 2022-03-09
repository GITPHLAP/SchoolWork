using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadInfoApi.Models
{
    public class Situation
    {
        private string id;
        private Int64 version;
        private DateTime situationversiontime;
        private string locationtext;
        private string eventtext;
        private string regiontext;
        private string noncodeableinformationtext;
        private string durationtext;
        private string icon;
        private string status;
        private List<SituationRecord> situationRecords;

        public Situation(string id, Int64 version, DateTime situationversiontime,
            string locationtext, string eventtext, string regiontext,
            string noncodeableinformationtext, string durationtext,
            string icon, string status)
        {
            this.id = id;
            this.version = version;
            this.situationversiontime = situationversiontime;
            this.locationtext = locationtext;
            this.eventtext = eventtext;
            this.regiontext = regiontext;
            this.noncodeableinformationtext = noncodeableinformationtext;
            this.durationtext = durationtext;
            this.icon = icon;
            this.status = status;
        }

        public string Status { get => status; set => status = value; }
        public string Icon { get => icon; set => icon = value; }
        public string Durationtext { get => durationtext; set => durationtext = value; }
        public string Noncodeableinformationtext { get => noncodeableinformationtext; set => noncodeableinformationtext = value; }
        public string Regiontext { get => regiontext; set => regiontext = value; }
        public string Eventtext { get => eventtext; set => eventtext = value; }
        public string Locationtext { get => locationtext; set => locationtext = value; }
        public DateTime Situationversiontime { get => situationversiontime; set => situationversiontime = value; }
        public Int64 Version { get => version; set => version = value; }
        public string Id { get => id; set => id = value; }
        public List<SituationRecord> SituationRecords { get => situationRecords; set => situationRecords = value; }
    }
}
