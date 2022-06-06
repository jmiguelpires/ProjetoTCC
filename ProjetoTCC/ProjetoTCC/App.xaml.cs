using ProjetoTCC.Interface;
using System;
using Xamarin.Essentials;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using ProjetoTCC.Views;

namespace ProjetoTCC
{
    public partial class App : Application
    {
        private bool _FirstLaunch;
        private bool _FirstLaunchCurrent;
        private string _CurrentVersion;

        public App()
        {
            InitializeComponent();

            //injecao de dependencia
            DependencyService.Register<IMessageService, ProjetoTCC.Views.Implementacao.MessageService>();
            DependencyService.Register<INavigationService, ProjetoTCC.Views.Implementacao.NavigationService>();
            DependencyService.Register<Util.IUsuarioLocal, Dal.UsuarioLocal>();

            Util.Global.IUsuario = DependencyService.Get<Util.IUsuarioLocal>();

            MainPage = new ContentPage();

            UsuarioLogado();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private async void UsuarioLogado()
        {
            try
            {
                // First time ever launched application
                _FirstLaunch = VersionTracking.IsFirstLaunchEver;

                // First time launching current version
                _FirstLaunchCurrent = VersionTracking.IsFirstLaunchForCurrentVersion;

                // Current app version (2.0.0)
                _CurrentVersion = VersionTracking.CurrentVersion;

                Util.Global.IUsuario = DependencyService.Get<Util.IUsuarioLocal>();

                Global.MessageService = DependencyService.Get<IMessageService>();
                Global.NavigationService = DependencyService.Get<INavigationService>();

                Model.Usuario usuario = null;
                try
                {
                    usuario = Util.Global.IUsuario.BuscarUsuarioLocal();

                }
                catch { }

                if (usuario != null)
                {
                    try
                    {
                        usuario = await ProjetoTCC.API.Usuario.GetUsuarioLogin(usuario);
                        ProjetoTCC.Global.UsuarioGlobal = usuario;                        
                        MainPage = new NavigationPage(new Home(false));
                    }
                    catch (Exception e)
                    {
                        Util.Global.IUsuario.ExcluirUsuarioLocal();
                        await Global.MessageService.ShowMessageAsync($"Sessão expirada, por favor refaça o login!\n{e.Message}", "Login");
                        MainPage = new NavigationPage(new Login());
                    }
                }
                else
                {
                    MainPage = new NavigationPage(new Login());
                }
            }
            catch (Exception e)
            {
                await Global.MessageService.ShowMessageAsync(e.Message, "Erro");
            }
        }
    }
}
