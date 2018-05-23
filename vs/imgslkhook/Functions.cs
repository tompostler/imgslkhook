using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace imgslkhook
{
    public static class Functions
    {
        private static string SAConnection => ConfigurationManager.AppSettings.Get("StorageAccountConnection");

        [FunctionName(nameof(ReceiveWebhookAsync))]
        public static async Task<HttpResponseMessage> ReceiveWebhookAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "slack/po")]HttpRequestMessage req,
            //[QueueTrigger("request", Connection = "StorageAccountConnection")]IAsyncCollector<Messages.Request> collector,
            ILogger logger)
        {
            logger.LogInformation("BODY: {0}", JsonConvert.SerializeObject(await req.Content.ReadAsAsync<SlackPost>()));
            return req.CreateResponse(HttpStatusCode.OK, new { text = "got your message. development in progress" }, JsonMediaTypeFormatter.DefaultMediaType);
        }
    }
}
