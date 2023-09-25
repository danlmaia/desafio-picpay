using picpaysimplificado.DTOs;
using picpaysimplificado.Entities;
using picpaysimplificado.Enums;
using picpaysimplificado.Jsons;

namespace picpaysimplificado.Mapper
{
    public static class Mapper
    {
        public static TransferDTO JsonTransferDTO(JsonTransfer jsonTransfer)
        {
            return new TransferDTO
            {
                Payer = int.Parse(jsonTransfer.Payer),
                Receiver = int.Parse(jsonTransfer.Receiver),
                Amount = jsonTransfer.Value,
                Date = DateTime.Now,
            };
        }

        public static Transfer DTOTransferEntity(TransferDTO transferDTO)
        {
            return new Transfer
            {
                Payer = transferDTO.Payer,
                Receiver = transferDTO.Receiver,
                Amount = transferDTO.Amount,
                Date = DateTime.Now,
            };
        }

        public static User JsonUserToEntity(JsonUser jsonUser)
        {
            System.Enum.TryParse(jsonUser.Type, out UserType Type);

            return new User
            {
                FirstName = jsonUser.FirstName,
                LastName = jsonUser.LastName,
                Email = jsonUser.Email,
                Balance = jsonUser.Balance,
                Document = jsonUser.Document,
                Password = jsonUser.Password,
                Type = Type
            };
        }
    }
}
