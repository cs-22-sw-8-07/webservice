using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using quack_api.Enums;
using quack_api.Models;
using quack_api.Objects;
using quack_api.Utilities;

namespace Quack.Utilities
{
    public class RecommenderServiceUtil
    {
        public static async Task<TypeDataResponse> GetResponse<TypeDataResponse>
            (
                Func<Task<TypeDataResponse>> getDataResponseMethod
            )
            where TypeDataResponse : ServiceResponse
        {
            // Nested function used for returning error response object
            TypeDataResponse getErrorResponse(int errorNo, string errorMessage)
            {
                Type type = typeof(TypeDataResponse);
                ConstructorInfo ctor = type.GetConstructor(new[] { typeof(int), typeof(string) });
                object instance = ctor.Invoke(new object[] { errorNo, errorMessage });
                return (TypeDataResponse)instance;
            }

            try
            {
                return await getDataResponseMethod();
            }
            catch (PathNullException exc)
            {
                return getErrorResponse((int)ResponseErrors.PathNull, exc.Message);
            }
            catch (PathNotFoundException exc)
            {
                return getErrorResponse((int)ResponseErrors.PathNotFound, exc.Message);
            }
            catch (ProcessCouldNotStartException exc)
            {
                return getErrorResponse((int)ResponseErrors.ProcessCouldNotStart, exc.Message);
            }
            catch (Exception exc)
            {
                // Return exception
                return getErrorResponse(((int)ResponseErrors.AnExceptionOccurredInTheSAL), exc.Message);
            }
        }
    }
}
