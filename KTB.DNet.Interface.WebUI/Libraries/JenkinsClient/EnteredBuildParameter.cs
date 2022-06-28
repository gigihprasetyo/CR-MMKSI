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
    public class EnteredBuildParameter
    {
        protected JObject data { get; private set; }

        public object value
        {
            get
            {
                return data["value"];
            }
        }
        public string name
        {
            get
            {
                return (string)data[CustomExtension.nameof(() => name)];
            }
        }

        internal EnteredBuildParameter(JObject data)
        {
            this.data = data;
        }
    }
}
