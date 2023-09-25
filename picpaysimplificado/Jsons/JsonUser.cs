using Newtonsoft.Json;
using picpaysimplificado.Enum;

namespace picpaysimplificado.Jsons
{
    public class JsonUser
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("document")]
        public string Document { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("balance")]
        public decimal Balance { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
