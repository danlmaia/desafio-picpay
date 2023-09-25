using Microsoft.EntityFrameworkCore;
using picpaysimplificado.Enum;

namespace picpaysimplificado.Entities
{
    public class User
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Document { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public decimal Balance { get; set; }

        public UserType Type { get; set; }

    }
}
