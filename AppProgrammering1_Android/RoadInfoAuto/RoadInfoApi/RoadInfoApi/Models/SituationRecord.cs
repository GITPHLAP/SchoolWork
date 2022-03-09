using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadInfoApi.Models
{
    public class SituationRecord
    {
        private string id;
        private Int64 version;
        private string situationId;
        private DateTime? situationrecordcreationtime;
        private DateTime? situationrecordversiontime;
        private string validitystatus;
        private DateTime? startofperiod;
        private DateTime? endofperiod;
        private string generalcomment_description;
        private string generalcomment_locationdescriptor;
        private string generalcomment_other;
        private double? latitude;
        private double? longitude;
        private int? temporaryspeedlimit;
        private string carparkidentity;
        private string carparkstatus;
        private string publiceventtype;
        private string type;

        public SituationRecord(string id,
            Int64 version,
            string situationId,
            DateTime? situationrecordcreationtime,
            DateTime? situationrecordversiontime,
            string validitystatus,
            DateTime? startofperiod,
            DateTime? endofperiod,
            string generalcomment_description,
            string generalcomment_locationdescriptor,
            string generalcomment_other,
            double? latitude,
            double? longitude,
            int? temporaryspeedlimit,
            string carparkidentity,
            string carparkstatus,
            string publiceventtype,
            string type)
        {
            this.id = id;
            this.version = version;
            this.situationId = situationId;
            this.situationrecordcreationtime = situationrecordcreationtime;
            this.situationrecordversiontime = situationrecordversiontime;
            this.validitystatus = validitystatus;
            this.startofperiod = startofperiod;
            this.endofperiod = endofperiod;
            this.generalcomment_description = generalcomment_description;
            this.generalcomment_locationdescriptor = generalcomment_locationdescriptor;
            this.generalcomment_other = generalcomment_other;
            this.latitude = latitude;
            this.longitude = longitude;
            this.temporaryspeedlimit = temporaryspeedlimit;
            this.carparkidentity = carparkidentity;
            this.carparkstatus = carparkstatus;
            this.publiceventtype = publiceventtype;
            this.type = type;
        }

        public string Id { get => id; set => id = value; }
        public Int64 Version { get => version; set => version = value; }
        public string SituationId { get => situationId; set => situationId = value; }
        public DateTime? Situationrecordcreationtime { get => situationrecordcreationtime; set => situationrecordcreationtime = value; }
        public DateTime? Situationrecordversiontime { get => situationrecordversiontime; set => situationrecordversiontime = value; }
        public string Validitystatus { get => validitystatus; set => validitystatus = value; }
        public DateTime? Startofperiod { get => startofperiod; set => startofperiod = value; }
        public DateTime? Endofperiod { get => endofperiod; set => endofperiod = value; }
        public string Generalcomment_description { get => generalcomment_description; set => generalcomment_description = value; }
        public string Generalcomment_locationdescriptor { get => generalcomment_locationdescriptor; set => generalcomment_locationdescriptor = value; }
        public string Generalcomment_other { get => generalcomment_other; set => generalcomment_other = value; }
        public double? Latitude { get => latitude; set => latitude = value; }
        public double? Longitude { get => longitude; set => longitude = value; }
        public int? Temporaryspeedlimit { get => temporaryspeedlimit; set => temporaryspeedlimit = value; }
        public string Carparkidentity { get => carparkidentity; set => carparkidentity = value; }
        public string Carparkstatus { get => carparkstatus; set => carparkstatus = value; }
        public string Publiceventtype { get => publiceventtype; set => publiceventtype = value; }
        public string Type { get => type; set => type = value; }
    }
}
