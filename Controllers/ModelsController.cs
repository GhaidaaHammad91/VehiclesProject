using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.IO;
using VehiclesProject.DTO;

namespace VehiclesProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModelsController : ControllerBase
    {
        private readonly ILogger<ModelsController> _logger;

        public ModelsController(ILogger<ModelsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "models")]
        public async Task<IActionResult> GetModelsForMakeYear([FromQuery] [Required] int modelyear, [FromQuery][Required] string make)
        {
            /* can make another option based on requirments :
             if not pass model year and make name can return all Models
             if pass model year not make = null return all Models for this year only
             if pass make name and year = null can return all Models for this make name only */
            
            if (modelyear <= 0) {
                var error = new InvalidDataException("Invalid model year");
                return base.BadRequest(error.Message);
            }

            if (string.IsNullOrWhiteSpace(make))
            {
                var error = new InvalidDataException("Invalid make name");
                return base.BadRequest(error.Message);
            }


            List<string> Models = new List<string>();

            HttpClient client = new HttpClient();
            string url = "https://vpic.nhtsa.dot.gov/api/vehicles/getmodelsformakeyear/make/" ;
            url = url + make + "/modelyear/"+ modelyear.ToString() + "?format=json";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var responseResult = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(responseResult))
                {
                    var APIResponse = JsonConvert.DeserializeObject<APIResponse>(responseResult);
                
                    if (APIResponse != null)
                    {
                        Models = APIResponse.Results.Select(x=> x.Model_Name).ToList();
                    }
                }
            }

            ModelResponse modelResponse = new ModelResponse(Models);
            return base.Ok(modelResponse);
        }
    }
}