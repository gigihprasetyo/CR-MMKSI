
namespace KTB.DNet.Interface.Framework.Models
{
    public interface IThrottleInfo
    {
        int Id { get; set; }
        int RequestLimit { get; set; }
        int TimeInSeconds { get; set; }
        bool Enable { get; set; }

        string GetURI();
    }
}
