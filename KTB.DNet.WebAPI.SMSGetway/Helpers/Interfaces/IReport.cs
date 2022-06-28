using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.WebAPI.SMSGetway.Helpers.Interfaces
{
    public interface IReport<T> where T : class
    {
        void SetReport();
        T Result { get; }
    }
}
