using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace ProjetoTCC.Model
{
    public class Usuario
    {
        [PrimaryKey]
        public string CPFCNPJ { get; set; }
        [Ignore]
        public virtual ICollection<UsuarioPet> UsuarioPets { get; set; }
        public string nmUsuario { get; set; }
        public DateTime? dtNascimento { get; set; }
        public string imgFotoPerfil { get; set; }
        public bool inPessoaFisica { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public string sexo { get; set; }
        public string telefone { get; set; }
        public string CdEstado { get; set; }
        public string? Cidade { get; set; }

        #region jsonIgnores
        [JsonIgnore]
        public string dtNascimentoEscrito
        {
            get
            {
                return dtNascimento?.ToString("dd/MM/yyyy");

            }
        }
        [JsonIgnore]
        public bool SexoMasculino
        {
            get
            {
                return sexo == "M";
            }
            set
            {
                if (value)
                {
                    sexo = "M";
                }
            }
        }
        [JsonIgnore]
        public bool SexoFeminino
        {
            get
            {
                return sexo == "F";
            }
            set
            {
                if (value)
                {
                    sexo = "F";
                }
            }
        }
        [JsonIgnore]
        public bool SexoOutros
        {
            get
            {
                return sexo == "X";
            }
            set
            {
                if (value)
                {
                    sexo = "X";
                }
            }
        }
        [JsonIgnore]
        public bool EmEdicao { get; set; }
        [JsonIgnore]
        public ImageSource ImgSourcePerfil
        {
            get
            {
                if (string.IsNullOrEmpty(imgFotoPerfil))
                {
                    return null;
                }
                //string img64 = "";
                string extensaoImagem = "";

                if (imgFotoPerfil.Contains("data:image/png;base64"))
                {
                    extensaoImagem = "png";
                }
                else if (imgFotoPerfil.Contains("data:image/jpeg;base64"))
                {
                    extensaoImagem = "jpeg";
                }
                else if (imgFotoPerfil.Contains("data:image/jpg;base64"))
                {
                    extensaoImagem = "jpg";
                }

                var img64 = imgFotoPerfil.Replace($"data:image/{extensaoImagem};base64,", "") ?? "";

                return ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(img64)));
            }
        }
        [JsonIgnore]
        public DateTime? DtUltimoLogin { get; set; }
        #endregion 

    }

}