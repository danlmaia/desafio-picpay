using Microsoft.AspNetCore.Mvc;
using picpaysimplificado.Entities;
using picpaysimplificado.Jsons;
using picpaysimplificado.Persistence;

namespace picpaysimplificado.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TransactionDbContext _transactionDbContext;

        public UserController(TransactionDbContext transactionDbContext)
        {
            _transactionDbContext = transactionDbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var users = _transactionDbContext.Users.ToList();

                return Ok(users);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var user = _transactionDbContext.Users.SingleOrDefault(x => x.Id == id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("/User")]
        public IActionResult GenerateUser(JsonUser jsonUser)
        {
            try
            {
                Validator.Validator.ValidateUser(jsonUser);

                User user = Mapper.Mapper.JsonUserToEntity(jsonUser);

                _transactionDbContext.Users.Add(user);
                _transactionDbContext.SaveChanges();
                return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
