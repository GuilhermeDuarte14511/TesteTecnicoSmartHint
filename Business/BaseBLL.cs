using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business
{

    public class BaseBLL
    {
        public string GerarSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt();
        }

        public string CriptografarSenha(string senha, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha, salt);
        }

        public bool VerificarSenha(string senhaDigitada, string senhaCriptografada)
        {
            return BCrypt.Net.BCrypt.Verify(senhaDigitada, senhaCriptografada);
        }
    }

}
