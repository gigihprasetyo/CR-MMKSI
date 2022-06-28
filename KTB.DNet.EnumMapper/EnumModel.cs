using System.Collections.Generic;

namespace KTB.DNet.ConsoleApp
{
    /// <summary>
    /// Enum model class 
    /// </summary>
    public class EnumModel
    {
        public string Name { get; set; }
        public Dictionary<string, object> Values { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        public EnumModel(string name)
        {
            Name = name;
            Values = new Dictionary<string, object>();
        }
    }
}
