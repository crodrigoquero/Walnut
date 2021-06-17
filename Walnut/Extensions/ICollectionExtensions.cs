using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Walnut.Extensions
{
    public static class ICollectionExtensions // static because contains extensione methods
    {
        public static IEnumerable<SelectListItem> ToSelectListItem<T>(
         this ICollection<T> items, int? selectedValue)
        {

            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("Description"),
                       Value = item.GetPropertyValue("Id"), //calling the other EXTENSION METHOD I have created
                       Selected = item.GetPropertyValue("Id")
                       .Equals(selectedValue.ToString())
                   };
        }


    }
}