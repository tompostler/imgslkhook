using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace imgslkhook
{
    public static class Functions
    {
        [FunctionName(nameof(ReceiveWebhookAsync))]
        public static async Task<HttpResponseMessage> ReceiveWebhookAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "slack/po")]HttpRequestMessage req,
            ILogger logger)
        {
            logger.LogInformation("BODY: {0}", await req.Content.ReadAsStringAsync());
            return req.CreateResponse(HttpStatusCode.OK, @"{""text"":""got your message. development in progress""}");
        }
    }
}
