
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KTB.DNet.Interface.Domain
{
    [Table("APIRole")]
    public class APIRole : BaseDomain
    {
        public APIRole()
        { }

        public APIRole(string name)
        {
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<APIUserRole> Users { get; set; }

        public string Level { get; set; }
    }
}
