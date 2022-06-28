using System;

namespace KTB.DNet.Interface.Framework.CustomAttribute
{
    public class SearchAble : Attribute
    {
        public readonly bool Value;

        public SearchAble(bool val)
        {
            this.Value = val;
        }
    }

    public class Ignore : Attribute
    {
        public readonly string Value;

        public Ignore()
        {
            this.Value = string.Empty;
        }
    }
}
