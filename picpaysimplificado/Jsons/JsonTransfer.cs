using Newtonsoft.Json;
using RestSharp;

namespace picpaysimplificado.Jsons
{
    public class JsonTransfer
    {
        [JsonProperty("value")]
        public decimal Value{ get; set; }

        [JsonProperty("payer")]
        public string Payer { get; set; }

        [JsonProperty("receiver")]
        public string Receiver { get; set; }
    }
}
