using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HTML_Serializer
{
    internal class Selector
    {
        public string tagName;
        public string Id;
        public List<string> Classes = new();
        private Selector Parent;
        public Selector Child;
        internal static Selector convertStringToSelector(string query)
        {
            Selector selector = new();
            Selector current = selector;
            List<string> subQueries = query.Split(' ').ToList();
            foreach (string subQuery in subQueries)
            {
                List<string> values = Regex.Split(subQuery, @"(?=[.#])").ToList();
                foreach(string value in values.Where(val => val.Length > 0).ToList())
                {
                    switch (value[0])
                    {
                        case '#': 
                            current.Id = value[1..];
                            break;
                        case '.':
                            current.Classes.Add(value[1..]);
                            break;
                        default:
                            if (HtmlHelper.Instance.htmlTags.Contains(value))
                                current.tagName = value;
                            break;
                    }
                }
                Selector newSelector = new Selector();
                newSelector.Parent = current;
                current.Child = newSelector;
                current = newSelector;
            }
            return selector;
        }
    }
}
