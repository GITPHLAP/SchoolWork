//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AirportDBApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class Route
    {
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public string ICAO { get; set; }
    
        public virtual Airline Airline_ICAO { get; set; }
        public virtual Airport Airport_Arrival { get; set; }
        public virtual Airport Airport_Departure { get; set; }
    }
}
