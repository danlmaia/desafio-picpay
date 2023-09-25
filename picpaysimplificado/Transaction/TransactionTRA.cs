using picpaysimplificado.DTOs;
using picpaysimplificado.Entities;
using picpaysimplificado.Persistence;

namespace picpaysimplificado.Transaction
{
    public static class TransactionTRA
    {

        public static void ValidateTransfer(TransferDTO transferDTO, TransactionDbContext _transactionDbContext)
        {
            try
            {
                User payer = _transactionDbContext.Users.Single(x => x.Id == transferDTO.Payer) ?? throw new Exception(string.Format("Payer com identificador {0} inexistente", transferDTO.Payer));

                if (payer.Type == Enums.UserType.Merchant)
                    throw new Exception(string.Format("Payer com identificador {0} é um Merchant", transferDTO.Payer));

                if (payer.Balance < transferDTO.Amount)
                    throw new Exception(string.Format("Payer com identificador {0} não possui saldo suficiente", transferDTO.Payer));

                User receiver = _transactionDbContext.Users.Single(x => x.Id == transferDTO.Receiver) ?? throw new Exception(string.Format("Receiver com identificador {0} inexistente", transferDTO.Receiver));

                RealizeTransfer(payer, receiver, transferDTO.Amount);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void RealizeTransfer(User payer, User receiver, decimal amount)
        {
            payer.Balance -= amount;
            receiver.Balance += amount;
        }
    }
}
