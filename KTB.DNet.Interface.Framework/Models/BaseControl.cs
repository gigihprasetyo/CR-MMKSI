using System;

namespace KTB.DNet.Interface.Framework
{
    public class BaseControl
    {
        protected Exception GetInnerException(Exception ex)
        {
            Exception innerEx = ex;
            while (innerEx.InnerException != null)
            {
                innerEx = innerEx.InnerException;
            }

            return innerEx;
        }
    }
}
