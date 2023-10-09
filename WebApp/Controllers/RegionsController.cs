using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7118/api/regions");

                httpResponseMessage.EnsureSuccessStatusCode();

                var stringResposeBody = await httpResponseMessage.Content.ReadAsStringAsync();
                ViewBag.Response = stringResposeBody;
            }
            catch (Exception ex)
            {

                throw;
            }

            return View();
        }
    }
}
