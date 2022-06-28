using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.SFIntegration.Helpers
{
    public class Convertions
    {
        public string ReplaceValue(IList<string> dataValue, string template)
        {
            string result = string.Empty;
            try
            {
                for (int i = 0; i < dataValue.Count(); i++)
                {
                    if (template.IndexOf(string.Format("[{0}]", (i+1)))> -1)
                    {
                        template = template.Replace(string.Format("[{0}]", (i + 1)), dataValue[i]);
                    }
                }
                result = template;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message+" | "+ex.StackTrace);
            }

            return result;
        }
    }
}
