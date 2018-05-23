using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
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
            var uselessData = await req.Content.ReadAsFormDataAsync();
            var data = new SlackPost
            {
                token = uselessData["token"],
                text = uselessData["text"],
                response_url = uselessData["response_url"]
            };

            logger.LogInformation("BODY: {0}", JsonConvert.SerializeObject(data));
            return req.CreateResponse(HttpStatusCode.OK, new { text = "got your message. development in progress" }, JsonMediaTypeFormatter.DefaultMediaType);
        }
    }
}
