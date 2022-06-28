using KTB.DNet.Interface.WebUI.Libraries;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JenkinsClient
{
    public class Job : LazyJObject
    {
        internal Client client { get; private set; }

        public string name { get; private set; }
        public string description
        {
            get
            {
                EnsureDataInLocal();

                return (string)data[CustomExtension.nameof(() => description)];
            }
        }
        public string displayName
        {
            get
            {
                EnsureDataInLocal();

                return (string)data[CustomExtension.nameof(() => displayName)];
            }
        }
        public int nextBuildNumber
        {
            get
            {
                EnsureDataInLocal();

                return (int)data[CustomExtension.nameof(() => nextBuildNumber)];
            }
        }
        public bool buildable
        {
            get
            {
                EnsureDataInLocal();

                return (bool)data[CustomExtension.nameof(() => buildable)];
            }
        }
        public string url
        {
            get
            {
                EnsureDataInLocal();

                return (string)data[CustomExtension.nameof(() => url)];
            }
        }
        public string color
        {
            get
            {
                EnsureDataInLocal();

                return (string)data[CustomExtension.nameof(() => color)];
            }
        }
        public List<BuildParameter> parameters
        {
            get
            {
                EnsureDataInLocal();

                var actions = (JArray)data["actions"];
                var result = new List<BuildParameter>();
                JToken trash = null;

                foreach (var _action in actions)
                {
                    var action = (JObject)_action;

                    if (action.TryGetValue("parameterDefinitions", out trash))
                    {
                        var pary = (JArray)action["parameterDefinitions"];

                        foreach (var param in pary)
                        {
                            result.Add(new BuildParameter((JObject)param));
                        }

                        return result;
                    }
                }

                return result;
            }
        }

        #region LAST_BUILDS
        /// <summary>
        /// This can be null if there's no build history
        /// </summary>
        public Build lastBuild
        {
            get
            {
                EnsureDataInLocal();

                return GetBuild(CustomExtension.nameof(() => lastBuild));
            }
        }
        /// <summary>
        /// This can be null if there's no build history
        /// </summary>
        public Build lastCompletedBuild
        {
            get
            {
                EnsureDataInLocal();

                return GetBuild(CustomExtension.nameof(() => lastCompletedBuild));
            }
        }
        /// <summary>
        /// This can be null if there's no build history
        /// </summary>
        public Build lastFailedBuild
        {
            get
            {
                EnsureDataInLocal();

                return GetBuild(CustomExtension.nameof(() => lastFailedBuild));
            }
        }
        /// <summary>
        /// This can be null if there's no build history
        /// </summary>
        public Build lastSuccessfulBuild
        {
            get
            {
                EnsureDataInLocal();

                return GetBuild(CustomExtension.nameof(() => lastSuccessfulBuild));
            }
        }
        /// <summary>
        /// This can be null if there's no build history
        /// </summary>
        public Build lastStableBuild
        {
            get
            {
                EnsureDataInLocal();

                return GetBuild(CustomExtension.nameof(() => lastStableBuild));
            }
        }
        /// <summary>
        /// This can be null if there's no build history
        /// </summary>
        public Build lastUnstableBuild
        {
            get
            {
                EnsureDataInLocal();

                return GetBuild(CustomExtension.nameof(() => lastUnstableBuild));
            }
        }
        #endregion

        public Job(Client client, string name)
        {
            this.client = client;
            this.name = name;
        }

        protected override async Task<JObject> Fetch()
        {
            var response = await client.api.GetJobData(name);

            return JObject.Parse(response.body);
        }

        public async Task<QueuedBuild> BuildAsync(Dictionary<string, string> param = null)
        {
            RestResponse response = null;

            if (param == null)
                response = await client.api.PostBuild(name);
            else
            {
                response = await client.api.PostBuildWithParameters(
                    name,
                    param ?? new Dictionary<string, string>());
            }

            if (response.code == System.Net.HttpStatusCode.Created)
            {
                var md = Regex.Match(
                    response.headers.Location.PathAndQuery,
                    "item/([0-9]+)");
                var itemId = int.Parse(md.Groups[1].Value);

                return new QueuedBuild(itemId, this);
            }
            else
            {
                throw new OperationFailedException(
                    "BuildAsync Failed, StatusCode : {response.code}",
                    response.body);
            }
        }

        public async Task<string> GetConsoleLog(string buildNo)
        {
            RestResponse response = null;
                response = await client.api.GetConsoleOutput(name, buildNo);
         

            if (response.code == System.Net.HttpStatusCode.OK)
            {
                return response.body;
            }
            else
            {
                throw new OperationFailedException(
                    "BuildAsync Failed, StatusCode : {response.code}",
                    response.body);
            }
        }
        private Build GetBuild(string tag)
        {
            var build = data[tag];
            if (!build.HasValues)
                return null;

            return new Build(this,
                (int)build["number"],
                (string)build["url"]);
        }
    }
}
