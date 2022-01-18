using BibadiBlobHtml.Models.Dtos.Inputs;
using BibadiBlobHtml.Models.Dtos.Outputs;

namespace BibadiBlobHtml.Core.Services.Html
{
    public interface IHtmlService
    {
        Task<HtmlDto> InsertDataToTemplate(HtmlDataDto htmlData);
    }
}
