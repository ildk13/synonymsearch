using Microsoft.AspNetCore.Mvc;
using SynonymSearchAPI.Data;

namespace SynonymSearchAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SynonymController : ControllerBase
    {
        private readonly ISynonymService synonymService;

        public SynonymController(ISynonymService synonymService)
        {
            this.synonymService = synonymService ?? throw new ArgumentNullException(nameof(synonymService));
        }

        [HttpGet]
        public IActionResult GetSynonyms([FromQuery] string word)
        {
            try
            {
                var result = synonymService.GetSynonyms(word);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("add")]
        public IActionResult AddWord([FromBody] WordRequest request)
        {
            synonymService.AddSynonym(request);
            return Ok();
        }
    }
}
