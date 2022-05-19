using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace quack_api.Objects
{
    public class ServiceResponse
    {
        [JsonPropertyName("is_successful")]
        public bool IsSuccessful => ErrorNo == 0;
        [JsonPropertyName("error_no")]
        public int ErrorNo { get; set; }
        [JsonPropertyName("error_message")]
        public string ErrorMessage { get; set; }

        public ServiceResponse()
        {
            ErrorNo = 0;
        }

        public ServiceResponse(int errorNo, string errorMessage = null)
        {
            ErrorNo = errorNo;
            ErrorMessage = errorMessage;
        }
    }

    public class ServiceResponse<ResultType> : ServiceResponse
    {
        [JsonPropertyName("result")]
        public ResultType Result { get; set; }

        public ServiceResponse()
        {

        }

        public ServiceResponse(ResultType result)
        {
            ErrorNo = 0;
            Result = result;
        }

        public ServiceResponse(int errorNo, string errorMessage = null) : base(errorNo, errorMessage)
        {
            Result = default;
        }
    }
}
