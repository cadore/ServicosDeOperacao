
namespace Siscom.Entities
{
    public class Bairros
    {
        public override string ToString()
        {
            return this.bairro;
        }
        public long id { get; set; }

        public string bairro { get; set; }

        public Cidades cidade_instc { get; set; }
    }
}
