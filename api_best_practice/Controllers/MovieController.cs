
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
        // private readonly string _apiKey = "5be2292958bad7179efadcebb8708b36"; // Buraya API anahtarınızı girin
        // your token.
        private readonly _token = 'eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJhOGYyNTk4NDk4MmQyMTI0NWEwYjM0MDEzZmNlYTFiYSIsIm5iZiI6MTcyNjU2ODc1Ny4yNzM3ODUsInN1YiI6IjVlM2ZlNDc1MGMyNzEwMDAxMzdjZWYxZiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.yBJ7WX1Q3oPyuzJxEXSbPK_8JEfLvg9OimzrkmeRdOM';
        public MovieController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetPopularMovies([FromQuery] int page = 1)
        {
            // string url = $"https://api.themoviedb.org/3/movie?api_key={_apiKey}&language=en-US&page={page}";
            string url = $"https://api.themoviedb.org/3/movie?include_adult=false&include_video=false&language=en-US&page=1&sort_by=vote_average.desc&without_genres=99,10755&vote_count.gte=200&page=${page}";
            // var request = new HttpRequestMessage(HttpMethod.Get, url);
    
            // request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound("Veriler bulunamadı.");
            }

            var json = await response.Content.ReadAsStringAsync();
            var movies = JObject.Parse(json);

            return Ok(movies);
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
