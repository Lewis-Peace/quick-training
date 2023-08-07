using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lift.Buddy.Core.Models
{
    public class Response
    {
        [JsonPropertyName("notes")]
        public string? notes { get; set; }
        [JsonPropertyName("result")]
        public bool result { get; set; }
        [JsonPropertyName("body")]
        public string? body { get; set; }
    }
}
