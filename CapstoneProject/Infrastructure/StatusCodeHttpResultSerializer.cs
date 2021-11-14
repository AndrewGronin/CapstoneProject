using System.Linq;
using System.Net;
using HotChocolate.AspNetCore.Serialization;
using HotChocolate.Execution;

namespace CapstoneProject.Infrastructure
{
    public class StatusCodeHttpResultSerializer : DefaultHttpResultSerializer
    {
        public override HttpStatusCode GetStatusCode(IExecutionResult result)
        {
            if (result is IQueryResult queryResult &&
                queryResult.Errors?.Count > 0 &&
                queryResult.Errors.All(error => error.Code != "AUTH_NOT_AUTHENTICATED"))
            {
                return HttpStatusCode.OK;
            }

            return base.GetStatusCode(result);
        }
    }
}