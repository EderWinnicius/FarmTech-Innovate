using System;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace ControlerProject
{
    public class RegisterClass
    {
        public string? NomeUser { get; set; }
        public string? ContatoUSer { get; set; }
        public string? ExtraContatoUser { get; set; }
        public string? EmailUser { get; set; }
        public bool SetEmailUser(string Email)
        {
            int ValiderCount = 0;

            for (int i = 0; i < Email.Length; i++)
            {
                if (Email[i] == '@')
                {
                    ValiderCount++;
                }
                if (Email[i] == '.')
                {
                    ValiderCount++;
                }

            }

            if (ValiderCount == 0)
            {
                return false;
            }
            else
            {
                this.EmailUser = Email;
                return true;
            }



        }

    }
    public class UserRegister : RegisterClass
    {
        protected string? CpfUser { get; set; }
        protected string? DataNascimento { get; set; }
        protected int Idade { get; set; }

        private string? LoginUser { get; set; }





        public bool SetCpfUser(string InCpf)
        {
            if (InCpf.Length != 11)
            {
                return false;
            }

            if (InCpf.Distinct().Count() == 1)
            {
                return false;
            }

            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(InCpf[i].ToString()) * (10 - i);
                int digitoVerif1 = soma % 11 < 2 ? 0 : 11 - soma % 11;
                if (int.Parse(InCpf[9].ToString()) != digitoVerif1)
                    return false;
            }

            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(InCpf[i].ToString()) * (11 - i);
                int digitoVerif2 = soma % 11 < 2 ? 0 : 11 - soma % 11;

                // Verificar o segundo dígito verificador
                if (int.Parse(InCpf[10].ToString()) != digitoVerif2)
                    return false;

            }

            this.CpfUser = InCpf;
            return true;
        }

        public bool SetDataNascimento(string InDataNascimento)
        {

            int.TryParse(InDataNascimento.Substring(InDataNascimento.Length - 4), out int AnoNasc);

            int AnoHoje = DateTime.Now.Year;

            int Idade = AnoHoje - AnoNasc;

            if (Idade < 18)
            {
                return false;
            }

            this.Idade = Idade;
            this.DataNascimento = InDataNascimento;
            return true;
        }


    }


}
