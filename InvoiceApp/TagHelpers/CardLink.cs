using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace InvoiceApp.TagHelpers
{
    [HtmlTargetElement("card-link")]
    public class CardLink : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public string Controller { get; set; } = String.Empty;
        public string Action { get; set; } = String.Empty;
        public string Text { get; set; } = String.Empty;


        public CardLink(IUrlHelperFactory urlHelperFacory)
        {
            _urlHelperFactory = urlHelperFacory;
        }


        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

            output.TagName = "a";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("href", urlHelper.Action(Action, Controller));
            output.Attributes.Add("class", "card-link card rounded text-decoration-none d-inline-flex flex-column align-items-center justify-content-center gap-1 py-1 px-0");

            var content = await output.GetChildContentAsync();
            output.Content.AppendHtml(content.GetContent());
            output.Content.AppendHtml($"<span class=\"fs-5 text-secondary m-0\">{Text}</span>");
        }
    }
}
