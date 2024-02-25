using Atithya_Api.Data;
using Atithya_Api.Models;
using Atithya_Api.Utilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Globalization;

namespace Atithya_Api.Helper.Subscription
{
    public class SubscriptionHelper
    {
        IConfiguration _configuration;
        private readonly EncryptionHelper EncryptionHelper;
        public SubscriptionHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            EncryptionHelper = new(_configuration.GetValue<string>("ENC:Key"), _configuration.GetValue<string>("ENC:Key"));
        }
        public APIResponse GetSubscriptionData(APIRequest data)
        {
            APIResponse response;
            try
            {
                if ((data.Content != null) && (data.RequestConfig != null))
                {
                    RequestConfig? requestConfig = JsonConvert.DeserializeObject<RequestConfig>(EncryptionHelper.DecryptData(data.RequestConfig));
                    string requestBody = new APIResponseFormat_Helper(_configuration).RetrieveRequestBody(data);

                    //ATMLogs_Request aTMLogs_Request = JsonConvert.DeserializeObject<ATMLogs_Request>(requestBody);

                    //var paramDef = new List<OracleParameter>
                    //{
                    //    new OracleParameter("SearchParam", OracleDbType.Varchar2, aTMLogs_Request.SearchParam, ParameterDirection.Input),
                    //    new OracleParameter("SearchParamData", OracleDbType.Varchar2, aTMLogs_Request.SearchParamData.Trim().ToUpper(), ParameterDirection.Input),
                    //    new OracleParameter("StartDate", OracleDbType.Varchar2, aTMLogs_Request.StartDate, ParameterDirection.Input),
                    //    new OracleParameter("EndDate", OracleDbType.Varchar2, aTMLogs_Request.EndDate, ParameterDirection.Input),
                    //    new OracleParameter("RES", OracleDbType.Varchar2, 10, null, ParameterDirection.Output),
                    //    //new OracleParameter("STATUS", OracleDbType.Varchar2, 500, null, ParameterDirection.Output),
                    //    new OracleParameter("p_recordset", OracleDbType.RefCursor, ParameterDirection.Output)
                    //};

                    var rs = new ResultSet<DataTable>();
                    //if (Convert.ToDateTime(DateTime.ParseExact(aTMLogs_Request.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)) < DateTime.Now.Date)
                    //    rs = new GenericDAL<DataTable>().ExecuteProcedure(Available_DbSchema.ATMWEB_APP, paramDef, "ia_ATM_TXN_LOGS_TMP");
                    //else
                    //    rs = new GenericDAL<DataTable>().ExecuteProcedure(Available_DbSchema.IA_GGS, paramDef, "PKG_IA_LOGS.ia_ATM_TXN_LOGS_TMP");

                    response = new APIResponseFormat_Helper(_configuration).FormatResponse(rs, requestConfig);
                }
                else
                    response = new APIResponseFormat_Helper(_configuration).BadRequestResponse(null);
                return response;
            }
            catch (Exception ex)
            {
                return new APIResponseFormat_Helper(_configuration).BadRequestResponse(ex);
            }

        }
    }
}
