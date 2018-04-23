using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace HangfireDemo.Logic.Services.HtmlService
{
    public interface IHtmlService
    {
        HtmlDocument Load(string url);

        IEnumerable<string> GetUrls(HtmlDocument document);

        int CountText(HtmlDocument document, string pattern);
    }
}
