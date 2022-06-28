using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Framework
{
    public class TransactionDataTablePostModel : DataTablePostModel
    {
        public Guid appId { get; set; }
        public Guid clientId { get; set; }
        public string dealerCode { get; set; }
        public Dictionary<object, object> searchParams { get; set; }
    }
}
