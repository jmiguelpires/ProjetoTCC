using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.ComponentModel;
using ProjetoTCC.ViewModel;



namespace ProjetoTCC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        private readonly AdotarPetViewModel adotarPetViewModel;
        private bool Convidado = false;
        public Home(bool convidado)
        {
            InitializeComponent();
            Convidado = convidado;
        }

        private void BotaoAdotarPet_Clicked(object sender, EventArgs e)
        {
            Global.NavigationService.NavigateToAdotarPet(Convidado);
        }

        private void BotaoCadastrarPet_Clicked(object sender, EventArgs e)
        {
            if (!Convidado)
            {
                Global.NavigationService.NavigateToCadastrarPet();
            }
            else
            {
                Global.MessageService.ShowMessageAsync("Para esta funcionalidade é necessário estar logado.", "Faça o Login");
            }
        }

        private void BotaoMeusPets_Clicked(object sender, EventArgs e)
        {

            if (!Convidado)
            {
                Global.NavigationService.NavigateToMeusPets(Global.UsuarioGlobal);
            }
            else
            {
                Global.MessageService.ShowMessageAsync("Para esta funcionalidade é necessário estar logado.", "Faça o Login");
            }
        }

        private void BotaoMeuPerfil_Clicked(object sender, EventArgs e)
        {
            if (!Convidado)
            {
                Global.NavigationService.NavigateToCadastro(Global.UsuarioGlobal);
            }
            else
            {
                Global.MessageService.ShowMessageAsync("Para esta funcionalidade é necessário estar logado.", "Faça o Login");
            }
        }

    }
}