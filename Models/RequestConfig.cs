using System.Runtime.Serialization;

namespace Atithya_Api.Models
{
    [DataContract(Namespace = "")]
    public class RequestConfig
    {
        [DataMember]
        //0 for false and 1 for true
        public string? RequestCompression { get; set; }
        [DataMember]
        //0 for false and 1 for true
        public string? RequestEncryption { get; set; }
        [DataMember]
        //0 for false and 1 for true
        public string? ResponseCompression { get; set; }
        [DataMember]
        //0 for false and 1 for true
        public string? ResponseEncryption { get; set; }
    }
}
