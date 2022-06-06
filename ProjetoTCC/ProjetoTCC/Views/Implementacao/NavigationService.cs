using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
//using ZXing.QrCode.Internal;

namespace ProjetoTCC.Views.Implementacao
{
    public class NavigationService : ProjetoTCC.Interface.INavigationService
    {
        public virtual async Task NavigateToCadastro(Model.Usuario usuarioCarregado = null)
        {
            INavigation _navigation = Application.Current.MainPage.Navigation;
            Page _page = _navigation.NavigationStack[_navigation.NavigationStack.Count - 1];

            if (_page != null)
            {
                if (!(_page is Cadastro))
                {
                    await _navigation.PushAsync(new Cadastro(usuarioCarregado), true);
                }
            }
        }

        public virtual async Task NavigateToLogin()
        {
            INavigation _navigation = Application.Current.MainPage.Navigation;
            Page _page = _navigation.NavigationStack[_navigation.NavigationStack.Count - 1];

            if (_page != null)
            {
                if (!(_page is Login))
                {
                    await _navigation.PushAsync(new Login(), true);
                }
            }
        }

        public virtual async Task NavigateToHome(bool convidado)
        {
            INavigation _navigation = Application.Current.MainPage.Navigation;
            Page _page = _navigation.NavigationStack[_navigation.NavigationStack.Count - 1];

            if (_page != null)
            {
                if (!(_page is Home))
                {
                    await _navigation.PushAsync(new Home(convidado), true);
                    
                }
            }
        }

        public virtual async Task NavigateToAdotarPet(bool convidado)
        {
            INavigation _navigation = Application.Current.MainPage.Navigation;
            Page _page = _navigation.NavigationStack[_navigation.NavigationStack.Count - 1];

            if (_page != null)
            {
                if (!(_page is AdotarPet))
                {
                    await _navigation.PushAsync(new AdotarPet(convidado), true);
                }
            }
        }

        public virtual async Task NavigateToPetDetalhe(Model.UsuarioPet pet)
        {
            INavigation _navigation = Application.Current.MainPage.Navigation;
            Page _page = _navigation.NavigationStack[_navigation.NavigationStack.Count - 1];

            if (_page != null)
            {
                if (!(_page is PetDetalhe))
                {
                    //depois da tela finalizada, trocar para PushModalAsync
                    await _navigation.PushAsync(new PetDetalhe(pet), true);
                }
            }
        }

        public virtual async Task NavigateToCadastrarPet(Model.UsuarioPet usuarioPet)
        {
            INavigation _navigation = Application.Current.MainPage.Navigation;
            Page _page = _navigation.NavigationStack[_navigation.NavigationStack.Count - 1];

            if (_page != null)
            {
                if (!(_page is CadastrarPet))
                {
                    //depois da tela finalizada, trocar para PushModalAsync
                    await _navigation.PushAsync(new CadastrarPet(usuarioPet), true);
                }
            }
        }

        public virtual async Task NavigateToMeusPets(Model.Usuario usuario)
        {
            INavigation _navigation = Application.Current.MainPage.Navigation;
            Page _page = _navigation.NavigationStack[_navigation.NavigationStack.Count - 1];

            if (_page != null)
            {
                if (!(_page is MeusPets))
                {
                    //depois da tela finalizada, trocar para PushModalAsync
                    await _navigation.PushAsync(new MeusPets(usuario), true);
                }
            }
        }

    }
}
