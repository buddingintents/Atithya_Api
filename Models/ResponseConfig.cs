using System.Runtime.Serialization;

namespace Atithya_Api.Models
{
    [DataContract(Namespace = "")]
    public class ResponseConfig
    {
        [DataMember]
        //numeric code
        public string? Status { get; set; }
        [DataMember]
        //text description of numeric code
        public string? Description { get; set; }
        [DataMember]
        //0 for false and 1 for true
        public string? ResponseCompression { get; set; }
        [DataMember]
        //0 for false and 1 for true
        public string? ResponseEncryption { get; set; }
    }
}
