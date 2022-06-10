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
    public class MeusPetsViewModel : INotifyPropertyChanged
    {
        public ICommand ExcluirPetCommand { get; set; }
        public ICommand EditarPetCommand { get; set; }

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

        private ObservableCollection<UsuarioPet> listaPetUsuario;
        public ObservableCollection<UsuarioPet> ListaPetUsuario
        {
            get { return listaPetUsuario; }
            set
            {
                listaPetUsuario = value;
                this.Notify(nameof(ListaPetUsuario));
            }
        }

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
        public MeusPetsViewModel(Usuario usuario)
        {
            Usuario = usuario;

            this.ExcluirPetCommand = new Command<UsuarioPet>(async (usuarioPet) => await ExcluirPet(usuarioPet));
            this.EditarPetCommand = new Command<UsuarioPet>(async (usuarioPet) => await EditarPet(usuarioPet));
        }

        private async Task ExcluirPet(UsuarioPet usuarioPet)
        {
            try
            {
                var resposta = await Global.MessageService.ShowMessageYesNoAsync(usuarioPet.nmPet, "Deseja excluir o Pet?");
                if (resposta)
                {
                    if (IsBusy)
                    {
                        return;
                    }

                    IsBusy = true;

                    await API.UsuarioPet.ExcluirPetDeleteAsync(usuarioPet);
                    ListaPetUsuario.Remove(usuarioPet);
                    this.Notify(nameof(ListaPetUsuario));
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

        private async Task EditarPet(UsuarioPet usuarioPet)
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;

                await Global.NavigationService.NavigateToCadastrarPet(usuarioPet);

                this.Notify(nameof(ListaPetUsuario));

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

        public async void BuscaPets()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;

                Usuario.CPFCNPJ = Usuario.CPFCNPJ.ToString().Replace(".", "").Replace("/", "").Replace("-", "");

                Usuario = await ProjetoTCC.API.Usuario.UsuarioCPFGetAsync(Usuario);
                                
                ListaPetUsuario = new ObservableCollection<UsuarioPet>(Usuario.UsuarioPets.OrderByDescending(u => u.dtCadastro));

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
