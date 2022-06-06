using ProjetoTCC.Model;
using ProjetoTCC.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjetoTCC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PetDetalhe : ContentPage
    {
        private readonly PetDetalheViewModel petDetalheViewModel;
        private readonly UsuarioPet _usuariopet;
        public PetDetalhe(UsuarioPet pet)
        {
            InitializeComponent();
            _usuariopet = pet;


            var img64 = _usuariopet.Usuario.imgFotoPerfil.Replace("data:image/jpeg;base64,", "") ?? "";
            frameFotoUsuario.HorizontalOptions = LayoutOptions.FillAndExpand;
            frameFotoUsuario.VerticalOptions = LayoutOptions.FillAndExpand;
            frameFotoUsuario.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(img64)));

            petDetalheViewModel = new PetDetalheViewModel(_usuariopet);
            this.BindingContext = petDetalheViewModel;
        }


    }
}