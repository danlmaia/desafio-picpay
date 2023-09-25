using picpaysimplificado.Enums;

namespace picpaysimplificado.Jsons
{
    public class JsonUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Document { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public decimal Balance { get; set; }

        public string Type { get; set; }
    }
}
