using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.SFIntegration.Model
{
    public class ParamUpdateCase
    {
        public ParamUpdateCase() { }
        public const String SObjectTypeName = "paramUpdateCase";

        public String id { get; set; }
        public String status { get; set; }
        public String Status_By_Dealer__c { get; set; }
        public String comment { get; set; }
    }
}
