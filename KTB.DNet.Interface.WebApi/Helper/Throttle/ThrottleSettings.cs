using System.Configuration;

namespace KTB.DNet.Interface.WebApi.Helper.Throttle
{
    /// <summary>
    /// Configure Throttle Settings based on configuration file
    /// </summary>
    public class ThrottleSettings : ConfigurationSection
    {
        /// <summary>
        /// Get ThrottleSettings from based on configuration file
        /// </summary>
        [ConfigurationProperty("endPoints", IsDefaultCollection = true)]
        public ThrottleEndPointCollection EndPoints
        {
            get { return (ThrottleEndPointCollection)this["endPoints"]; }
        }
    }
}