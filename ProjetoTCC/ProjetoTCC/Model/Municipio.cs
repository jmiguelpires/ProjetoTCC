
namespace ProjetoTCC.Model
{
    public class Municipio
    {
        public string CdEstado { get; set; }
        public int CdUfIbge { get; set; }

        public string NmMunicipio { get; set; }
        public string Nome
        {
            get
            {
                return NmMunicipio;
            }
            set
            {
                NmMunicipio = value;
            }
        }

        public int CdMunicipio { get; set; }
        public int Id
        {
            get
            {
                return CdMunicipio;
            }
            set
            {
                CdMunicipio = value;
            }
        }
    }


}
