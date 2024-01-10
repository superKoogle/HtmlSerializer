using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTML_Serializer
{
    internal static class HtmlElementExtention
    {
        public static List<HtmlElement> FindBySelector(this HtmlElement element, Selector selector)
        {
            HashSet<HtmlElement> searchResult = new();
            FilterElements(element, selector, searchResult);
            return searchResult.ToList();
        }

        private static void FilterElements(HtmlElement element, Selector selector, HashSet<HtmlElement> results)
        {
            if (selector.Child == null)
            {
                results.Add(element);
                return;
            }
            List<HtmlElement> allDescendants = element.Descendants().ToList();
            foreach (HtmlElement child in allDescendants)
            {
                if (selector.Id != null && !selector.Id.Equals(child.Id))
                    continue;
                else if (selector.tagName != null && !selector.tagName.Equals(child.Name))
                    continue;
                else if (selector.Classes.Count > 0 && selector.Classes.Except(child.Classes).Any())
                    continue;
                FilterElements(child, selector.Child, results);
            }                
        }
    }
}