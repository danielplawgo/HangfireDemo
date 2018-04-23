using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace HangfireDemo.Logic.Services.HtmlService
{
    public class HtmlService : IHtmlService
    {
        public HtmlDocument Load(string url)
        {
            var web = new HtmlWeb();
            return web.Load(url);
        }

        public IEnumerable<string> GetUrls(HtmlDocument document)
        {
            return document.DocumentNode.SelectNodes("//a[@href]")
                .Select(x => x.Attributes["href"].Value)
                .Where(x => x.StartsWith("http"))
                .ToList();
        }

        public int CountText(HtmlDocument document, string pattern)
        {
            return document.DocumentNode.SelectNodes(String.Format("//*[text()[contains(., '{0}')]]", pattern)).Count;
        }
    }
}
