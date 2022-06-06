using ProjetoTCC.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjetoTCC.ViewModel
{
    public class CadastroViewModel
    {
        public ICommand CadastrarCommand { get; set; }

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

        private Estado estado;
        public Estado Estado
        {
            get { return estado; }
            set
            {
                estado = value;
                this.Notify(nameof(Estado));
                //usuario.CdEstado = estado?.CdEstado;
            }
        }

        private Municipio cidade;
        public Municipio Cidade
        {
            get { return cidade; }
            set
            {
                if (value != null)
                {
                    cidade = value;
                    this.Notify(nameof(Municipio));
                }
            }
        }

        private Usuario usuario;
        public Usuario Usuario
        {
            get
            {
                return usuario;
            }
            set
            {
                usuario = value;
                this.Notify(nameof(Usuario));
            }
        }

        private ObservableCollection<Estado> listaEstados;
        public ObservableCollection<Estado> ListaEstados
        {
            get
            {
                return listaEstados;
            }
            set
            {
                listaEstados = value;
                this.Notify(nameof(ListaEstados));
            }
        }

        private ObservableCollection<Municipio> listacidades;
        public ObservableCollection<Municipio> ListaCidades
        {
            get { return listacidades; }
            set
            {
                listacidades = value;
                this.Notify(nameof(ListaCidades));
            }
        }
        private readonly bool _inEdicao;

        public CadastroViewModel(Usuario usuario)
        {
            this.CadastrarCommand = new Command(async () => await Cadastrar());

            this.Usuario = usuario;

            _inEdicao = usuario != null;


            //Cidade = Usuario.Cidade.ToList();

            ListaEstados = new ObservableCollection<Estado>(Util.Rotinas.ListaEstados());
            ListaCidades = new ObservableCollection<Municipio>();


            if (_inEdicao)
            {
                Estado = ListaEstados.FirstOrDefault(e => e.CdEstado == Usuario.CdEstado);
                DtNascimento = Usuario.dtNascimento?.ToString("dd/MM/yyyy");
                //FiltraListaMunicipios(Estado);
            }
        }

        public async Task FiltraListaMunicipios(Estado estado)
        {
            try
            {
                var listaFiltrada = await API.Correios.MunicipiosGetAsync(estado.CdUfIbge);
                var totalCidades = listaFiltrada.Count;

                ListaCidades.Clear();

                for (int i = 0; i < totalCidades; i++)
                {
                    ListaCidades.Add(new Municipio
                    {
                        Nome = listaFiltrada[i].Nome
                    });
                }

                //if (Usuario.Cidade == null)//(cidade == null)
                //{
                //    Cidade = ListaCidades?.FirstOrDefault();
                //}
                //else
                //{
                //    Cidade = ListaCidades?.FirstOrDefault(x => x.NmMunicipio == Usuario.Cidade);
                //}
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("SSL"))
                {
                    await Global.MessageService.ShowMessageAsync("Erro no serviço do IBGE, tente novamente.", "Erro");
                }
                else
                {
                    throw ex;
                }

            }
        }

        private async Task Cadastrar()
        {
            try
            {
                ValidaCamposUsuario();

                if (!Usuario.EmEdicao)
                {
                    var _usuario = await API.Usuario.CadastrarPostAsync(Usuario);

                    if (_usuario != null)
                    {
                        //salva usuário global 
                        Global.UsuarioGlobal = _usuario;
                    }
                }
                else
                {
                    var _usuario = await API.Usuario.AtualizarPutAsync(Usuario);
                    await Global.MessageService.ShowMessageAsync("Perfil atualizado com sucesso! ", "Meu Perfil");
                }

                //navegar para a tela Home com usuário carregado
                await Global.NavigationService.NavigateToHome(false);
            }
            catch (Exception ex)
            {
                await Global.MessageService.ShowMessageAsync(ex.Message, "Erro");
            }
        }

        private void ValidaCamposUsuario()
        {
            var CPFCNPJString = Util.Rotinas.RetiraCaracteresNaoNumericos(Usuario.CPFCNPJ);

            if (CPFCNPJString.Length > 11)
            {
                Usuario.inPessoaFisica = false;
            }
            else
            {
                Usuario.inPessoaFisica = true;
            }

            if (Usuario.inPessoaFisica)
            {
                if (!Util.Rotinas.CpfValido(CPFCNPJString))
                {
                    throw new Exception("Informe um CPF válido!");
                }
            }
            else
            {
                if (!Util.Rotinas.CnpjValido(CPFCNPJString))
                {
                    throw new Exception("Informe um CNPJ válido!");
                }
            }


            Usuario.CPFCNPJ = CPFCNPJString;


            if (!string.IsNullOrEmpty(DtNascimento))
            {
                var isValido = DateTime.TryParseExact(DtNascimento, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime data);
                if (isValido)
                {
                    Usuario.dtNascimento = data;
                }
                else
                {
                    throw new Exception("Informe uma data de nascimento válida!");
                }
            }

            Usuario.CdEstado = Estado.CdEstado;
            Usuario.Cidade = Cidade.Nome;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
