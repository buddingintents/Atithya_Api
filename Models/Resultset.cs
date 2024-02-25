using System.Data;

namespace Atithya_Api.Models
{
    public class ResultSet<T>
    {
        public T? DtResults { get; set; }
        public string? Result { get; set; }
    }
}
