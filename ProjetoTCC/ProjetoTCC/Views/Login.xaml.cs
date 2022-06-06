using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoTCC.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjetoTCC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private readonly LoginViewModel loginViewModel;

        public Login()
        {
            InitializeComponent();

            loginViewModel = new LoginViewModel();
            this.BindingContext = loginViewModel;
        }
    }
}