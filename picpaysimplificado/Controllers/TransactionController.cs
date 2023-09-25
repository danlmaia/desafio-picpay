using Microsoft.AspNetCore.Mvc;
using picpaysimplificado.DTOs;
using picpaysimplificado.Entities;
using picpaysimplificado.Jsons;
using picpaysimplificado.Mapper;
using picpaysimplificado.Persistence;
using picpaysimplificado.Transaction;

namespace picpaysimplificado.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionDbContext _transactionDbContext;

        public TransactionController(TransactionDbContext transactionDbContext) 
        {
            _transactionDbContext = transactionDbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var transactions = _transactionDbContext.Transactions.ToList();

            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var transaction = _transactionDbContext.Transactions.SingleOrDefault(x => x.Id == id);

                if (transaction == null)
                {
                    return NotFound();
                }

                return Ok(transaction);
            }
            catch (Exception)
            {
                throw;
            }

            
        }

        [HttpPost]
        public IActionResult GenerateTransfer(JsonTransfer jsonTransfer)
        {
            try
            {
                TransferDTO transferDTO = Mapper.Mapper.JsonTransferDTO(jsonTransfer);

                TransactionTRA.ValidateTransfer(transferDTO, _transactionDbContext);

                Transfer transfer = Mapper.Mapper.DTOTransferEntity(transferDTO);

                _transactionDbContext.Transactions.Add(transfer);
                _transactionDbContext.SaveChanges();
                return CreatedAtAction(nameof(GetById), new { id = transfer.Id }, transfer);
            }
            catch (Exception)
            {
                throw;
            }

            
            
        }
    }
}
