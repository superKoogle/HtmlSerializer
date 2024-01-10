// See https://aka.ms/new-console-template for more information


using HTML_Serializer;
using System.Text.RegularExpressions;

var html = await Load("https://moodle.malkabruk.co.il/my/courses.php");
var cleanHtml = new Regex("\\s+").Replace(html, " ");
cleanHtml = new Regex("/\\*\\*(.*?)\\*\\*/").Replace(cleanHtml, "");
var htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Where(l => l.Length > 0 && l != " ");
HtmlElement tree = Serialize(htmlLines);
List<HtmlElement> searchresults1 = tree.FindBySelector(Selector.convertStringToSelector("body div div script"));
List<HtmlElement> searchresults2 = tree.FindBySelector(Selector.convertStringToSelector(".dropdown-divider"));
List<HtmlElement> searchresults3 = tree.FindBySelector(Selector.convertStringToSelector("div#page-wrapper a.sr-only-focusable"));


Console.ReadLine();

async Task<string> Load(string url)
{
    HttpClient client = new();
    var response = await client.GetAsync(url);
    return await response.Content.ReadAsStringAsync();
}



HtmlElement Serialize(IEnumerable<string> html)
{
    HtmlElement rootElement = new();
    var currentElement = rootElement;
    foreach (string htmlLine in html)
    {
        string firstWord = htmlLine.Contains(' ') ? htmlLine[..htmlLine.IndexOf(' ')] : htmlLine;
        if (htmlLine == "/html")
            return rootElement;
        if (htmlLine.StartsWith("/"))
        {
            currentElement = currentElement.Parent;
            continue;
        }
        if (HtmlHelper.Instance.htmlTags.Contains(firstWord))
        {
            currentElement = updateCurrentElement(currentElement, firstWord, htmlLine);
            if (HtmlHelper.Instance.htmlVoidTags.Contains(firstWord) || htmlLine.EndsWith(" /"))
            {
                currentElement = currentElement.Parent;
            }
        }
        else
        {
            currentElement.InnerHtml = htmlLine;
        }
    }
    return rootElement;
}


HtmlElement updateCurrentElement(HtmlElement currentElement, string tag, string line)
{
    var newElement = new HtmlElement
    {
        Parent = currentElement,
        Name = tag
    };
    currentElement.Children.Add(newElement);
    FillHtmlElementFromString(line[tag.Length..], newElement);
    return newElement;
}

void FillHtmlElementFromString(string line, HtmlElement newElement)
{
    var attributes = new Regex("(.*?)=\"(.*?)\"").Matches(line)
        .Select(Match => Match.Value).ToList();
    foreach (var attribute in attributes)
    {
        var equalIndex = attribute.IndexOf('=');
        var att = new Tuple<string, string>(attribute[..equalIndex], attribute[(equalIndex + 1)..]);
        switch (att.Item1[1..])
        {
            case "id":
                newElement.Id = att.Item2[1..(att.Item2.Length-1)];
                break;
            case "class":
                newElement.Classes = att.Item2[1..(att.Item2.Length - 1)].Split(' ').ToList();
                break;
            default:
                newElement.Attributes.Add(attribute);
                break;
        }
    }
}