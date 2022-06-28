
namespace KTB.DNet.Interface.Framework
{
    public class TopApiModel
    {
        public TopApiModel(int no, string endpoint, int hitcount)
        {
            No = no;
            EndPoint = endpoint;
            HitCount = hitcount;
        }

        public int No { get; set; }
        public string EndPoint { get; set; }
        public int HitCount { get; set; }
    }
}
