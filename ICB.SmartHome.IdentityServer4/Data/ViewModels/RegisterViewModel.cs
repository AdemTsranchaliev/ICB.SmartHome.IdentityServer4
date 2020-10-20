using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICB.SmartHome.IdentityServer4.Data.ViewModels
{
    [JsonObject]
    public class RegisterViewModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("secondName")]
        public string SecondName { get; set; }

        [JsonProperty("email")]
        public string Password { get; set; }

        [JsonProperty("email")]
        public string RepeatPassword { get; set; }

        [JsonProperty("email")]
        public string Salt { get; set; }


    }
}
