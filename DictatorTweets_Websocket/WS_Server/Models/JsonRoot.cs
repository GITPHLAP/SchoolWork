using System.Collections.Generic;

namespace WS_Server.Models
{
    public class JsonRoot
    {
        public List<Data120161205TrumpTwitterAll> Data120161205TrumpTwitterAll { get; set; }
    }

    public class Data120161205TrumpTwitterAll
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string Tweet { get; set; }
        public string Client { get; set; }
        public string ClientSimplified { get; set; }
    }

}
