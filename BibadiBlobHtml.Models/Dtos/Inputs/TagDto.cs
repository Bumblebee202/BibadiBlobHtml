using BibadiBlobHtml.Models.Commons.Enums;

namespace BibadiBlobHtml.Models.Dtos.Inputs
{
    public class TagDto
    {
        public string Id { get; set; }
        public string Content { get; set; }

        public IEnumerable<TagDto> Children { get; set; }
        public TagType Type { get; set; }
        public IEnumerable<string> Classes { get; set; }
    }
}
