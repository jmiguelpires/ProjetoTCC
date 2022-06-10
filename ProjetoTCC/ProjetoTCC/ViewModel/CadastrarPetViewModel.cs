using ProjetoTCC.Model;
using ProjetoTCC.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjetoTCC.ViewModel
{
    public class CadastrarPetViewModel : INotifyPropertyChanged
    {
        public ICommand CadastrarPetCommand { get; set; }

        private string dtNascimento;
        public string DtNascimento
        {
            get { return dtNascimento; }
            set
            {
                dtNascimento = value;
                this.Notify(nameof(DtNascimento));
            }
        }

        private string dtAdotado;
        public string DtAdotado
        {
            get { return dtAdotado; }
            set
            {
                dtAdotado = value;
                this.Notify(nameof(DtAdotado));
            }
        }

        private UsuarioPet usuarioPet;
        public UsuarioPet UsuarioPet
        {
            get
            {
                return usuarioPet;
            }
            set
            {
                usuarioPet = value;
                this.Notify(nameof(UsuarioPet));
            }
        }

        private Racas raca;
        public Racas Raca
        {
            get { return raca; }
            set
            {
                raca = value;
                this.Notify(nameof(Raca));
            }
        }

        private ObservableCollection<Racas> listaRacas;
        public ObservableCollection<Racas> ListaRacas
        {
            get
            {
                return listaRacas;
            }
            set
            {
                listaRacas = value;
                this.Notify(nameof(ListaRacas));
            }
        }

        public CadastrarPetViewModel(UsuarioPet usuarioPet)
        {
            this.CadastrarPetCommand = new Command(async () => await CadastrarPet());

            this.UsuarioPet = usuarioPet;

            if (UsuarioPet.EmEdicao)
            {
                TipoPet(UsuarioPet.tipoPet);
                Raca = ListaRacas.FirstOrDefault(r => r.raca == UsuarioPet.raca);
                DtNascimento = UsuarioPet.dtNascimento.ToString("dd/MM/yyyy");
                DtAdotado = UsuarioPet.dtAdotado?.ToString("dd/MM/yyyy");
            }
        }

        private async Task CadastrarPet()
        {
            try
            {
                ValidaCamposPet();

                if (!UsuarioPet.EmEdicao)
                {
                    UsuarioPet.dtCadastro = DateTime.Today;
                    UsuarioPet.CPFCNPJ = Global.UsuarioGlobal.CPFCNPJ.ToString().Replace(".", "").Replace("/", "").Replace("-", "");
                    await API.UsuarioPet.CadastrarPostAsync(UsuarioPet);
                    await Global.MessageService.ShowMessageAsync("Pet cadastrado com sucesso!", "Parabéns!"); ;
                }
                else
                {
                    await API.UsuarioPet.AtualizarPetPutAsync(UsuarioPet);
                    await Global.MessageService.ShowMessageAsync("Pet atualizado com sucesso!", "Show!"); ;
                }
                await Global.NavigationService.NavigateToHome(false);
            }

            catch (Exception ex)
            {
                await Global.MessageService.ShowMessageAsync(ex.Message, "Erro"); ;
            }
        }

        public async Task TipoPet(string tipoPet)
        {
            UsuarioPet.tipoPet = tipoPet;

            //Preenche lista de Raças de acordo com o tipoPet (G ou C)
            if (tipoPet == "C")
            {
                ListaRacas = new ObservableCollection<Racas>(Util.Rotinas.ListaRacasCachorro());
            }
            else if (tipoPet == "G")
            {
                ListaRacas = new ObservableCollection<Racas>(Util.Rotinas.ListaRacasGato());
            }

        }

        public async Task PortePet(string portePet)
        {
            UsuarioPet.porte = portePet;
        }

        public async Task SexoPet(string sexoPet)
        {
            UsuarioPet.sexo = sexoPet;
        }

        public async Task RacaPet(Racas racas)
        {
            UsuarioPet.raca = racas.raca;
        }

        #region personalidades
        public async Task Personalidade_Docil(bool docil)
        {
            UsuarioPet.inPersonalidadeDocil = docil;
        }

        public async Task Personalidade_Tranquilo(bool tranquilo)
        {
            UsuarioPet.inPersonalidadeTranquilo = tranquilo;
        }

        public async Task Personalidade_Alerta(bool alerta)
        {
            UsuarioPet.inPersonalidadeAlerta = alerta;
        }

        public async Task Personalidade_Agressivo(bool agressivo)
        {
            UsuarioPet.inPersonalidadeAgressivo = agressivo;
        }

        public async Task Personalidade_Ativos(bool pouco, bool medio, bool muito)
        {
            UsuarioPet.inPersonalidadeAtivoMinimo = pouco;
            UsuarioPet.inPersonalidadeAtivoMedio = medio;
            UsuarioPet.inPersonalidadeAtivoMaximo = muito;
        }

        public async Task Personalidade_Carinhoso(bool carinhoso)
        {
            UsuarioPet.inPersonalidadeCarinhoso = carinhoso;
        }

        public async Task Personalidade_Assustado(bool assustado)
        {
            UsuarioPet.inPersonalidadeAssustado = assustado;
        }

        public async Task Personalidade_Preguicoso(bool preguicoso)
        {
            UsuarioPet.inPersonalidadePreguicoso = preguicoso;
        }

        public async Task Personalidade_Explorador(bool explorador)
        {
            UsuarioPet.inPersonalidadeExplorador = explorador;
        }

        public async Task Personalidade_Curioso(bool curioso)
        {
            UsuarioPet.inPersonalidadeCurioso = curioso;
        }

        public async Task Personalidade_Medroso(bool medroso)
        {
            UsuarioPet.inPersonalidadeMedroso = medroso;
        }

        #endregion personalidades

        private void ValidaCamposPet()
        {
            if (!string.IsNullOrEmpty(DtNascimento))
            {
                var isValido = DateTime.TryParseExact(DtNascimento, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime data);
                if (isValido)
                {
                    UsuarioPet.dtNascimento = data;
                }
                else
                {
                    throw new Exception("Informe uma data de nascimento válida!");
                }

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
