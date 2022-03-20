using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace quack_api.Objects
{
    public class QuackResponse
    {
        [JsonPropertyName("is_successful")]
        public bool IsSuccessful { get => ErrorNo == 0; }
        [JsonPropertyName("error_no")]
        public int ErrorNo { get; set; }
        [JsonPropertyName("error_message")]
        public string ErrorMessage { get; set; }

        public QuackResponse()
        {
            ErrorNo = 0;
        }

        public QuackResponse(int errorNo, string errorMessage = null)
        {
            ErrorNo = errorNo;
            ErrorMessage = errorMessage;
        }
    }

    public class QuackResponse<ResultType> : QuackResponse
    {
        [JsonPropertyName("result")]
        public ResultType Result { get; set; }

        public QuackResponse(ResultType result)
        {
            ErrorNo = 0;
            Result = result;
        }

        public QuackResponse(int errorNo, string errorMessage = null) : base (errorNo, errorMessage)
        {
            Result = default;
        }
    }
}
