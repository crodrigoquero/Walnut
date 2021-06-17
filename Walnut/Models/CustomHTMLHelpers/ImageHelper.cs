using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Walnut.Models.CustomHTMLHelpers
{
    public static class ImageHelper
    {

        public static MvcHtmlString SerializedImage(this HtmlHelper helper, byte[] imageData, string altText, string width, string @class, string id)
        {

            var builder = new TagBuilder("img");

            string base64 = "";
            string imgSrc = "";

            if (imageData != null)
            {
                base64 = Convert.ToBase64String(imageData);
                imgSrc = String.Format("data:image/gif;base64,{0}", base64);
            }

            else
            {
                //return null;
            }

            builder.MergeAttribute("src", imgSrc);
            //builder.MergeAttribute("alt", altText);
            builder.MergeAttribute("width", width);
            builder.MergeAttribute("class", @class);
            builder.MergeAttribute("id", id);
            //builder.MergeAttribute("height", "auto");
            //align="left|right|middle|top|bottom"

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));

        }
    }
}