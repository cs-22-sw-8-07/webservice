using quack_api.Enums;
using quack_api.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace quack_api.Utilities
{
    public class ControllerUtil
    {
        /// <summary>
        /// Method used to get WASPResponse object
        /// </summary>
        /// <typeparam name="TypeDataResponse">DataResponse type</typeparam>
        /// <typeparam name="TypeQuackResponse">QuackResponse type</typeparam>
        /// <param name="getDataResponseMethod">Method returning DataResponse object</param>
        /// <param name="parseDataResponseMethod">Method returning QuackResponse object</param>
        /// <returns></returns>
        public static async Task<TypeQuackResponse> GetResponse<TypeDataResponse, TypeQuackResponse>
            (
                Func<Task<TypeDataResponse>> getDataResponseMethod, 
                Func<TypeDataResponse, TypeQuackResponse> parseDataResponseMethod
            )
            where TypeDataResponse : DataResponse
            where TypeQuackResponse : QuackResponse
        {
            TypeQuackResponse getErrorResponse(int errorNo, string errorMessage)
            {
                Type type = typeof(TypeQuackResponse);
                ConstructorInfo ctor = type.GetConstructor(new[] { typeof(int), typeof(string) });
                object instance = ctor.Invoke(new object[] { errorNo, errorMessage });
                return (TypeQuackResponse)instance;
            }

            try
            {
                // Get data
                var dataResponse = await getDataResponseMethod();
                // Check for errors
                if (!dataResponse.IsSuccessful)
                    return getErrorResponse(dataResponse.ErrorNo, dataResponse.ErrorMessage);
                // Return response
                return parseDataResponseMethod(dataResponse);
            }
            catch (Exception exc)
            {
                return getErrorResponse((int)ResponseErrors.AnExceptionOccurredInAController, exc.Message);
            }
        }
    }
}
