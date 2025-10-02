using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoPokegochi.Data.Dtos;
using ProjetoPokegochi.Services;
using System;
using System.Text.Json;

namespace ProjetoPokegochi.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class MascoteController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();

        [HttpGet]
        public async Task<IActionResult> GetMascotesAsync()
        {
            HttpResponseMessage response = await client.GetAsync("https://pokeapi.co/api/v2/pokemon?limit=100000&offset=0");

            string content = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<object>(content);

            return Ok(json);

        }

        [HttpGet("id")]
        public async Task<IActionResult> GetMascotesIdAsync(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"https://pokeapi.co/api/v2/pokemon/{id}");

            string content = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<object>(content);

            return Ok(json);

        }
    }
}
