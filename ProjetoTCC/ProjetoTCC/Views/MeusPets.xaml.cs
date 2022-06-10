using ProjetoTCC.Model;
using ProjetoTCC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjetoTCC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MeusPets : ContentPage
    {
        private readonly MeusPetsViewModel meusPetsViewModel;

        private readonly Usuario _usuario;

        public MeusPets(Model.Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;

            meusPetsViewModel = new MeusPetsViewModel(_usuario);
            this.BindingContext = meusPetsViewModel;

            //Define a cor da Barra da página
            var navigationPage = Application.Current.MainPage as NavigationPage;
            navigationPage.BarBackgroundColor = Color.FromHex("#00374f");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            meusPetsViewModel.BuscaPets();

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (sender is SwipeView swipe)
            {
                swipe.Open(OpenSwipeItem.RightItems);
            }
        }
    }
}