using System;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace ControlerProject
{
    public class RegisterClass
    {
        public string? NomeRegister { get; set; }
        public string? ContatoRegister { get; set; }
        public string? ExtraContatoRegister { get; set; }
        public string? EmailRegister { get; set; }
        public bool SetEmailRegister(string Email)
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
        protected int Idade;


        //REGISTRO DE FUNCIONARIO
        public string? Cargo { get; set; }
        public string? DataAdmissional { get; set; }

        protected string? DataFerias;

        //PARA O LOGIN PRECISAMOS DE UM VALIDADOR EM COMUNICAÇÃO COM O BD ONDE NÃO HAJA NENHUM LOGIN IGUAL
        private string? LoginUser;

        //DEFINIR EM CONJUNTO CRITERIOS DE SENHA
        private string? PasswordUser;




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

            int AnoAtual = DateTime.Now.Year;

            int Idade = AnoAtual - AnoNasc;

            if (Idade < 18)
            {
                return false;
            }

            this.Idade = Idade;
            this.DataNascimento = InDataNascimento;
            return true;
        }

        public bool SetDataAdmissional(string Registro)
        {
            int.TryParse(Registro.Substring(Registro.Length - 4), out int AnoAdmitido);

            string AnoFerias = (AnoAdmitido + 1).ToString();

            this.DataAdmissional = Registro;
            this.DataFerias = Registro.Remove(4, 4) + AnoFerias;
            return true;
        }

    }

    public class FornecedorRegister : RegisterClass
    {
        protected string? CnpjFornecedor;
        protected string? EnderecoFornecedor { get; set; }
        public string? UltimaCompra { get; set; }


    }


}
