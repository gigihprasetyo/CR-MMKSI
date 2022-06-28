using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTB.DNet.Utility;
using System.IO;

namespace KTB.DNet.BusinessValidation
{
    public class SAPHelper
    {
        private readonly string _user;
        private readonly string _password;
        private readonly string _webServer;

        public SAPHelper(string user, string password, string webServer)
        {
            _user = user;
            _password = password;
            _webServer = webServer;
        }

        public void SentFile(string destFile, StringBuilder str)
        {
            SAPImpersonate imp = new SAPImpersonate(_user, _password, _webServer);

            try
            {
                // start it
                bool success = imp.Start();
                if (success)
                    File.WriteAllText(destFile, str.ToString());
                else
                    throw new Exception("Error saat mengakses directory. Harap hubungi administrator");
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                // stop 
                imp.StopImpersonate();
            }
        }
    }
}
