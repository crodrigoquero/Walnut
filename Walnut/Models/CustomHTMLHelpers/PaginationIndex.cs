using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Walnut.Models.CustomHTMLHelpers
{
    public static class PaginationIndex
    {

        public enum ButtonsType { Text, Image };

        public static MvcHtmlString PageIndexLink(this HtmlHelper htmlhelper,
        string ControllerName, int CurrentPage)
        {
            string enlace = ControllerName + "?page=" + CurrentPage.ToString();

            var AnchorTag = new TagBuilder("a");
            AnchorTag.MergeAttribute("href", enlace);
            AnchorTag.InnerHtml = " " + CurrentPage.ToString() + " ";


            return MvcHtmlString.Create(AnchorTag.ToString
               (TagRenderMode.Normal));
        }

        public static MvcHtmlString PageIndexLinks(this HtmlHelper htmlhelper,
         int CurrentPage, int TotalRecords, int recordsPerPage=10, string @class= "", string controllerName = "", ButtonsType buttonsType = ButtonsType.Text)
        {
            int _TotalPages = Math.Abs((TotalRecords / recordsPerPage));

            // If we have decimals, that means that we have an additional page at the end with a few records...
            if ((_TotalPages * recordsPerPage) != TotalRecords)
            {
                _TotalPages = _TotalPages + 1;
            }


            var chorizoHTML = "";
            string enlace = "";

            if (CurrentPage > 1)
            {
                var AnchorTag = new TagBuilder("a");
                enlace = controllerName + "?page=" + (CurrentPage - 1).ToString();
                AnchorTag.InnerHtml = "< ";
                AnchorTag.MergeAttribute("class", "PagerPageNumber");
                AnchorTag.MergeAttribute("id", "PagerPageNumberPrevious");
                AnchorTag.MergeAttribute("value", (CurrentPage - 1).ToString());

                AnchorTag.MergeAttribute("href", enlace);
                chorizoHTML = chorizoHTML + MvcHtmlString.Create(AnchorTag.ToString(TagRenderMode.Normal));

            }

            for (int a = 1; a <= _TotalPages; a++)
            {
                var AnchorTag = new TagBuilder("a");
                var ImageTag = new TagBuilder("img");

                enlace = "";


                if (a == CurrentPage)
                {
                    var BoldTag = new TagBuilder("b");
                    BoldTag.MergeAttribute("id", "PagerCurrentPage");
                    BoldTag.MergeAttribute("class", @class);
                    BoldTag.InnerHtml = a.ToString();
                    chorizoHTML = chorizoHTML + MvcHtmlString.Create(BoldTag.ToString(TagRenderMode.Normal)) + "&nbsp|&nbsp";
                } else
                {
                    enlace = controllerName + "?page=" + a.ToString();
                    AnchorTag.InnerHtml = " " + a.ToString() + " ";

                    AnchorTag.MergeAttribute("href", enlace);
                    AnchorTag.MergeAttribute("class", "PagerPageNumber");
                    AnchorTag.MergeAttribute("id", "PagerPageNumber" + a.ToString());
                    AnchorTag.MergeAttribute("value", a.ToString()); //we'll need this value later...
                    chorizoHTML = chorizoHTML + MvcHtmlString.Create(AnchorTag.ToString(TagRenderMode.Normal)); //+ "&nbsp|&nbsp";

                    ImageTag.MergeAttribute("src", "../../Content/Images/animation2.gif");
                    ImageTag.MergeAttribute("class", "LoadingPageIcon");
                    ImageTag.MergeAttribute("id", "LoadingPageIcon" + a.ToString());
                    //ImageTag.MergeAttribute("alt", "Loading page data");
                    ImageTag.MergeAttribute("height", "15");
                    ImageTag.MergeAttribute("width", "15");
                    ImageTag.MergeAttribute("hidden", "hidden");
                    ImageTag.MergeAttribute("value", a.ToString()); //we'll need this value later...

                    chorizoHTML = chorizoHTML + MvcHtmlString.Create(ImageTag.ToString()) +  "&nbsp|&nbsp";

                }


            }

            if (CurrentPage < _TotalPages)
            {
                var AnchorTag = new TagBuilder("a");
                enlace = controllerName + "?page=" + (CurrentPage + 1).ToString();
                AnchorTag.MergeAttribute("class", "PagerPageNumber");
                AnchorTag.MergeAttribute("id", "PagerPageNumberNext");
                AnchorTag.InnerHtml = " >";
                AnchorTag.MergeAttribute("value", (CurrentPage + 1).ToString());
                AnchorTag.MergeAttribute("href", enlace);

                chorizoHTML = chorizoHTML + MvcHtmlString.Create(AnchorTag.ToString(TagRenderMode.Normal));
            }

            return MvcHtmlString.Create(chorizoHTML);
        }
    }
}