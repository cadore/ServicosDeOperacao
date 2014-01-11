using System;
namespace Siscom.Entities
{
    public class Funcionarios
    {
        public long id { get; set; }

        public string nome { get; set; }

        public string senha { get; set; }

        public decimal salario_fixo { get; set; }

        public decimal comissao { get; set; }

        public bool vendas { get; set; }

        public bool administrador { get; set; }

        public bool relatorios { get; set; }

        public bool acesso_inativo { get; set; }

        public bool inativo { get; set; }

        public override string ToString()
        {
            return this.nome;
        }
        public static Funcionarios ToFuncionarios(Object o)
        {
            Funcionarios c = (Funcionarios)o;
            return c;
        }
    }
}
