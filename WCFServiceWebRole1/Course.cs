using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WCFServiceWebRole1
{
    public class Course
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("visible")]
        public long Visible { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("summaryformat")]
        public long Summaryformat { get; set; }

        [JsonProperty("section")]
        public long Section { get; set; }

        [JsonProperty("hiddenbynumsections")]
        public long Hiddenbynumsections { get; set; }

        [JsonProperty("uservisible")]
        public bool Uservisible { get; set; }

        [JsonProperty("modules")]
        public Module[] Modules { get; set; }
    }

    public partial class Module
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("instance")]
        public long Instance { get; set; }

        [JsonProperty("visible")]
        public long Visible { get; set; }

        [JsonProperty("uservisible")]
        public bool Uservisible { get; set; }

        [JsonProperty("visibleoncoursepage")]
        public long Visibleoncoursepage { get; set; }

        [JsonProperty("modicon")]
        public Uri Modicon { get; set; }

        [JsonProperty("modname")]
        public string Modname { get; set; }

        [JsonProperty("modplural")]
        public string Modplural { get; set; }

        [JsonProperty("indent")]
        public long Indent { get; set; }

        [JsonProperty("onclick")]
        public string Onclick { get; set; }

        [JsonProperty("afterlink")]
        public object Afterlink { get; set; }


        [JsonProperty("completion")]
        public long Completion { get; set; }

        [JsonProperty("contents", NullValueHandling = NullValueHandling.Ignore)]
        public Content[] Contents { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
    }

    public partial class Content
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("filepath")]
        public string Filepath { get; set; }

        [JsonProperty("filesize")]
        public long Filesize { get; set; }

        [JsonProperty("fileurl")]
        public Uri Fileurl { get; set; }

        [JsonProperty("timecreated")]
        public long? Timecreated { get; set; }

        [JsonProperty("timemodified")]
        public long Timemodified { get; set; }

        [JsonProperty("sortorder")]
        public long? Sortorder { get; set; }

        [JsonProperty("mimetype", NullValueHandling = NullValueHandling.Ignore)]
        public string Mimetype { get; set; }

        [JsonProperty("isexternalfile", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Isexternalfile { get; set; }

        [JsonProperty("userid")]
        public long? Userid { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("license")]
        public string License { get; set; }
    }
}