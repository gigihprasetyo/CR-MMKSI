using System.Configuration;

namespace KTB.DNet.Interface.WebApi.Helper.Throttle
{
    /// <summary>
    /// Throttle endpoint configuration map from config file
    /// </summary>
    public class ThrottleEndpoints : ConfigurationElement
    {
        /// <summary>
        /// Identity
        /// </summary>
        [ConfigurationProperty("Id", IsKey = true, DefaultValue = 1, IsRequired= true)]
        public int Id
        {
            get
            {
                return (int)this["Id"];
            }
        }

        /// <summary>
        /// Endpoint
        /// </summary>
        [ConfigurationProperty("EndPoint", DefaultValue = "", IsRequired = true)]
        public string EndPoint
        {
            get
            {
                return (string)this["EndPoint"];
            }
        }

        /// <summary>
        /// Request Limit
        /// </summary>
        [ConfigurationProperty("RequestLimit", DefaultValue = 0, IsRequired = true)]
        public int RequestLimit
        {
            get
            {
                return (int)this["RequestLimit"];
            }
        }

        /// <summary>
        /// Request in seconds
        /// </summary>
        [ConfigurationProperty("RequestInSeconds", DefaultValue = 0, IsRequired = true)]
        public int RequestInSeconds
        {
            get
            {
                return (int)this["RequestInSeconds"];
            }
        }

        /// <summary>
        /// Enable throttle
        /// </summary>
        [ConfigurationProperty("Enable", DefaultValue = true, IsRequired = true)]
        public bool Enable
        {
            get
            {
                return (bool)this["Enable"];
            }
        }
    }
}