﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTML_Serializer
{
    public static class HtmlElementExtention
    {
        public static HtmlElement findBySelector(this Selector selector)
        {
            HashSet<HtmlElement> searchResult = new();
        }

        private static void filterElements(HtmlElement element, Selector selector, Collection<HtmlElement> results)
        {
            if (selector.Child == null)
            {
                results.Add(element);
                return;
            }
            List<HtmlElement> allDescendants = element.Descendants().ToList();
            foreach (HtmlElement child in allDescendants)
            {
                if(selector.Id != null && !selector.Id.Equals(child.Id))
                    allDescendants.Remove(child);
                else if (selector.tagName != null && !selector.tagName.Equals(child.Name))
                    allDescendants.Remove(child);
                else if (selector.Classes.Count > 0 && !(element.Classes.Intersect(selector.Classes).Count() == selector.Classes.Count))
                    allDescendants.Remove(child);
            }
            foreach (HtmlElement child in allDescendants)
                filterElements(child, selector.Child, results);
        }
    }
}