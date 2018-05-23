using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;

namespace imgslkhook
{
    public static class Functions
    {
        [FunctionName(nameof(ReceiveWebhook))]
        public static HttpResponseMessage ReceiveWebhook(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "slack/po")]HttpRequestMessage req,
            ILogger logger)
        {
            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}
