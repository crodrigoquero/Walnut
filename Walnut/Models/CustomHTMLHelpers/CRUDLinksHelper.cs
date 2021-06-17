using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Walnut.Models.CustomHTMLHelpers
{
    public static class CRUDLinksHelper
    {
        public static MvcHtmlString CRUD_ActionLink(this HtmlHelper htmlhelper, string caption, string controllerName, string action, int recordNumber, bool useIcons)
        {
            var chorizoHTML = "";
            //string enlace = "";

            //output sample:
            //< img class="doSomethingSomewhere" src="/Content/Images/Saving.gif" height="17" width="17" id = "imgDelete1" hidden/>
            //<a class="btnDelete" href="/Admin/Candidate/Delete/1" id="btnDelete1" value="1">Delete</a>
            var imageTag = new TagBuilder("img");
            var innerHTMLText = "";

            imageTag.MergeAttribute("src", "../../Content/Images/Saving.gif");
            imageTag.MergeAttribute("class", "doSomethingSomewhere");
            imageTag.MergeAttribute("id", "img" + action + recordNumber.ToString());
            imageTag.MergeAttribute("height", "15");
            imageTag.MergeAttribute("width", "15");
            imageTag.MergeAttribute("hidden", "hidden");
            imageTag.MergeAttribute("value", recordNumber.ToString()); //we'll need this value later...

            chorizoHTML = chorizoHTML + MvcHtmlString.Create(imageTag.ToString(TagRenderMode.SelfClosing) + "\r" + "\n");

            //Image as a link
            imageTag = new TagBuilder("img");

            imageTag.MergeAttribute("src", "../../Content/Images/" + action +".png");
            imageTag.MergeAttribute("height", "17");
            imageTag.MergeAttribute("width", "17");

            innerHTMLText = MvcHtmlString.Create(imageTag.ToString(TagRenderMode.Normal) + "\r" + "\n").ToString();


            var AnchorTag = new TagBuilder("a");
            //enlace = controllerName + "?page=" + (CurrentPage - 1).ToString();

            AnchorTag.MergeAttribute("class", "btn" + action);
            AnchorTag.MergeAttribute("id", "btn" + action + recordNumber);
            AnchorTag.MergeAttribute("value", recordNumber.ToString());
            AnchorTag.MergeAttribute("href", "/Admin/" + controllerName + "/" + action + "/" + recordNumber);
            AnchorTag.InnerHtml = useIcons ? innerHTMLText: caption; // this must be conditional!!!!
            chorizoHTML = chorizoHTML + MvcHtmlString.Create(AnchorTag.ToString(TagRenderMode.Normal) + "\r" + "\n");

            return MvcHtmlString.Create(chorizoHTML);
        }
    }
}