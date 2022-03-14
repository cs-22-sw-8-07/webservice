using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quack_api.Objects
{
    public class QuackResponse
    {
        public bool IsSuccessful { get => ErrorNo == 0; }
        public int ErrorNo { get; set; }
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
