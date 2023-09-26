using Microsoft.EntityFrameworkCore;
using picpaysimplificado.Enum;
using picpaysimplificado.Jsons;
using System.Runtime.CompilerServices;

namespace picpaysimplificado.Entities
{
    public class User
    {
        public User()
        {
            
        }

        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Document { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public decimal Balance { get; set; }

        public UserType Type { get; set; }

        public void Update(JsonUser jsonUser)
        {
            FirstName = jsonUser.FirstName;
            LastName = jsonUser.LastName;
            Document = jsonUser.Document;
            Email = jsonUser.Email;
            Password = jsonUser.Password;
            Balance = jsonUser.Balance;
        }

    }
}
