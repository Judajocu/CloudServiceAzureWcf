using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WCFServiceWebRole1
{
    public class User
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("username")]
        public string username { get; set; }
        [JsonProperty("firstname")]
        public string firstname { get; set; }
        [JsonProperty("lastname")]
        public string lastname { get; set; }
        [JsonProperty("fullname")]
        public string fullname { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }
        [JsonProperty("department")]
        public string department { get; set; }
        [JsonProperty("firstaccess")]
        public int firstaccess { get; set; }
        [JsonProperty("lastaccess")]
        public int lastaccess { get; set; }
        [JsonProperty("auth")]
        public string auth { get; set; }
        [JsonProperty("suspended")]
        public bool suspended { get; set; }
        [JsonProperty("confirmed")]
        public bool confirmed { get; set; }
        [JsonProperty("lang")]
        public string lang { get; set; }
        [JsonProperty("theme")]
        public string theme { get; set; }
        [JsonProperty("timezone")]
        public string timezone { get; set; }
        [JsonProperty("mailformat")]
        public int mailformat { get; set; }
        [JsonProperty("city")]
        public string city { get; set; }
        [JsonProperty("country")]
        public string country { get; set; }
        [JsonProperty("profileimageurlsmall")]
        public string profileimageurlsmall { get; set; }
        [JsonProperty("profileimageurl")]
        public string profileimageurl { get; set; }
    }

    public class RootObject
    {
        public List<User> users { get; set; }
        public List<object> warnings { get; set; }
    }
}