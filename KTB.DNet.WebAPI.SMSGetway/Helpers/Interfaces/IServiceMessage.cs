using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.WebAPI.SMSGetway.Helpers.Interfaces
{
    public interface IServiceMessage<T> where T : class
    {
        void SendMessage();
        T GetResponse { get; }
    }
}
