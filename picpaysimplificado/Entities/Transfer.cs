using picpaysimplificado.Jsons;
using System.Reflection.Metadata;

namespace picpaysimplificado.Entities
{
    public class Transfer
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public required long Payer { get; set; }

        public required long Receiver { get; set; }

        public DateTime Date { get; set; }
    }
}
