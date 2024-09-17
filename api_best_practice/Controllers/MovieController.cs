
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace api_best_practice.Controllers
{
    [ApiController]
    [Route("movie")]
    public class MovieController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "5be2292958bad7179efadcebb8708b36"; // Buraya API anahtarınızı girin

        public MovieController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            string url = $"https://api.themoviedb.org/3/movie/{id}?api_key={_apiKey}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound("Film bulunamadı.");
            }

            var json = await response.Content.ReadAsStringAsync();
            var movie = JObject.Parse(json);

            return Ok(movie);
        }
    }
}
