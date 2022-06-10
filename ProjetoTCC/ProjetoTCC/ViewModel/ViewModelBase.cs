using ProjetoTCC.Interface;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjetoTCC.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase()
        {

        }

        private Model.Usuario usuario;
        public Model.Usuario Usuario
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


        public async Task<Model.Usuario> Logar(string Email, string Senha)
        {
            Model.Usuario usuarioAutenticado = null;

            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Senha))
            {
                await Global.MessageService.ShowMessageAsync("Informe Usuário e Senha", "Login");
            }
            else
            {
                var _senha = Senha;

                var usuario = new Model.Usuario
                {
                    email = Email,
                    senha = _senha
                };

                if (Util.Rotinas.EmailValido(Email))
                {
                    usuario.email = Email;
                }
                else
                {
                    await Global.MessageService.ShowMessageAsync("E-mail inválido!\nTente novamente", "Login");
                }

                try
                {
                    usuarioAutenticado = await ProjetoTCC.API.Usuario.GetUsuarioLogin(usuario);

                }
                catch (Exception ex)
                {
                    await Global.MessageService.ShowMessageAsync(ex.Message, "Erro");
                }
            }

            return usuarioAutenticado;
        }

        public async void Logout()
        {
            try

            {
                var deslogar = await Global.MessageService.ShowMessageYesNoAsync("Deseja desconectar?", "Log Off");
                if (deslogar)
                {
                    Util.Global.IUsuario.ExcluirUsuarioLocal();
                    
                    Global.UsuarioGlobal = null;

                    await Global.NavigationService.NavigateToLogin();
                }
            }
            catch (Exception e)
            {
                await Global.MessageService.ShowMessageAsync(e.Message, "Erro");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

