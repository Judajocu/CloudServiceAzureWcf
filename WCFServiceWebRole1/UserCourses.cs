using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WCFServiceWebRole1
{
    public class UserCourses
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("shortname")]
        public string shortname { get; set; }
        [JsonProperty("fullname")]
        public string fullname { get; set; }
        [JsonProperty("displayname")]
        public string displayname { get; set; }
        [JsonProperty("enrolledusercount")]
        public int enrolledusercount { get; set; }
        [JsonProperty("idnumber")]
        public string idnumber { get; set; }
        [JsonProperty("visible")]
        public int visible { get; set; }
        [JsonProperty("summary")]
        public string summary { get; set; }
        [JsonProperty("summaryformat")]
        public int summaryformat { get; set; }
        [JsonProperty("format")]
        public string format { get; set; }
        [JsonProperty("showgrades")]
        public bool showgrades { get; set; }
        [JsonProperty("lang")]
        public string lang { get; set; }
        [JsonProperty("enablecompletion")]
        public bool enablecompletion { get; set; }
        [JsonProperty("completionhascriteria")]
        public bool completionhascriteria { get; set; }
        [JsonProperty("category")]
        public int category { get; set; }
        [JsonProperty("progress")]
        public double? progress { get; set; }
        [JsonProperty("completed")]
        public bool? completed { get; set; }
        [JsonProperty("startdate")]
        public int startdate { get; set; }
        [JsonProperty("enddate")]
        public int enddate { get; set; }
        [JsonProperty("marker")]
        public int marker { get; set; }
        [JsonProperty("lastaccess")]
        public int? lastaccess { get; set; }
        [JsonProperty("isfavourite")]
        public bool isfavourite { get; set; }
        [JsonProperty("hidden")]
        public bool hidden { get; set; }
        [JsonProperty("overviewfiles")]
        public List<Overviewfile> overviewfiles { get; set; }
    }

    public class Overviewfile
    {
        [JsonProperty("filename")]
        public string filename { get; set; }
        [JsonProperty("filepath")]
        public string filepath { get; set; }
        [JsonProperty("filesize")]
        public int filesize { get; set; }
        [JsonProperty("fileurl")]
        public string fileurl { get; set; }
        [JsonProperty("timemodified")]
        public int timemodified { get; set; }
        [JsonProperty("mimetype")]
        public string mimetype { get; set; }
    }
}