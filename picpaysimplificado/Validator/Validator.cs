using picpaysimplificado.Enums;
using picpaysimplificado.Jsons;
using System.Text.RegularExpressions;

namespace picpaysimplificado.Validator
{
    public static class Validator
    {
        public static void ValidateUser(JsonUser jsonUser)
        {
            try
            {
                if (string.IsNullOrEmpty(jsonUser.FirstName))
                    throw new Exception(string.Format("Parâmetro {0} não informado!", nameof(jsonUser.FirstName)));

                if (string.IsNullOrEmpty(jsonUser.LastName))
                    throw new Exception(string.Format("Parâmetro {0} não informado!", nameof(jsonUser.LastName)));

                Regex regexCPF = new Regex(@"([0-9]{2}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[\\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[-]?[0-9]{2})$");
                if (!regexCPF.IsMatch(jsonUser.Document))
                    throw new Exception(string.Format("Parâmetro {0} inválido!", nameof(jsonUser.Document)));

                Regex email = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                if (!regexCPF.IsMatch(jsonUser.Email))
                    throw new Exception(string.Format("Parâmetro {0} inválido!", nameof(jsonUser.Email)));

                if (!System.Enum.IsDefined(typeof(UserType), jsonUser.Type))
                    throw new Exception(string.Format("Parâmetro {0} inválido!", nameof(jsonUser.Type)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
