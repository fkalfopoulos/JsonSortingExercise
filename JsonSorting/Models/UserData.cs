using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonSorting.Models
{
    public class UserData
    {
        
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("userId")]
            public string UserId { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("body")]
            public string Body { get; set; }
        }
  }

