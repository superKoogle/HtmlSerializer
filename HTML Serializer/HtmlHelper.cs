using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace HTML_Serializer
{
    internal class HtmlHelper
    {
        internal List<string> htmlTags;
        internal List<string> htmlVoidTags;
        private readonly static HtmlHelper _singleHtmlHelper = new();
        public static HtmlHelper Instance => _singleHtmlHelper;
        private HtmlHelper()
        {
            htmlTags = JsonSerializer.Deserialize<List<string>>(File.ReadAllText("seed/HtmlTags.json"));
            htmlVoidTags = JsonSerializer.Deserialize<List<string>>(File.ReadAllText("seed/HtmlVoidTags.json"));
        }
    }
}
