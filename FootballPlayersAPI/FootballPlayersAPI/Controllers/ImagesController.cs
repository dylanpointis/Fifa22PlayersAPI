using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FootballPlayersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    //This controller is necessary because the request to the image is blocked by CORS/CORB in the browser.
    //This controller works as a proxy, so the request is not blocked because it's not made by a browser.
    //On the frontend, it doesn't make the request to the sofifa cdn directly but to the backend server, which returns the image as a file.
    public class ImagesController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        // Inject HttpClient to make an http request to the sofifa CDN.
        public ImagesController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // GET api/Images?url=https://cdn.sofifa.net/players/158/023/22_120.png
        [HttpGet]
        public async Task<IActionResult> ProxyImage([FromQuery] string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return BadRequest("Invalid URL");

            try
            {
                //http request to the CDN
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode) //if it fails get default image
                {
                    //return StatusCode(Convert.ToInt32(response.StatusCode), "Error fetching image");
                    return await GetDefaultImage();
                }
                // define the content type,  image/png
                var contentType = response.Content.Headers.ContentType?.ToString() ?? "image/png";
                // convert the image to a byte array
                var imageBytes = await response.Content.ReadAsByteArrayAsync();

                // return the image as a file
                return File(imageBytes, contentType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Proxy error: {ex.Message}");
            }
        }

        private async Task<IActionResult> GetDefaultImage()
        {
            string defaultImageUrl = "https://cdn.sofifa.net/player_0.svg";
            HttpResponseMessage response = await _httpClient.GetAsync(defaultImageUrl);
            if (!response.IsSuccessStatusCode)
                return StatusCode(500, "Error fetching default player image.");

            
            var contentType = response.Content.Headers.ContentType?.ToString() ?? "image/svg+xml";
            var imageBytes = await response.Content.ReadAsByteArrayAsync();

            return File(imageBytes, contentType);
        }
    }
}
