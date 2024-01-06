using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTML_Serializer
{
    internal class HtmlElement
    {
        internal string Id;

        internal string Name;

        internal List<string> Attributes = new();

        internal List<string> Classes = new();

        internal string InnerHtml;
    
        public HtmlElement Parent;

        public HashSet<HtmlElement> Children = new ();
        public IEnumerable<HtmlElement> Descendants()
        {
            Queue<HtmlElement> elements = new();
            elements.Enqueue(this);
            while (elements.Count > 0)
            {
                HtmlElement element = elements.Dequeue();
                yield return element;
                foreach (HtmlElement child in element.Children) 
                    elements.Enqueue(child);
            }
        }
        public IEnumerable<HtmlElement> Ancestors()
        {
            HtmlElement currentParent = this.Parent;
            while (currentParent != null) { 
                yield return currentParent;
                currentParent = currentParent.Parent;
            }
        }


    }
}
