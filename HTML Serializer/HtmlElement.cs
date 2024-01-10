using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTML_Serializer
{
    public class HtmlElement
    {
        public string Id;

        public string Name;

        public List<string> Attributes = new();

        public List<string> Classes = new();

        public string InnerHtml;
    
        public HtmlElement Parent;

        public HashSet<HtmlElement> Children = new ();
        public IEnumerable<HtmlElement> Descendants()
        {
            Queue<HtmlElement> elements = new();
            foreach(var child in Children) elements.Enqueue(child);
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
