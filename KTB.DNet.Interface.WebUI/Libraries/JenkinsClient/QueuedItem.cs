using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KTB.DNet.Interface.WebUI.Libraries;

namespace JenkinsClient
{
    public class QueuedItem
    {
        public int id
        {
            get
            {
                return (int)data[CustomExtension.nameof(() => id)];
            }
        }

        public string why
        {
            get
            {
                return (string)data[CustomExtension.nameof(() => why)];
            }
        }
        public int? buildNumber
        {
            get
            {
                JToken executable = null;
                if (!data.TryGetValue("executable", out executable))
                    return null;

                return (int)executable["number"];
            }
        }

        private JObject data { get; set; }

        internal QueuedItem(string json)
        {
            this.data = JObject.Parse(json);
        }
    }
}
