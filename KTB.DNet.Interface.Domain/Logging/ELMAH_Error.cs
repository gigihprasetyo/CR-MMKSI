using KTB.DNet.Interface.Framework.CustomAttribute;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Xml.Linq;

namespace KTB.DNet.Interface.Domain
{
    [Table("ELMAH_Error")]
    public class ELMAH_Error
    {
        [Key]
        public Guid ErrorId { get; set; }
        public string Application { get; set; }
        public string Host { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string User { get; set; }
        public int StatusCode { get; set; }

        public DateTime TimeUtc { get; set; }

        public int Sequence { get; set; }

        public string AllXml { get; set; }

        [Ignore]
        [NotMapped]
        public string Verb
        {
            get
            {
                if (!string.IsNullOrEmpty(this.AllXml))
                {
                    var doc = XDocument.Parse(this.AllXml);
                    XElement el = doc.Descendants().Where(x => (string)x.Attribute("name") == "REQUEST_METHOD").SingleOrDefault();

                    return el.Elements().First().Attribute("string").Value;
                }
                return string.Empty;
            }
        }

        [Ignore]
        [NotMapped]
        public string URL
        {
            get
            {
                if (!string.IsNullOrEmpty(this.AllXml))
                {
                    var doc = XDocument.Parse(this.AllXml);
                    XElement el = doc.Descendants().Where(x => (string)x.Attribute("name") == "URL").SingleOrDefault();

                    return el.Elements().First().Attribute("string").Value;
                }
                return string.Empty;
            }
        }

        [Ignore]
        [NotMapped]
        public int Total
        {
            get;
            set;
        }
    }
}