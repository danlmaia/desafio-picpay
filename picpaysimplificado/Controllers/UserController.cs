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
        private readonly ApplicationDbContext _applicationDbContext;

        public UserController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var users = _applicationDbContext.Users.ToList();

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
                var user = _applicationDbContext.Users.SingleOrDefault(x => x.Id == id);

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

        [HttpPost("/GenerateUser")]
        public IActionResult GenerateUser(JsonUser jsonUser)
        {
            try
            {
                TryValidateModel(jsonUser);
                Validator.Validator.ValidateUser(jsonUser);

                User user = Mapper.Mapper.JsonUserToEntity(jsonUser);

                _applicationDbContext.Users.Add(user);
                _applicationDbContext.SaveChanges();
                return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(JsonUser jsonUser, int id)
        {
            try
            {
                TryValidateModel(jsonUser);
                Validator.Validator.ValidateUser(jsonUser);

                var user = _applicationDbContext.Users.SingleOrDefault(x => x.Id == id);

                if (user == null)
                {
                    return NotFound();
                }

                user.Update(jsonUser);

                _applicationDbContext.Users.Remove(user);
                _applicationDbContext.SaveChanges();

                return Ok(user);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var user = _applicationDbContext.Users.SingleOrDefault(x => x.Id == id);

                if (user == null)
                {
                    return NotFound();
                }

                _applicationDbContext.Users.Remove(user);
                _applicationDbContext.SaveChanges();

                return Ok(user);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
