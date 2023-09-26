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
        private readonly ApplicationDbContext _ApplicationDbContext;

        public UserController(ApplicationDbContext ApplicationDbContext)
        {
            _ApplicationDbContext = ApplicationDbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var users = _ApplicationDbContext.Users.ToList();

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
                var user = _ApplicationDbContext.Users.SingleOrDefault(x => x.Id == id);

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

                _ApplicationDbContext.Users.Add(user);
                _ApplicationDbContext.SaveChanges();
                return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
