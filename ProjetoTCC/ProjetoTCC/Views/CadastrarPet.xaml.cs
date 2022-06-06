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
    public partial class CadastrarPet : ContentPage

    {
        private readonly CadastrarPetViewModel cadastrarPetViewModel;

        private readonly UsuarioPet _usuarioPet;
        public CadastrarPet(UsuarioPet usuarioPet)
        {
            //Define a cor da Barra da página
            var navigationPage = Application.Current.MainPage as NavigationPage;
            navigationPage.BarBackgroundColor = Color.FromHex("#00374f");

            InitializeComponent();

            _usuarioPet = usuarioPet;


            if (_usuarioPet == null) //veio do Cadastrar Pet para adoção, logo, é para cadastrar
            {

                _usuarioPet = new UsuarioPet
                {

                };

                //Verifica se está em edição
                _usuarioPet.EmEdicao = false;

                btnCadastrar.Text = "Cadastrar";

                //esconde o controle com a data de adoção 
                frameDtAdocao.IsVisible = false;
                boxViewSeparador.IsVisible = false;
                GridAdocao.IsVisible = false;
            }
            else //veio da edição do Meus Pets
            {
                //Verifica se está em edição
                _usuarioPet.EmEdicao = true;

                //preenche data de nascimento com as informações do banco de dados                   
                entryDtNascimento.Text = _usuarioPet.dtNascimentoEscrito;

                //marca o tipo do pet (Cachorro ou Gato)
                if (_usuarioPet.tipoPet == "C")
                {
                    SeleconouTipoPetCachorro();
                }
                else if (_usuarioPet.tipoPet == "G")
                {
                    SeleconouTipoPetGato();
                }

                //marca sexo do pet (Macho ou Fêmea)
                if (_usuarioPet.sexo == "M")
                {
                    SelecionouSexoMacho();
                }
                else if (_usuarioPet.sexo == "F")
                {
                    SelecionouSexoFemea();
                }

                //marca porte do pet (Pequeno, Médio ou Grande)
                if (_usuarioPet.porte == "P")
                {
                    SelecionouPorteP();
                }
                else if (_usuarioPet.porte == "M")
                {
                    SelecionouPorteM();
                }
                else if (_usuarioPet.porte == "G")
                {
                    SelecionouPorteG();
                }

                //marcações de personalidade
                SelecionouPersonalidadeDocil();
                SelecionouPersonalidadeTranquilo();
                SelecionouPersonalidadeAlerta();
                SelecionouPersonalidadeAgressivo();
                SelecionouPersonalidadeAtivoPouco();
                SelecionouPersonalidadeAtivoMedio();
                SelecionouPersonalidadeAtivoMuito();
                SelecionouPersonalidadeCarinhoso();
                SelecionouPersonalidadeAssustado();
                SelecionouPersonalidadePreguicoso();
                SelecionouPersonalidadeExplorador();
                SelecionouPersonalidadeCurioso();
                SelecionouPersonalidadeMedroso();

                //mostra o controle com a data de adoção 
                frameDtAdocao.IsVisible = true;
                boxViewSeparador.IsVisible = true;
                GridAdocao.IsVisible = true;

                entryDtAdotado.Text = _usuarioPet.dtAdotadoEscrito;

                //carrega imgp do pet
                CarregaImgPets();

                //troca nome do botão 
                btnCadastrar.Text = "Atualizar";

            }
            cadastrarPetViewModel = new CadastrarPetViewModel(_usuarioPet);
            BindingContext = cadastrarPetViewModel;
        }

        protected override async void OnAppearing()
        {
            if (_usuarioPet.EmEdicao)
            {
                var raca = cadastrarPetViewModel.ListaRacas.FirstOrDefault(r => r.raca == _usuarioPet.raca);
                if (raca != null)
                {

                    pickerRaca.SelectedItem = raca;
                }
            }
        }
        private async void SelecionouSexoMachoPet_Clicked(object sender, System.EventArgs e)
        {
            var CorPrincipal = Color.FromHex("#e3f5ff");

            //troca de cor do botão Macho
            if (btnSexo_Macho.BackgroundColor == CorPrincipal)
            {
                SelecionouSexoMacho();

                await cadastrarPetViewModel.SexoPet("M");
            }

        }

        private async void SelecionouSexoFemeaPet_Clicked(object sender, EventArgs e)
        {
            var CorPrincipal = Color.FromHex("#e3f5ff");

            //troca de cor do botão fêmea
            if (btnSexo_Femea.BackgroundColor == CorPrincipal)
            {
                SelecionouSexoFemea();

                await cadastrarPetViewModel.SexoPet("F");
            }

        }

        private async void SelecionouPorteP_Clicked(object sender, EventArgs e)
        {
            var CorPrincipal = Color.FromHex("#e3f5ff");

            if (btnPorteP.BackgroundColor == CorPrincipal)
            {
                SelecionouPorteP();

                await cadastrarPetViewModel.PortePet("P");
            }
        }

        private async void SelecionouPorteM_Clicked(object sender, EventArgs e)
        {
            var CorPrincipal = Color.FromHex("#e3f5ff");

            if (btnPorteM.BackgroundColor == CorPrincipal)
            {
                SelecionouPorteM();

                await cadastrarPetViewModel.PortePet("M");
            }
        }

        private async void SelecionouPorteG_Clicked(object sender, EventArgs e)
        {
            var CorPrincipal = Color.FromHex("#e3f5ff");

            if (btnPorteG.BackgroundColor == CorPrincipal)
            {
                SelecionouPorteG();

                await cadastrarPetViewModel.PortePet("G");
            }
        }

        private async void SelecionouTipoPetCachorro_Clicked(object sender, EventArgs e)
        {
            var CorPrincipal = Color.FromHex("#e3f5ff");

            if (btnCachorro.BackgroundColor == CorPrincipal)
            {
                SeleconouTipoPetCachorro();

                await cadastrarPetViewModel.TipoPet("C");
            }
        }

        private async void SelecionouTipoPetGato_Clicked(object sender, EventArgs e)
        {
            var CorPrincipal = Color.FromHex("#e3f5ff");

            if (btnGato.BackgroundColor == CorPrincipal)
            {
                SeleconouTipoPetGato();

                await cadastrarPetViewModel.TipoPet("G");
            }
        }

        private async void PickerRaca_Unfocused(object sender, FocusEventArgs e)
        {
            if (pickerRaca.SelectedItem is Racas raca)
            {
                try
                {
                    await cadastrarPetViewModel.RacaPet(raca);
                }
                catch (Exception ex)
                {
                    await Global.MessageService.ShowMessageAsync(ex.Message, "Erro");
                }
            }
        }

        private async void ImgPet1_Tapped(object sender, EventArgs e)
        {
            PhotoClickedAsync("ImgPet1");
        }

        private void ImgPet2_Tapped(object sender, EventArgs e)
        {
            PhotoClickedAsync("ImgPet2");
        }

        private void ImgPet3_Tapped(object sender, EventArgs e)
        {
            PhotoClickedAsync("ImgPet3");
        }

        private void ImgPet4_Tapped(object sender, EventArgs e)
        {
            PhotoClickedAsync("ImgPet4");
        }

        #region Tratamento das fotos dos Pets
        private async Task PhotoClickedAsync(string ImgPet)
        {
            try
            {
                var resposta = await Global.MessageService.ShowMessageYesNoAsync("Deseja utilizar a câmera?", "Foto");

                if (resposta)
                {
                    if (MediaPicker.IsCaptureSupported)
                    {
                        var permissao = await Permissions.CheckStatusAsync<Permissions.Camera>();

                        if (permissao == PermissionStatus.Granted)
                        {
                            await TakePhotoAsync(ImgPet);
                        }
                        else
                        {
                            if ((permissao == PermissionStatus.Denied) && (Device.RuntimePlatform == Device.iOS) || permissao == PermissionStatus.Disabled)
                            {
                                await Global.MessageService.ShowMessageAsync("Permita o acesso a câmera nas configurações do dispositivo.", "Erro");
                            }
                            else
                            {
                                permissao = await Permissions.RequestAsync<Permissions.Camera>();

                                if (permissao == PermissionStatus.Granted)
                                {
                                    await TakePhotoAsync(ImgPet);
                                }
                                else
                                {
                                    await Global.MessageService.ShowMessageAsync("Acesso a câmera negado pelo usuário.", "Erro");
                                }
                            }
                        }
                    }
                    else
                    {
                        await Global.MessageService.ShowMessageAsync("Dispositivo não possui suporte a câmera.", "Erro");
                    }
                }
                else
                {
                    await PicPhotoAsync(ImgPet);
                }
            }
            catch (Exception)
            {
                await Global.MessageService.ShowMessageAsync("Não foi possível alterar a foto!", "Erro");
            }
        }

        private async Task TakePhotoAsync(string ImgPet)
        {
            var result = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions { Title = "Tire sua foto." });

            if (result != null)
            {
                Stream stream = await result.OpenReadAsync();
                ImageSourceToBase64(stream, true, ImgPet);
            }
        }

        private async Task PicPhotoAsync(string ImgPet)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Selecione a foto." });

            if (result != null)
            {
                Stream stream = await result.OpenReadAsync();
                ImageSourceToBase64(stream, false, ImgPet);
            }
        }

        private void ImageSourceToBase64(Stream stream, bool inCamera, string ImgPet)
        {
            byte[] b;
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                b = ms.ToArray();
            }

            var ResizeImage = DependencyService.Get<ProjetoTCC.Interface.IResizeImage>();

            var resized = ResizeImage.Resize(b, 200, 200, inCamera);
            var foto64 = Convert.ToBase64String(resized);

            if (ImgPet == "ImgPet1")
            {
                _usuarioPet.ImgPet1 = $"data:image/jpeg;base64,{foto64}";
                ImgPet1.HorizontalOptions = LayoutOptions.FillAndExpand;
                ImgPet1.VerticalOptions = LayoutOptions.FillAndExpand;
                ImgPet1.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(foto64)));
            }
            else if (ImgPet == "ImgPet2")
            {
                _usuarioPet.ImgPet2 = $"data:image/jpeg;base64,{foto64}";
                ImgPet2.HorizontalOptions = LayoutOptions.FillAndExpand;
                ImgPet2.VerticalOptions = LayoutOptions.FillAndExpand;
                ImgPet2.HeightRequest = 50;
                ImgPet2.WidthRequest = 50;
                ImgPet2.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(foto64)));
            }
            else if (ImgPet == "ImgPet3")
            {
                _usuarioPet.ImgPet3 = $"data:image/jpeg;base64,{foto64}";
                ImgPet3.HorizontalOptions = LayoutOptions.FillAndExpand;
                ImgPet3.VerticalOptions = LayoutOptions.FillAndExpand;
                ImgPet3.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(foto64)));
            }
            else if (ImgPet == "ImgPet4")
            {
                _usuarioPet.ImgPet4 = $"data:image/jpeg;base64,{foto64}";
                ImgPet4.HorizontalOptions = LayoutOptions.FillAndExpand;
                ImgPet4.VerticalOptions = LayoutOptions.FillAndExpand;
                ImgPet4.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(foto64)));
            }


        }

        #endregion

        #region Imagens Personlidade (troca imagens da personalidade ao serem clicadas)
        private async void personalidadeDocil_Tapped(object sender, EventArgs e)
        {
            var ImgPersonalidadeDocil = personalidadeDocil.Source as FileImageSource;

            if (ImgPersonalidadeDocil.File == "personalidadeDocil.png")
            {
                personalidadeDocil.Source = "personalidadeDocil_checked.png";
                await cadastrarPetViewModel.Personalidade_Docil(true);
            }
            else if (ImgPersonalidadeDocil.File == "personalidadeDocil_checked.png")
            {
                personalidadeDocil.Source = "personalidadeDocil.png";
                await cadastrarPetViewModel.Personalidade_Docil(false);
            }
        }

        private async void personalidadeTranquilo_Tapped(object sender, EventArgs e)
        {
            var ImgPersonalidadeTranquilo = personalidadeTranquilo.Source as FileImageSource;

            if (ImgPersonalidadeTranquilo.File == "personalidadeTranquilo.png")
            {
                personalidadeTranquilo.Source = "personalidadeTranquilo_checked.png";
                await cadastrarPetViewModel.Personalidade_Tranquilo(true);
            }
            else if (ImgPersonalidadeTranquilo.File == "personalidadeTranquilo_checked.png")
            {
                personalidadeTranquilo.Source = "personalidadeTranquilo.png";
                await cadastrarPetViewModel.Personalidade_Tranquilo(false);
            }
        }

        private async void personalidadeAlerta_Tapped(object sender, EventArgs e)
        {
            var ImgPersonalidadeAlerta = personalidadeAlerta.Source as FileImageSource;

            if (ImgPersonalidadeAlerta.File == "personalidadeAlerta.png")
            {
                personalidadeAlerta.Source = "personalidadeAlerta_checked.png";
                await cadastrarPetViewModel.Personalidade_Alerta(true);
            }
            else if (ImgPersonalidadeAlerta.File == "personalidadeAlerta_checked.png")
            {
                personalidadeAlerta.Source = "personalidadeAlerta.png";
                await cadastrarPetViewModel.Personalidade_Alerta(false);
            }
        }

        private async void personalidadeAgressivo_Tapped(object sender, EventArgs e)
        {
            var ImgPersonalidadeAgressivo = personalidadeAgressivo.Source as FileImageSource;

            if (ImgPersonalidadeAgressivo.File == "personalidadeAgressivo.png")
            {
                personalidadeAgressivo.Source = "personalidadeAgressivo_checked.png";
                await cadastrarPetViewModel.Personalidade_Agressivo(true);
            }
            else if (ImgPersonalidadeAgressivo.File == "personalidadeAgressivo_checked.png")
            {
                personalidadeAgressivo.Source = "personalidadeAgressivo.png";
                await cadastrarPetViewModel.Personalidade_Agressivo(false);
            }
        }

        private async void personalidadeAtivoPouco_Tapped(object sender, EventArgs e)
        {
            var ImgPersonalidadeAtivoPouco = personalidadeAtivoPouco.Source as FileImageSource;

            if (ImgPersonalidadeAtivoPouco.File == "personalidadeAtivoPouco.png")
            {
                personalidadeAtivoPouco.Source = "personalidadeAtivoPouco_checked.png";

                personalidadeAtivoMedio.Source = "personalidadeAtivoMedio.png";
                personalidadeAtivoMuito.Source = "personalidadeAtivoMuito.png";

                await cadastrarPetViewModel.Personalidade_Ativos(true, false, false);
            }
        }

        private async void personalidadeAtivoMedio_Tapped(object sender, EventArgs e)
        {
            var ImgPersonalidadeAtivoMedio = personalidadeAtivoMedio.Source as FileImageSource;

            if (ImgPersonalidadeAtivoMedio.File == "personalidadeAtivoMedio.png")
            {
                personalidadeAtivoMedio.Source = "personalidadeAtivoMedio_checked.png";

                personalidadeAtivoPouco.Source = "personalidadeAtivoPouco.png";
                personalidadeAtivoMuito.Source = "personalidadeAtivoMuito.png";

                await cadastrarPetViewModel.Personalidade_Ativos(false, true, false);
            }

        }

        private async void personalidadeAtivoMuito_Tapped(object sender, EventArgs e)
        {
            var ImgPersonalidadeAtivoMuito = personalidadeAtivoMuito.Source as FileImageSource;

            if (ImgPersonalidadeAtivoMuito.File == "personalidadeAtivoMuito.png")
            {
                personalidadeAtivoMuito.Source = "personalidadeAtivoMuito_checked.png";

                personalidadeAtivoPouco.Source = "personalidadeAtivoPouco.png";
                personalidadeAtivoMedio.Source = "personalidadeAtivoMedio.png";

                await cadastrarPetViewModel.Personalidade_Ativos(false, false, true);
            }
        }

        private async void personalidadeCarinhoso_Tapped(object sender, EventArgs e)
        {
            var ImgPersonalidadeCarinhoso = personalidadeCarinhoso.Source as FileImageSource;

            if (ImgPersonalidadeCarinhoso.File == "personalidadeCarinhoso.png")
            {
                personalidadeCarinhoso.Source = "personalidadeCarinhoso_checked.png";
                await cadastrarPetViewModel.Personalidade_Carinhoso(true);
            }
            else if (ImgPersonalidadeCarinhoso.File == "personalidadeCarinhoso_checked.png")
            {
                personalidadeCarinhoso.Source = "personalidadeCarinhoso.png";
                await cadastrarPetViewModel.Personalidade_Carinhoso(false);
            }
        }

        private async void personalidadeAssustado_Tapped(object sender, EventArgs e)
        {
            var ImgPersonalidadeAssustado = personalidadeAssustado.Source as FileImageSource;

            if (ImgPersonalidadeAssustado.File == "personalidadeAssustado.png")
            {
                personalidadeAssustado.Source = "personalidadeAssustado_checked.png";
                await cadastrarPetViewModel.Personalidade_Assustado(true);
            }
            else if (ImgPersonalidadeAssustado.File == "personalidadeAssustado_checked.png")
            {
                personalidadeAssustado.Source = "personalidadeAssustado.png";
                await cadastrarPetViewModel.Personalidade_Assustado(false);
            }
        }

        private async void personalidadePreguicoso_Tapped(object sender, EventArgs e)
        {
            var ImgPersonalidadePreguicoso = personalidadePreguicoso.Source as FileImageSource;

            if (ImgPersonalidadePreguicoso.File == "personalidadePreguicoso.png")
            {
                personalidadePreguicoso.Source = "personalidadePreguicoso_checked.png";
                await cadastrarPetViewModel.Personalidade_Preguicoso(true);
            }
            else if (ImgPersonalidadePreguicoso.File == "personalidadePreguicoso_checked.png")
            {
                personalidadePreguicoso.Source = "personalidadePreguicoso.png";
                await cadastrarPetViewModel.Personalidade_Preguicoso(false);
            }
        }

        private async void personalidadeExplorador_Tapped(object sender, EventArgs e)
        {
            var ImgPersonalidadeExplorador = personalidadeExplorador.Source as FileImageSource;

            if (ImgPersonalidadeExplorador.File == "personalidadeExplorador.png")
            {
                personalidadeExplorador.Source = "personalidadeExplorador_checked.png";
                await cadastrarPetViewModel.Personalidade_Explorador(true);
            }
            else if (ImgPersonalidadeExplorador.File == "personalidadeExplorador_checked.png")
            {
                personalidadeExplorador.Source = "personalidadeExplorador.png";
                await cadastrarPetViewModel.Personalidade_Explorador(false);
            }
        }

        private async void personalidadeCurioso_Tapped(object sender, EventArgs e)
        {
            var ImgPersonalidadeCurioso = personalidadeCurioso.Source as FileImageSource;

            if (ImgPersonalidadeCurioso.File == "personalidadeCurioso.png")
            {
                personalidadeCurioso.Source = "personalidadeCurioso_checked.png";
                await cadastrarPetViewModel.Personalidade_Curioso(true);
            }
            else if (ImgPersonalidadeCurioso.File == "personalidadeCurioso_checked.png")
            {
                personalidadeCurioso.Source = "personalidadeCurioso.png";
                await cadastrarPetViewModel.Personalidade_Curioso(false);
            }
        }

        private async void personalidadeMedroso_Tapped(object sender, EventArgs e)
        {
            var ImgPersonalidadeMedroso = personalidadeMedroso.Source as FileImageSource;

            if (ImgPersonalidadeMedroso.File == "personalidadeMedroso.png")
            {
                personalidadeMedroso.Source = "personalidadeMedroso_checked.png";
                await cadastrarPetViewModel.Personalidade_Medroso(true);
            }
            else if (ImgPersonalidadeMedroso.File == "personalidadeMedroso_checked.png")
            {
                personalidadeMedroso.Source = "personalidadeMedroso.png";
                await cadastrarPetViewModel.Personalidade_Medroso(false);
            }
        }
        #endregion

        #region Selecionou tipo de Pet
        private void SeleconouTipoPetCachorro()
        {
            var CorPrincipal = Color.FromHex("#e3f5ff");
            var CorSecundaria = Color.FromHex("#00374f");

            btnCachorro.BackgroundColor = CorSecundaria;
            btnCachorro.Source = "imageButtonCachorro_Claro.png";

            btnGato.BackgroundColor = CorPrincipal;
            btnGato.Source = "imageButtonGato_Escuro.png";

            //if (_usuarioPet.EmEdicao)
            //{
            //cadastrarPetViewModel.ListaRacas = new ObservableCollection<Racas>(Util.Rotinas.ListaRacasCachorro());
            //}
        }

        private void SeleconouTipoPetGato()
        {
            var CorPrincipal = Color.FromHex("#e3f5ff");
            var CorSecundaria = Color.FromHex("#00374f");

            btnGato.BackgroundColor = CorSecundaria;
            btnGato.Source = "imageButtonGato_Claro.png";

            btnCachorro.BackgroundColor = CorPrincipal;
            btnCachorro.Source = "imageButtonCachorro_Escuro.png";

            //if (_usuarioPet.EmEdicao)
            //{
            //    cadastrarPetViewModel.ListaRacas = new ObservableCollection<Racas>(Util.Rotinas.ListaRacasGato());
            //}
        }
        #endregion

        #region Selecionou sexo do Pet
        private void SelecionouSexoFemea()
        {
            var CorPrincipal = Color.FromHex("#e3f5ff");
            var CorSecundaria = Color.FromHex("#00374f");

            btnSexo_Femea.BackgroundColor = CorSecundaria;
            btnSexo_Femea.TextColor = CorPrincipal;

            btnSexo_Macho.BackgroundColor = CorPrincipal;
            btnSexo_Macho.TextColor = CorSecundaria;
        }

        private void SelecionouSexoMacho()
        {
            var CorPrincipal = Color.FromHex("#e3f5ff");
            var CorSecundaria = Color.FromHex("#00374f");

            btnSexo_Macho.BackgroundColor = CorSecundaria;
            btnSexo_Macho.TextColor = CorPrincipal;

            btnSexo_Femea.BackgroundColor = CorPrincipal;
            btnSexo_Femea.TextColor = CorSecundaria;
        }
        #endregion

        #region Selecionou porte do Pet

        private void SelecionouPorteP()
        {
            var CorPrincipal = Color.FromHex("#e3f5ff");
            var CorSecundaria = Color.FromHex("#00374f");

            btnPorteP.BackgroundColor = CorSecundaria;
            btnPorteP.TextColor = CorPrincipal;

            btnPorteM.BackgroundColor = CorPrincipal;
            btnPorteM.TextColor = CorSecundaria;
            btnPorteG.BackgroundColor = CorPrincipal;
            btnPorteG.TextColor = CorSecundaria;
        }

        private void SelecionouPorteM()
        {
            var CorPrincipal = Color.FromHex("#e3f5ff");
            var CorSecundaria = Color.FromHex("#00374f");

            btnPorteM.BackgroundColor = CorSecundaria;
            btnPorteM.TextColor = CorPrincipal;

            btnPorteP.BackgroundColor = CorPrincipal;
            btnPorteP.TextColor = CorSecundaria;
            btnPorteG.BackgroundColor = CorPrincipal;
            btnPorteG.TextColor = CorSecundaria;
        }

        private void SelecionouPorteG()
        {
            var CorPrincipal = Color.FromHex("#e3f5ff");
            var CorSecundaria = Color.FromHex("#00374f");

            btnPorteG.BackgroundColor = CorSecundaria;
            btnPorteG.TextColor = CorPrincipal;

            btnPorteM.BackgroundColor = CorPrincipal;
            btnPorteM.TextColor = CorSecundaria;
            btnPorteP.BackgroundColor = CorPrincipal;
            btnPorteP.TextColor = CorSecundaria;

        }
        #endregion

        #region Selecionou personalidades do Pet
        private async void SelecionouPersonalidadeDocil()
        {
            var ImgPersonalidadeDocil = personalidadeDocil.Source as FileImageSource;

            if (_usuarioPet.inPersonalidadeDocil)
            {
                personalidadeDocil.Source = "personalidadeDocil_checked.png";
            }
            else
            {
                personalidadeDocil.Source = "personalidadeDocil.png";
            }
        }

        private async void SelecionouPersonalidadeTranquilo()
        {
            var ImgPersonalidadeTranquilo = personalidadeTranquilo.Source as FileImageSource;

            if (_usuarioPet.inPersonalidadeTranquilo)
            {
                personalidadeTranquilo.Source = "personalidadeTranquilo_checked.png";
            }
            else
            {
                personalidadeTranquilo.Source = "personalidadeTranquilo.png";
            }
        }

        private async void SelecionouPersonalidadeAlerta()
        {
            var ImgPersonalidadeAlerta = personalidadeAlerta.Source as FileImageSource;

            if (_usuarioPet.inPersonalidadeAlerta)
            {
                personalidadeAlerta.Source = "personalidadeAlerta_checked.png";
            }
            else
            {
                personalidadeAlerta.Source = "personalidadeAlerta.png";
            }
        }

        private async void SelecionouPersonalidadeAgressivo()
        {
            var ImgPersonalidadeAgressivo = personalidadeAgressivo.Source as FileImageSource;

            if (_usuarioPet.inPersonalidadeAgressivo)
            {
                personalidadeAgressivo.Source = "personalidadeAgressivo_checked.png";
            }
            else
            {
                personalidadeAgressivo.Source = "personalidadeAgressivo.png";
            }
        }

        private async void SelecionouPersonalidadeAtivoPouco()
        {
            var ImgPersonalidadeAtivoPouco = personalidadeAtivoPouco.Source as FileImageSource;

            if (_usuarioPet.inPersonalidadeAtivoMinimo)
            {
                personalidadeAtivoPouco.Source = "personalidadeAtivoPouco_checked.png";
            }
            else
            {
                personalidadeAtivoPouco.Source = "personalidadeAtivoPouco.png";
            }
        }

        private async void SelecionouPersonalidadeAtivoMedio()
        {
            var ImgPersonalidadeAtivoMedio = personalidadeAtivoMedio.Source as FileImageSource;

            if (_usuarioPet.inPersonalidadeAtivoMedio)
            {
                personalidadeAtivoMedio.Source = "personalidadeAtivoMedio_checked.png";
            }
            else
            {
                personalidadeAtivoMedio.Source = "personalidadeAtivoMedio.png";
            }
        }

        private async void SelecionouPersonalidadeAtivoMuito()
        {
            var ImgPersonalidadeAtivoMuito = personalidadeAtivoMuito.Source as FileImageSource;

            if (_usuarioPet.inPersonalidadeAtivoMaximo)
            {
                personalidadeAtivoMuito.Source = "personalidadeAtivoMuito_checked.png";
            }
            else
            {
                personalidadeAtivoMuito.Source = "personalidadeAtivoMuito.png";
            }
        }

        private async void SelecionouPersonalidadeCarinhoso()
        {
            var ImgPersonalidadeCarinhoso = personalidadeCarinhoso.Source as FileImageSource;

            if (_usuarioPet.inPersonalidadeCarinhoso)
            {
                personalidadeCarinhoso.Source = "personalidadeCarinhoso_checked.png";
            }
            else
            {
                personalidadeCarinhoso.Source = "personalidadeCarinhoso.png";
            }
        }

        private async void SelecionouPersonalidadeAssustado()
        {
            var ImgPersonalidadeAssustado = personalidadeAssustado.Source as FileImageSource;

            if (_usuarioPet.inPersonalidadeAssustado)
            {
                personalidadeAssustado.Source = "personalidadeAssustado_checked.png";
            }
            else
            {
                personalidadeAssustado.Source = "personalidadeAssustado.png";
            }
        }

        private async void SelecionouPersonalidadePreguicoso()
        {
            var ImgPersonalidadePreguicoso = personalidadePreguicoso.Source as FileImageSource;

            if (_usuarioPet.inPersonalidadePreguicoso)
            {
                personalidadePreguicoso.Source = "personalidadePreguicoso_checked.png";
            }
            else
            {
                personalidadePreguicoso.Source = "personalidadePreguicoso.png";
            }
        }

        private async void SelecionouPersonalidadeExplorador()
        {
            var ImgPersonalidadeExplorador = personalidadeExplorador.Source as FileImageSource;

            if (_usuarioPet.inPersonalidadeExplorador)
            {
                personalidadeExplorador.Source = "personalidadeExplorador_checked.png";
            }
            else
            {
                personalidadeExplorador.Source = "personalidadeExplorador.png";
            }
        }

        private async void SelecionouPersonalidadeCurioso()
        {
            var ImgPersonalidadeCurioso = personalidadeCurioso.Source as FileImageSource;

            if (_usuarioPet.inPersonalidadeCurioso)
            {
                personalidadeCurioso.Source = "personalidadeCurioso_checked.png";
            }
            else
            {
                personalidadeCurioso.Source = "personalidadeCurioso.png";
            }
        }

        private async void SelecionouPersonalidadeMedroso()
        {
            var ImgPersonalidadeMedroso = personalidadeMedroso.Source as FileImageSource;

            if (_usuarioPet.inPersonalidadeMedroso)
            {
                personalidadeMedroso.Source = "personalidadeMedroso_checked.png";
            }
            else
            {
                personalidadeMedroso.Source = "personalidadeMedroso.png";
            }
        }
        #endregion

        #region Carrega ImgPets 1, 2, 3 e 4
        private void CarregaImgPets()
        {
            //ImgPet1
            if (!string.IsNullOrEmpty(_usuarioPet.ImgPet1))
            {

                string extensaoImagem = "";

                if (_usuarioPet.ImgPet1.Contains("data:image/png;base64"))
                {
                    extensaoImagem = "png";
                }
                else if (_usuarioPet.ImgPet1.Contains("data:image/jpeg;base64"))
                {
                    extensaoImagem = "jpeg";
                }
                else
                {
                    extensaoImagem = "jpg";
                }

                var img64 = _usuarioPet.ImgPet1.Replace($"data:image/{extensaoImagem};base64,", "") ?? "";
                ImgPet1.HorizontalOptions = LayoutOptions.FillAndExpand;
                ImgPet1.VerticalOptions = LayoutOptions.FillAndExpand;
                ImgPet1.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(img64)));
            }

            //ImgPet2
            if (!string.IsNullOrEmpty(_usuarioPet.ImgPet2))
            {

                string extensaoImagem = "";

                if (_usuarioPet.ImgPet2.Contains("data:image/png;base64"))
                {
                    extensaoImagem = "png";
                }
                else if (_usuarioPet.ImgPet2.Contains("data:image/jpeg;base64"))
                {
                    extensaoImagem = "jpeg";
                }
                else
                {
                    extensaoImagem = "jpg";
                }

                var img64 = _usuarioPet.ImgPet2.Replace($"data:image/{extensaoImagem};base64,", "") ?? "";
                ImgPet2.HorizontalOptions = LayoutOptions.FillAndExpand;
                ImgPet2.VerticalOptions = LayoutOptions.FillAndExpand;
                ImgPet2.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(img64)));
            }

            //ImgPet3
            if (!string.IsNullOrEmpty(_usuarioPet.ImgPet3))
            {

                string extensaoImagem = "";

                if (_usuarioPet.ImgPet3.Contains("data:image/png;base64"))
                {
                    extensaoImagem = "png";
                }
                else if (_usuarioPet.ImgPet3.Contains("data:image/jpeg;base64"))
                {
                    extensaoImagem = "jpeg";
                }
                else
                {
                    extensaoImagem = "jpg";
                }

                var img64 = _usuarioPet.ImgPet3.Replace($"data:image/{extensaoImagem};base64,", "") ?? "";
                ImgPet3.HorizontalOptions = LayoutOptions.FillAndExpand;
                ImgPet3.VerticalOptions = LayoutOptions.FillAndExpand;
                ImgPet3.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(img64)));
            }

            //ImgPet4
            if (!string.IsNullOrEmpty(_usuarioPet.ImgPet4))
            {

                string extensaoImagem = "";

                if (_usuarioPet.ImgPet4.Contains("data:image/png;base64"))
                {
                    extensaoImagem = "png";
                }
                else if (_usuarioPet.ImgPet4.Contains("data:image/jpeg;base64"))
                {
                    extensaoImagem = "jpeg";
                }
                else
                {
                    extensaoImagem = "jpg";
                }

                var img64 = _usuarioPet.ImgPet4.Replace($"data:image/{extensaoImagem};base64,", "") ?? "";
                ImgPet4.HorizontalOptions = LayoutOptions.FillAndExpand;
                ImgPet4.VerticalOptions = LayoutOptions.FillAndExpand;
                ImgPet4.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(img64)));
            }

        }
        #endregion

        private void entryDtAdotado_Unfocused(object sender, FocusEventArgs e)
        {

            var isValido = DateTime.TryParseExact(entryDtAdotado.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime data);
            if (isValido)
            {
                _usuarioPet.dtAdotado = data;
            }
            else if (string.IsNullOrEmpty(entryDtAdotado.Text))
            {
                _usuarioPet.dtAdotado = null;
            }
            else

            {
                throw new Exception("Informe uma data de nascimento válida!");
            }
            //}
        }
    }
}