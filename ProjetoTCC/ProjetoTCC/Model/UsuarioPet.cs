using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace ProjetoTCC.Model
{
    public class UsuarioPet
    {
        public long idPet { get; set; }
        public virtual Usuario Usuario { get; set; }
        public string CPFCNPJ { get; set; }
        public string tipoPet { get; set; }
        public string nmPet { get; set; }
        public DateTime dtNascimento { get; set; }
        public string sexo { get; set; }
        public string raca { get; set; }
        public string porte { get; set; }
        public decimal? pesokg { get; set; }
        public bool inVacinado { get; set; }
        public bool inVermifugado { get; set; }
        public bool inPedigree { get; set; }
        public bool inCuidadoEspecial { get; set; }
        public bool inPersonalidadeDocil { get; set; }
        public bool inPersonalidadeTranquilo { get; set; }
        public bool inPersonalidadeAlerta { get; set; }
        public bool inPersonalidadeAgressivo { get; set; }
        public bool inPersonalidadeAtivoMinimo { get; set; }
        public bool inPersonalidadeAtivoMedio { get; set; }
        public bool inPersonalidadeAtivoMaximo { get; set; }
        public bool inPersonalidadeCarinhoso { get; set; }
        public bool inPersonalidadeAssustado { get; set; }
        public bool inPersonalidadePreguicoso { get; set; }
        public bool inPersonalidadeExplorador { get; set; }
        public bool inPersonalidadeCurioso { get; set; }
        public bool inPersonalidadeMedroso { get; set; }
        public string ImgPet1 { get; set; }
        public string ImgPet2 { get; set; }
        public string ImgPet3 { get; set; }
        public string ImgPet4 { get; set; }
        public string txDescricao { get; set; }
        public DateTime dtCadastro { get; set; }
        public DateTime? dtAdotado { get; set; }

        #region jsonIgnores

        [JsonIgnore]
        public string idade
        {
            get
            {
                var hoje = DateTime.Today;
                var DifData = hoje.Subtract(dtNascimento);
                var QtdDias = DifData.Days;
                int idade = 0;
                string nmPeriodo = "";

                if (QtdDias < 365)
                {
                    idade = QtdDias / 30;
                    nmPeriodo = "meses";
                }
                else if (QtdDias >= 365 && QtdDias <= 729)
                {
                    idade = 1;
                    nmPeriodo = "ano";
                }
                else if (QtdDias >= 730)
                {
                    idade = QtdDias / 365;
                    nmPeriodo = "anos";
                }
                else
                {
                    nmPeriodo = "Idade indefinida";
                }

                return $"{idade} {nmPeriodo}";

            }
        }

        [JsonIgnore]
        public string tipoPetEscrito
        {
            get
            {
                if (tipoPet == "C")
                {
                    return "Cachorro";
                }
                else if (tipoPet == "G")
                {
                    return "Gato";
                }
                else
                {
                    return "";
                }

            }
        }

        [JsonIgnore]
        public string sexoEscrito
        {
            get
            {
                if (sexo == "M")
                {
                    return "Macho";
                }
                else if (sexo == "F")
                {
                    return "Fêmea";
                }
                else
                {
                    return "Indefinido";
                }

            }
        }

        [JsonIgnore]
        public string porteEscrito
        {
            get
            {
                if (porte == "P")
                {
                    return "Pequeno";
                }
                else if (porte == "M")
                {
                    return "Médio";
                }
                else if (porte == "G")
                {
                    return "Grande";
                }
                else
                {
                    return "Indefinido";
                }

            }
        }

        [JsonIgnore]
        public string dtNascimentoEscrito
        {
            get
            {
                return dtNascimento.ToString("dd/MM/yyyy");

            }
        }

        [JsonIgnore]
        public string dtCadastroEscrito
        {
            get
            {
                return dtCadastro.ToString("dd/MM/yyyy");

            }
        }

        [JsonIgnore]
        public string dtAdotadoEscrito
        {
            get
            {
                return dtAdotado?.ToString("dd/MM/yyyy");

            }
        }

        [JsonIgnore]
        public string thumbColorSwitch_inVacinado
        {
            get
            {
                if (inVacinado)
                {
                    return "Green";
                }
                else
                {
                    return "LightGray";
                }

            }
        }

        [JsonIgnore]
        public string thumbColorSwitch_inVermifugado
        {
            get
            {
                if (inVermifugado)
                {
                    return "Green";
                }
                else
                {
                    return "LightGray";
                }

            }
        }

        [JsonIgnore]
        public string thumbColorSwitch_inPedigree
        {
            get
            {
                if (inPedigree)
                {
                    return "Green";
                }
                else
                {
                    return "LightGray";
                }

            }
        }

        [JsonIgnore]
        public string thumbColorSwitch_inCuidadoEspecial
        {
            get
            {
                if (inCuidadoEspecial)
                {
                    return "Green";
                }
                else
                {
                    return "LightGray";
                }

            }
        }

        [JsonIgnore]
        public string onColorSwitch_inVacinado
        {
            get
            {
                if (inVacinado)
                {
                    return "LightGreen";
                }
                else
                {
                    return "Gray";
                }

            }
        }

        [JsonIgnore]
        public string onColorSwitch_inVermifugado
        {
            get
            {
                if (inVermifugado)
                {
                    return "LightGreen";
                }
                else
                {
                    return "Gray";
                }

            }
        }

        [JsonIgnore]
        public string onColorSwitch_inPedigree
        {
            get
            {
                if (inPedigree)
                {
                    return "LightGreen";
                }
                else
                {
                    return "Gray";
                }

            }
        }

        [JsonIgnore]
        public string onColorSwitch_inCuidadoEspecial
        {
            get
            {
                if (inCuidadoEspecial)
                {
                    return "LightGreen";
                }
                else
                {
                    return "Gray";
                }

            }
        }

        [JsonIgnore]
        public string iconePet_AdotarPet
        {
            get
            {
                if (tipoPet == "C")
                {
                    return "iconCachorro.png";
                }
                else if (tipoPet == "G")
                {
                    return "iconGato.png";
                }
                else
                {
                    return "";
                }

            }
        }

        [JsonIgnore]
        public ImageSource ImgSourcePet1
        {
            get
            {
                if (string.IsNullOrEmpty(ImgPet1))
                {
                    return null;
                }
                string extensaoImagem = "";

                if (ImgPet1.Contains("data:image/png;base64"))
                {
                    extensaoImagem = "png";
                }
                else if (ImgPet1.Contains("data:image/jpeg;base64"))
                {
                    extensaoImagem = "jpeg";
                }
                else if (ImgPet1.Contains("data:image/jpg;base64"))
                {
                    extensaoImagem = "jpg";
                }

                var img64 = ImgPet1.Replace($"data:image/{extensaoImagem};base64,", "") ?? "";

                return ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(img64)));
            }
        }

        [JsonIgnore]
        public ImageSource ImgSourcePet2
        {
            get
            {
                if (string.IsNullOrEmpty(ImgPet2))
                {
                    return null;
                }
                string extensaoImagem = "";

                if (ImgPet2.Contains("data:image/png;base64"))
                {
                    extensaoImagem = "png";
                }
                else if (ImgPet2.Contains("data:image/jpeg;base64"))
                {
                    extensaoImagem = "jpeg";
                }
                else if (ImgPet2.Contains("data:image/jpg;base64"))
                {
                    extensaoImagem = "jpg";
                }

                var img64 = ImgPet2.Replace($"data:image/{extensaoImagem};base64,", "") ?? "";

                return ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(img64)));
            }
        }

        [JsonIgnore]
        public ImageSource ImgSourcePet3
        {
            get
            {
                if (string.IsNullOrEmpty(ImgPet3))
                {
                    return null;
                }
                string extensaoImagem = "";

                if (ImgPet3.Contains("data:image/png;base64"))
                {
                    extensaoImagem = "png";
                }
                else if (ImgPet3.Contains("data:image/jpeg;base64"))
                {
                    extensaoImagem = "jpeg";
                }
                else if (ImgPet3.Contains("data:image/jpg;base64"))
                {
                    extensaoImagem = "jpg";
                }

                var img64 = ImgPet3.Replace($"data:image/{extensaoImagem};base64,", "") ?? "";

                return ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(img64)));
            }
        }

        [JsonIgnore]
        public ImageSource ImgSourcePet4
        {
            get
            {
                if (string.IsNullOrEmpty(ImgPet4))
                {
                    return null;
                }
                string extensaoImagem = "";

                if (ImgPet4.Contains("data:image/png;base64"))
                {
                    extensaoImagem = "png";
                }
                else if (ImgPet4.Contains("data:image/jpeg;base64"))
                {
                    extensaoImagem = "jpeg";
                }
                else if (ImgPet1.Contains("data:image/jpg;base64"))
                {
                    extensaoImagem = "jpg";
                }

                var img64 = ImgPet4.Replace($"data:image/{extensaoImagem};base64,", "") ?? "";

                return ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(img64)));
            }
        }

        [JsonIgnore]
        public bool EmEdicao { get; set; }

        [JsonIgnore]
        public string frameSituacaoPet_Color 
        {
            get
            {
                if (string.IsNullOrEmpty(dtAdotado.ToString()))
                {
                    return "#7a2c2c";
                }
                else
                {
                    return "#53B175";
                }

            }
        }

        [JsonIgnore]
        public string labelSituacaoPet_Text
        {
            get
            {
                if (string.IsNullOrEmpty(dtAdotado.ToString()))
                {
                    return "Não adotado";
                }
                else
                {
                    return "Adotado";
                }

            }
        }

        [JsonIgnore]
        public int lblnmPet_FontSize
        {
            get
            {
                if (nmPet.Length <= 6)
                {
                    return 14;
                }
                else
                {
                    return 12;
                }

            }
        }

        #endregion

    }

}