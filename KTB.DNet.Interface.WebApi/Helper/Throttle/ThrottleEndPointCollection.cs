using System.Configuration;

namespace KTB.DNet.Interface.WebApi.Helper.Throttle
{
    /// <summary>
    /// Throttle end point collection
    /// </summary>
    [ConfigurationCollection(typeof(ThrottleEndpoints))]
    public class ThrottleEndPointCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Override create cofiguration element
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ThrottleEndpoints();
        }

        /// <summary>
        /// Get configuration element
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ThrottleEndpoints)element).Id;
        }
    }
}