using System.Threading.Tasks;

namespace JenkinsClient
{
    public class ConsoleOutput
    {
        private Client client { get; set; }

        internal ConsoleOutput(Client client)
        {
            this.client = client;
        }

        public async Task<string> GetConsoleOutput(string jobName, string buildNo)
        {
            var response = await client.api.GetConsoleOutput(jobName, buildNo);

            return response.body;
        }
    }
}
