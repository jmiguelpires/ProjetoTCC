using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using ProjetoTCC.ViewModel;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using System.IO;
using ProjetoTCC.Model;

namespace ProjetoTCC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdotarPet : ContentPage

    {
        private readonly AdotarPetViewModel adotarPetViewModel;

        private readonly UsuarioPet _usuarioPet;
        public AdotarPet(bool convidado)
        {
            InitializeComponent();
            adotarPetViewModel = new AdotarPetViewModel(convidado);
            this.BindingContext = adotarPetViewModel;

            //_usuarioPet = usuarioPet;

            //Define a cor da Barra da página
            var navigationPage = Application.Current.MainPage as NavigationPage;
            navigationPage.BarBackgroundColor = Color.FromHex("#00374f");
            
            
        }

        //private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        //{
        //    Navigation.PushModalAsync(new PetDetalhe(_usuarioPet));            
        //}
    }
}