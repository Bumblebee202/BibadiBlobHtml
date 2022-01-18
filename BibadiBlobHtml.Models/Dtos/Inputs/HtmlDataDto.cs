namespace BibadiBlobHtml.Models.Dtos.Inputs
{
    public class HtmlDataDto
    {
        public byte[] Template { get; set; }
        public IEnumerable<TagDto> Tags { get; set; }
        public IEnumerable<TextDto> Texts { get; set; }
    }
}
