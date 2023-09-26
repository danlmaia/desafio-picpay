using picpaysimplificado.DTOs;
using picpaysimplificado.Entities;
using picpaysimplificado.Persistence;

namespace picpaysimplificado.Transaction
{
    public static class TransactionTRA
    {

        public static void ValidateTransfer(TransferDTO transferDTO, ApplicationDbContext _applicationDbContext)
        {
            try
            {
                User payer = _applicationDbContext.Users.Single(x => x.Id == transferDTO.Payer) ?? throw new Exception(string.Format("Payer com identificador {0} inexistente", transferDTO.Payer));

                if (payer.Type == Enum.UserType.Merchant)
                    throw new Exception(string.Format("Payer com identificador {0} é um Merchant", transferDTO.Payer));

                if (payer.Balance < transferDTO.Amount)
                    throw new Exception(string.Format("Payer com identificador {0} não possui saldo suficiente", transferDTO.Payer));

                User receiver = _applicationDbContext.Users.Single(x => x.Id == transferDTO.Receiver) ?? throw new Exception(string.Format("Receiver com identificador {0} inexistente", transferDTO.Receiver));

                RealizeTransfer(payer, receiver, transferDTO.Amount, _applicationDbContext);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void CancelTransfer(int id, ApplicationDbContext _applicationDbContext)
        {
            try
            {
                var transfer = _applicationDbContext.Transactions.SingleOrDefault(x => x.Id == id);

                if (transfer == null)
                    throw new Exception(string.Format("Transferência com identificador {0} não existe!", transfer.Id));

                var payer = _applicationDbContext.Users.SingleOrDefault(x => x.Id == transfer.Payer);

                if (payer == null)
                    throw new Exception(string.Format("pagador com identificador {0} não existe!", payer.Id));

                var receiver = _applicationDbContext.Users.SingleOrDefault(x => x.Id == transfer.Receiver);

                if (receiver == null)
                    throw new Exception(string.Format("receiver com identificador {0} não existe!", receiver.Id));

                RefoundTransfer(payer, receiver, transfer.Amount, _applicationDbContext);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void RealizeTransfer(User payer, User receiver, decimal amount, ApplicationDbContext _applicationDbContext)
        {
            payer.Balance -= amount;
            receiver.Balance += amount;

            _applicationDbContext.Users.Update(payer);
            _applicationDbContext.Users.Update(receiver);

        }

        public static void RefoundTransfer(User payer, User receiver, decimal amount, ApplicationDbContext _applicationDbContext)
        {
            payer.Balance += amount;
            receiver.Balance -= amount;

            _applicationDbContext.Users.Update(payer);
            _applicationDbContext.Users.Update(receiver);

        }
    }
}
