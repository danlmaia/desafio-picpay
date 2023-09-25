namespace picpaysimplificado.DTOs
{
    public class TransferDTO
    {
        public decimal Amount { get; set; }

        public required int Payer { get; set; }

        public required int Receiver { get; set; }

        public DateTime Date { get; set; }
    }
}
