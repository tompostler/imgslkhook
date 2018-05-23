using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
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
            logger.LogInformation("BODY: {0}", Convert.ToBase64String(Encoding.UTF8.GetBytes(await req.Content.ReadAsStringAsync())));
            return req.CreateResponse(HttpStatusCode.OK, new { text = "got your message. development in progress" }, JsonMediaTypeFormatter.DefaultMediaType);
        }
    }
}
