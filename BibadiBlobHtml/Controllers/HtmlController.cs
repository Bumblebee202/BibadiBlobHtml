using BibadiBlobHtml.Core.Services.Html;
using BibadiBlobHtml.Models.Dtos.Inputs;
using Microsoft.AspNetCore.Mvc;

namespace BibadiBlobHtml.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HtmlController : Controller
    {
        IHtmlService _htmlService;

        public HtmlController(IHtmlService htmlService) => _htmlService = htmlService;

        [HttpPost("InsertDataToTemplate")]
        public async Task<IActionResult> InsertDataToTemplate([FromBody] HtmlDataDto htmlData)
        {
            var html = await _htmlService.InsertDataToTemplate(htmlData);
            return Ok(html);
        }
    }
}
