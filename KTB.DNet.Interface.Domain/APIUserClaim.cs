

namespace KTB.DNet.Interface.Domain
{
    public class APIUserClaim
    {
        public string ClaimType { get; set; }
        
        public string ClaimValue { get; set; }
        
        public int Id { get; set; }
        
        public int UserId { get; set; }
    }
}
