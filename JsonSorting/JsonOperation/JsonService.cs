using JsonSorting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JsonSorting.JsonOperation
{
    public class JsonService
    {

        private readonly ILogger<JsonService> _logger;

        public JsonService(ILogger<JsonService> logger)
        {
            _logger = logger;
        }



        public List<UserData> SortingBubble(List<UserData> model)
        {
            for (int i = 0; i < model.Count; ++i)
            {
                for (int j = i; j < model.Count; ++j)
                {
                    int m = 0;
                    while (m != model[i].Title.Length || m != model[j].Title.Length)
                    {
                        if (model[i].Title[m] > model[j].Title[m])
                        {
                            var temp = model[i];
                            model[i] = model[j];
                            model[j] = temp;
                            break;
                        }
                        else if (model[i].Title[m] == model[j].Title[m])
                        {
                            ++m;
                        }
                        else
                        {
                            break;
                        }
                    }
                    Console.WriteLine(model);                }
            }
            return model;
        }


        public async Task<string> GetJson()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = string.Format("https://jsonplaceholder.typicode.com/posts");
                    var response = client.GetAsync(url).Result;
                    string str = await response.Content.ReadAsStringAsync();
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"the problem is {ex}");
                return "problem";
            }
        }

        public async Task<string> FindString()
        {
            try
            {
                string text = await GetJson();
                text = text.Replace("\\n", " ");
                text = text.Replace("\\", " ");
                text = text.Replace("\n", " ");
                text = Regex.Replace(text, "[^a-zA-Z0-9 -]", " ");
                string[] words = text.Split(new[] { " " }, StringSplitOptions.None);

                string word = "";
                int biggestNum = 0;
                foreach (var s in words)
                {
                    if (s.Length > biggestNum)
                    {
                        word = s;
                        biggestNum = s.Length;
                    }
                }
                return word;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"the problem is {ex}");
                return "function has problem";
            }
        }


        public async void FindStrings_With_Linq()
        {
            try
            {
                string text = await GetJson();
                text = text.Replace("\\n", " ");
                text = text.Replace("\\", " ");
                text = text.Replace("\n", " ");
                text = Regex.Replace(text, "[^a-zA-Z0-9 -]", " ");
                string[] words = text.Split(new[] { " " }, StringSplitOptions.None);

                List<int> allLengths = words.Select(x => x.Length).Distinct().ToList();
                List<string> biggestwords = words.OrderByDescending(u => u.Length).Distinct().Take(5).ToList();
                int biggestLength = allLengths.OrderByDescending(u => u).Distinct().Take(1).SingleOrDefault();
                var bigges = biggestwords.Where(u => u.Length == biggestLength).ToList();

                foreach (var c in bigges)
                {
                    _logger.LogInformation($"the biggest word(s) are : {c}");
                }
               
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"the problem is {ex}");
                
            }
        }
    }
}