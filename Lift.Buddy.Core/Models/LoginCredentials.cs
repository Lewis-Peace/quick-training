using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lift.Buddy.Core.Models
{
    public class LoginCredentials
    {
        [JsonPropertyName("username")]
        public string? Username { get; set; }
        [JsonPropertyName ("password")]
        public string? Password { get; set; }
    }
}
