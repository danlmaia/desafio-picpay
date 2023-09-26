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
        private readonly ApplicationDbContext _applicationDbContext;

        public TransactionController(ApplicationDbContext ApplicationDbContext) 
        {
            _applicationDbContext = ApplicationDbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var transactions = _applicationDbContext.Transactions.ToList();

            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var transaction = _applicationDbContext.Transactions.SingleOrDefault(x => x.Id == id);

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

                TransactionTRA.ValidateTransfer(transferDTO, _applicationDbContext);

                Transfer transfer = Mapper.Mapper.DTOTransferEntity(transferDTO);

                _applicationDbContext.Transactions.Add(transfer);
                _applicationDbContext.SaveChanges();
                return CreatedAtAction(nameof(GetById), new { id = transfer.Id }, transfer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            
            
        }
    }
}
