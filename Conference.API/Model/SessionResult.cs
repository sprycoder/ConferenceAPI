using System.Collections.Generic;

namespace Conference.API.Model
{
    public class SessionResult
    {
        public int SessionID { get; set; }

        public string Title { get; set; }

        public string Desciption { get; set; }

        public string Href { get; set; }

        public string TimeSlot { get; set; }

        public string Speaker { get; set; }

        public List<LinkResult> Links { get; set; }
    }
}
