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
        private static string SlackToken => ConfigurationManager.AppSettings.Get("SlackToken");

        [FunctionName(nameof(ReceiveWebhookAsync))]
        public static async Task<HttpResponseMessage> ReceiveWebhookAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "slack/po")]HttpRequestMessage req,
            [Queue("request", Connection = "StorageAccountConnection")]IAsyncCollector<Messages.Request> collector,
            ILogger logger)
        {
            // Get the bits from the message
            var uselessData = await req.Content.ReadAsFormDataAsync();
            var data = new SlackPost
            {
                token = uselessData["token"],
                text = uselessData["text"],
                response_url = uselessData["response_url"]
            };

            // Make sure it's a legit request
            if (!SlackToken.Equals(data.token))
                return req.CreateResponse(HttpStatusCode.Unauthorized);

            // TODO: remove this once more development has happened
            logger.LogInformation("BODY: {0}", JsonConvert.SerializeObject(data));

            // Queue up the work and send back a response
            await collector.AddAsync(new Messages.Request { category = data.text, response_url = data.response_url });
            return req.CreateResponse(HttpStatusCode.OK, new { text = "got your message. development in progress" }, JsonMediaTypeFormatter.DefaultMediaType);
        }
    }
}
