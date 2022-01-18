using BibadiBlobHtml.Models.Commons.Enums;
using BibadiBlobHtml.Models.Dtos.Inputs;
using BibadiBlobHtml.Models.Dtos.Outputs;
using HtmlAgilityPack;

namespace BibadiBlobHtml.Core.Services.Html
{
    public class HtmlService : IHtmlService
    {
        public Task<HtmlDto> InsertDataToTemplate(HtmlDataDto htmlData)
        {
            return Task.Run(async () =>
            {
                using var memoryStream = new MemoryStream(htmlData.Template);
                var htmlDocument = new HtmlDocument();
                htmlDocument.Load(memoryStream);

                var insertTagsTask = InsertTags(htmlDocument, htmlData.Tags);
                var insertTextTask = InsertText(htmlDocument, htmlData.Texts);

                await Task.WhenAll(insertTagsTask, insertTextTask);

                await insertTagsTask;
                await insertTextTask;

                var bytes = Convert.FromBase64String(htmlDocument.DocumentNode.OuterHtml);

                return new HtmlDto
                {
                    Content = bytes
                };
            });
        }

        async Task InsertTags(HtmlDocument htmlDocument, IEnumerable<TagDto> tags)
        {
            foreach (var tag in tags)
            {
                var parentNode = htmlDocument.GetElementbyId(tag.Id);

                var node = tag.Type switch
                {
                    TagType.Img => await CreateImageNode(htmlDocument, tag),
                    _ => await CreateNode(htmlDocument, tag)
                };

                parentNode.ChildNodes.Add(node);
            }
        }

        Task InsertText(HtmlDocument htmlDocument, IEnumerable<TextDto> texts)
        {
            return Task.Run(() =>
            {
                foreach (var text in texts)
                {
                    var node = htmlDocument.GetElementbyId(text.HtmlTagId);
                    node.InnerHtml = text.Content;
                }
            });
        }

        Task<HtmlNode> CreateImageNode(HtmlDocument htmlDocument, TagDto tag)
        {
            return Task.Run(() =>
            {
                if (tag.Children != null && tag.Children.Any())
                {
                    throw new ArgumentException("bib");
                }

                var node = htmlDocument.CreateElement("img");
                node.SetAttributeValue("src", $"data:image/png;base64,{tag.Content}");

                return node;
            });
        }

        async Task<HtmlNode> CreateNode(HtmlDocument htmlDocument, TagDto tag)
        {
            var tagName = tag.Type.ToString().ToLower();
            var node = htmlDocument.CreateElement(tagName);
            node.InnerHtml = tag.Content;

            foreach (var child in tag.Children)
            {
                var childNode = tag.Type switch
                {
                    TagType.Img => await CreateImageNode(htmlDocument, tag),
                    _ => await CreateNode(htmlDocument, tag)
                };

                node.ChildNodes.Add(childNode);
            }

            return node;
        }

    }
}
