using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebBrowseren
{
    public class HtmlManager
    {
        //string tagPattern = @"(<\/[^>]*)";
        static string defaultTagBodyPattern = @"(<p.*>)((.|\n)*?)(<\/p)"; 

        //TODO: This should be a part of getformatter it should return tags content 
        //TODO: When find a tag loop through result and check for other tags until no more tags can be find the print content
        //private List<string> GetAllTagNames(string response)
        //{
        //    // Get all tagNames in the response
        //    Regex tagRgx = new(tagPattern);

        //    var tagMatches = tagRgx.Matches(response);

        //    List<string> tags = new List<string>();

        //    foreach (Match m in tagMatches)
        //    {
        //        string tagName = m.ToString().Split('/')[1]; //search for example </p then split with / and then select name

        //        tags.Add(tagName);
        //    }
        //    return tags;
        //}

        //public void HtmlFormatter(string response, string unSelectedTagsStr = "")
        //{
        //    List<string> tags = GetAllTagNames(response);

        //    //Get all tags from the response with content
        //    List<HtmlTagMatch> htmlTagMatches = new(); //List to save all match and then order by index after

        //    foreach (string tagname in tags)
        //    {
        //        string tagBodyPattern = defaultTagBodyPattern.Replace("p", tagname);
        //        Regex tagBodyRgx = new(tagBodyPattern);

        //        foreach (Match match in tagBodyRgx.Matches(response))
        //        {
        //            htmlTagMatches.Add(new HtmlTagMatch{ Html = match.ToString(), Index = match.Index });
        //        }
        //    }

        //    foreach (HtmlTagMatch tag in htmlTagMatches.OrderBy(t => t.Index).ToList())
        //    {
        //        Console.WriteLine(tag.Html);
        //    }
        //}

        ///// <summary>
        ///// Select all other tags then input if input is not set then return all tags
        ///// </summary>
        ///// <param name="tags">if multiple tags seperate with , </param>
        ///// <returns></returns>
        //public List<string> UnselectHtmlTags(string html, string unSelectedTagsStr)
        //{
        //    List<string> tags = GetAllTagNames(html).Distinct().ToList();
        //    if (unSelectedTagsStr == "")
        //    {
        //        return tags;
        //    }

        //    string[] unSelectedTags = unSelectedTagsStr.Split(",");

        //    var test = tags.Except(unSelectedTags).ToList();
        //    return test;
        //}

        /// <summary>
        /// Return string without unwanted tags
        /// </summary>
        /// <param name="html"> The html response from httpClient</param>
        /// <param name="unselectedTags">The tags you want to remove</param>
        /// <returns></returns>
        public static  string GetFormattedHtml(string html, string[] unselectedTags = null)
        {
            if (unselectedTags is null)
            {
                return html;
            }

            foreach (string tagName in unselectedTags)
            {
                string tagpattern = defaultTagBodyPattern.Replace("p", tagName);

                Regex tagrgx = new(tagpattern);

                html = tagrgx.Replace(html, "");
            }
            return html;
        }

    }

    public class HtmlTagMatch
    {
        public string Html{ get; set; }
        public int Index { get; set; }
    }
}
