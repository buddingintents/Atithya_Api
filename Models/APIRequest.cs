using System.Runtime.Serialization;

namespace Atithya_Api.Models
{
    [DataContract(Namespace = "")]
    public class APIRequest
    {
        [DataMember]
        public string? RequestConfig { get; set; }
        //serialised data
        [DataMember]
        public string? Content { get; set; }
    }
}
