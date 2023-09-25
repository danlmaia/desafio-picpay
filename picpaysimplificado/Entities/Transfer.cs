namespace picpaysimplificado.Entities
{
    public class Transfer
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public required int Payer { get; set; }

        public required int Receiver { get; set; }

        public DateTime Date { get; set; }
    }
}
