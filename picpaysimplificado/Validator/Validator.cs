using picpaysimplificado.Enum;
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

                Regex regexEmail = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                if (!regexEmail.IsMatch(jsonUser.Email))
                    throw new Exception(string.Format("Parâmetro {0} inválido!", nameof(jsonUser.Email)));

                int.TryParse(jsonUser.Type, out int type);

                if (!System.Enum.IsDefined(typeof(UserType), type))
                    throw new Exception(string.Format("Parâmetro {0} inválido!", nameof(jsonUser.Type)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
