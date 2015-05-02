using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace DependencyElimination
{
    static class LinksCounter
    {

        public static IEnumerable<string> GetAllLinks(string url)
        {
            for (var page = 1; page < 6; page++)
            {
                string content;
                var fulledUrl = url + page;
                Console.WriteLine(fulledUrl);
                try
                {
                    content = GetPageContent(fulledUrl);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                var linksOnPage = ExtractLinks(content);
                Console.WriteLine("found {0} links", linksOnPage.Length);
                foreach (var link in linksOnPage)
                {
                    yield return link;
                }
            }
        }

        public static string GetPageContent(string url)
        {
            using (var http = new HttpClient())
            {
                var response = http.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsStringAsync().Result;
                throw new Exception("Error: " + response.StatusCode + " " + response.ReasonPhrase);
            }
        }

        private static string[] ExtractLinks(string content)
        {
            var matches = Regex.Matches(content, @"\Whref=[""'](.*?)[""'\s>]").Cast<Match>();
            return matches.Select(match => match.Groups[1].Value).ToArray();
        }
    }

    internal class Program
    {
        private static void Main()
        {
            var sw = Stopwatch.StartNew();
            var links = LinksCounter.GetAllLinks(@"http://habrahabr.ru/top/page").ToArray();
            File.WriteAllLines("links.txt", links);
            Console.WriteLine("Total links found: {0}", links.Length);
            Console.WriteLine("Finished");
            Console.WriteLine(sw.Elapsed);
        }




    }
}