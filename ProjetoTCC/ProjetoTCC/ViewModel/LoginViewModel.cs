using ProjetoTCC.Views;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjetoTCC.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
            }
        }

        private string senha;
        public string Senha
        {
            get { return senha; }
            set
            {
                senha = value;
            }
        }

        public ICommand LoginCommand { get; set; }
        public ICommand ConvidadoCommand { get; set; }
        public ICommand CadastroCommand { get; set; }
        public ICommand SenhaCommand { get; set; }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                this.Notify(nameof(IsBusy));
            }
        }

        public LoginViewModel()
        {
            this.CadastroCommand = new Command(this.Cadastrar);
            this.LoginCommand = new Command(this.Logar);
            this.ConvidadoCommand = new Command(this.LogarConvidado);
        }

        private async void Cadastrar()
        {
            try
            {
                await Global.NavigationService.NavigateToCadastro();
            }
            catch (Exception ex)
            {
                await Global.MessageService.ShowMessageAsync(ex.Message, "Erro");
            }
        }

        private async void Logar()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;

                var usuarioAutenticado = await Logar(this.Email, this.Senha);
                if (usuarioAutenticado != null)
                {
                    ProjetoTCC.Global.UsuarioGlobal = usuarioAutenticado;
                    await Global.NavigationService.NavigateToHome(false);
                }
            }
            catch (Exception ex)
            {
                await Global.MessageService.ShowMessageAsync(ex.Message, "Erro");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void LogarConvidado()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;


                await Global.NavigationService.NavigateToHome(true);

            }
            catch (Exception ex)
            {
                await Global.MessageService.ShowMessageAsync(ex.Message, "Erro");
            }
            finally
            {
                IsBusy = false;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
