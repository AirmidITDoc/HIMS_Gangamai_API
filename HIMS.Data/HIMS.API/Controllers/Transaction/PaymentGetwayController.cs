using System.Net.Http;
using System.Threading.Tasks;
using HIMS.Model.OnlinePayment;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HIMS.API.Controllers.Transaction
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentGetwayController : Controller
    {

        [HttpPost("OnlinePayment")]
        public async Task<IActionResult> OnpayAsync(OnlinePaymentParams obj)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://www.plutuscloudserviceuat.in:8201/API/CloudBasedIntegration/V1/UploadBilledTransaction");
            var content = new StringContent(JsonConvert.SerializeObject(obj), null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();

            return Ok(res);

        }

        [HttpPost("OnlinePaymentStatus")]
        public async Task<IActionResult> OnpaystatusAsync(OnlinePaymentStatusParams obj)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://www.plutuscloudserviceuat.in:8201/API/CloudBasedIntegration/V1/GetCloudBasedTxnStatus");
            var content = new StringContent(JsonConvert.SerializeObject(obj), null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return Ok(res);

        }


        [HttpPost("OnlinePaymentCancel")]
        public async Task<IActionResult> OnpayCancelAsync(OnlinePaymentCancelParams obj)
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://www.plutuscloudserviceuat.in:8201/API/CloudBasedIntegration/V1/CancelTransaction");
            var content = new StringContent(JsonConvert.SerializeObject(obj), null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string res = await response.Content.ReadAsStringAsync();
            return Ok(res);

        }

    }
}
