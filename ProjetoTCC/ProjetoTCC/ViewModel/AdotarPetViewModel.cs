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
    public class AdotarPetViewModel : INotifyPropertyChanged
    {
        public ICommand AdotarPetCommand { get; set; }
        public ICommand DetalheDoPetCommand { get; set; }

        private Model.UsuarioPet usuarioPet;
        public Model.UsuarioPet UsuarioPet
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

        private ObservableCollection<UsuarioPet> listaPets;
        public ObservableCollection<UsuarioPet> ListaPets
        {
            get
            {
                return listaPets;
            }
            set
            {
                listaPets = value;
                this.Notify(nameof(ListaPets));
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
        private bool Convidado = false;

        public AdotarPetViewModel(bool convidado)
        {
            Convidado = convidado;
            this.AdotarPetCommand = new Command(async () => await AdotarPet());
            this.DetalheDoPetCommand = new Command<UsuarioPet>(async (pet) => await DetalhesDoPet(pet));

            ListaPets = new ObservableCollection<UsuarioPet>();

            RetornaPetsParaAdocao();

        }
        public async Task RetornaPetsParaAdocao()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;

                List<UsuarioPet> _listaPets = await API.UsuarioPet.ListaPetsGetAsync();
                _listaPets = _listaPets.Where(p => p.dtAdotado == null).ToList();
                ListaPets = new ObservableCollection<UsuarioPet>(_listaPets);

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
        private async Task AdotarPet()
        {
            try
            {
                await RetornaPetsParaAdocao();
            }
            catch (Exception ex)
            {
                await Global.MessageService.ShowMessageAsync(ex.Message, "Erro");
            }
        }

        private async Task DetalhesDoPet(UsuarioPet pet)
        {
            if (!Convidado)
            {
                //Navigation.PushModalAsync(new PetDetalhe(_usuarioPet));
                await Global.NavigationService.NavigateToPetDetalhe(pet);
            }
            else
            {
                Global.MessageService.ShowMessageAsync("Para esta funcionalidade é necessário estar logado.", "Faça o Login");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
