using Atithya_Api.Models;
using Atithya_Api.Utilities;
using Newtonsoft.Json;
using System.Data;
using System.Net;

namespace Atithya_Api.Helper
{
    public class APIResponseFormat_Helper
    {
        EncryptionHelper _encryptionHelper;
        public APIResponseFormat_Helper(IConfiguration configuration)
        {
            _encryptionHelper = new EncryptionHelper(configuration);
        }
        public string RetrieveRequestBody(APIRequest data)
        {
            RequestConfig? requestConfig = JsonConvert.DeserializeObject<RequestConfig>(_encryptionHelper.DecryptData(data.RequestConfig));
            string requestBody = string.Empty;

            if (requestConfig == null)
                return string.Empty;
            if (requestConfig.RequestEncryption == "1")
                requestBody = _encryptionHelper.DecryptData(data.Content);
            if (requestConfig.RequestCompression == "1")
                requestBody = new CompressionHelper().DecompressString(data.Content);
            return requestBody;
        }
        public APIResponse FormatResponse(ResultSet<DataTable> rs, RequestConfig? requestConfig)
        {
            var response = new APIResponse();
            if (rs.Result != null && rs.Result.ToUpper() == "Y")
            {
                if (rs.DtResults != null && rs.DtResults.Columns.Count > 0)
                {
                    if (rs.DtResults.Rows.Count == 0)
                    {
                        DataRow dr = rs.DtResults.NewRow();
                        rs.DtResults.Rows.Add(dr);
                        rs.DtResults.AcceptChanges();
                    }
                    string responseBody = JsonConvert.SerializeObject(rs.DtResults);
                    if (requestConfig.ResponseCompression == "1")
                        responseBody = new CompressionHelper().CompressString(responseBody);
                    if (requestConfig.ResponseEncryption == "1")
                        responseBody = _encryptionHelper.EncryptData(responseBody);

                    response.ResponseConfig = new CompressionHelper().CompressString(JsonConvert.SerializeObject(new ResponseConfig
                    {
                        ResponseEncryption = requestConfig.ResponseEncryption,
                        ResponseCompression = requestConfig.ResponseCompression,
                        Status = ((int)HttpStatusCode.OK).ToString(),
                        Description = HttpStatusCode.OK.ToString()
                    }));

                    response.Content = responseBody;
                }
                else
                {
                    response.ResponseConfig = new CompressionHelper().CompressString(JsonConvert.SerializeObject(new ResponseConfig
                    {
                        ResponseEncryption = "0",
                        ResponseCompression = "1",
                        Status = ((int)HttpStatusCode.NoContent).ToString(),
                        Description = HttpStatusCode.NoContent.ToString()
                    }));
                    response.Content = new CompressionHelper().CompressString("No Data Found");
                }
            }
            else
            {
                response.ResponseConfig = new CompressionHelper().CompressString(JsonConvert.SerializeObject(new ResponseConfig
                {
                    ResponseEncryption = "0",
                    ResponseCompression = "1",
                    Status = ((int)HttpStatusCode.BadRequest).ToString(),
                    Description = HttpStatusCode.BadRequest.ToString()
                }));

                response.Content = new CompressionHelper().CompressString(rs.Result);
            }
            return response;
        }
        public APIResponse BadRequestResponse(Exception? ex)
        {
            return new APIResponse
            {
                ResponseConfig = new CompressionHelper().CompressString(JsonConvert.SerializeObject(new ResponseConfig
                {
                    ResponseEncryption = "0",
                    ResponseCompression = "1",
                    Status = ((int)HttpStatusCode.BadRequest).ToString(),
                    Description = HttpStatusCode.BadRequest.ToString()
                })),
                Content = new CompressionHelper().CompressString(ex == null ? "Bad Request" : ex.ToString())
            };
        }
    }
}
