using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProjetoTCC.ViewModel;
using ProjetoTCC.Model;

namespace ProjetoTCC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cadastro : ContentPage
    {
        private readonly Usuario _usuario;
        private readonly Municipio municipio;
        private readonly Estado estado;

        private readonly CadastroViewModel cadastroViewModel;

        public Cadastro(Usuario usuario)
        {
            var navigationPage = Application.Current.MainPage as NavigationPage;
            navigationPage.BarBackgroundColor = Color.FromHex("#00374f");
            InitializeComponent();

            _usuario = usuario;

            cadastroViewModel = new CadastroViewModel(usuario);
            BindingContext = cadastroViewModel;

            if (_usuario == null) //veio do Criar conta, logo, é para cadastrar
            {
                _usuario = new Usuario();

                btnCadastrar.Text = "Cadastrar";

                //Verifica se está em edição
                _usuario.EmEdicao = false;
            }
            else //veio do Meu Perfil, logo, é para editar o perfil
            {
                btnCadastrar.Text = "Atualizar";

                //altera o texto do cabeçalho
                lblCabecalho.Text = "Atualize aqui seus dados de cadastro";

                //Verifica se está em edição
                _usuario.EmEdicao = true;

                //trava controle de cpf uma vez que é chave primária e uma vez cadastrado nao pode ser alterado
                entryCPF.IsEnabled = false;

                //preenche data de nascimento com as informações do banco de dados                   
                entryDataDeNascimento.Text = _usuario.dtNascimentoEscrito;

                //carrega foto de perfil do usuário
                if (!string.IsNullOrEmpty(_usuario.imgFotoPerfil))
                {

                    string extensaoImagem = "";

                    if (_usuario.imgFotoPerfil.Contains("data:image/png;base64"))
                    {
                        extensaoImagem = "png";
                    }
                    else if (_usuario.imgFotoPerfil.Contains("data:image/jpeg;base64"))
                    {
                        extensaoImagem = "jpeg";
                    }
                    else
                    {
                        extensaoImagem = "jpg";
                    }

                    var img64 = _usuario.imgFotoPerfil.Replace($"data:image/{extensaoImagem};base64,", "") ?? "";
                    foto.HorizontalOptions = LayoutOptions.FillAndExpand;
                    foto.VerticalOptions = LayoutOptions.FillAndExpand;
                    foto.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(img64)));
                }

            }

            //cadastroViewModel = new CadastroViewModel(_usuario);
            //BindingContext = cadastroViewModel;
        }

        protected override async void OnAppearing()
        {
            if (_usuario.EmEdicao)
            {
                var estado = cadastroViewModel.ListaEstados.FirstOrDefault(e => e.CdEstado == _usuario.CdEstado);
                if (estado != null)
                {
                    pickerEstado.SelectedItem = estado;
                    await cadastroViewModel.FiltraListaMunicipios(estado);
                    var cidade = cadastroViewModel.ListaCidades.FirstOrDefault(e => e.Nome == _usuario.Cidade);
                    if (cidade != null)
                    {
                        pickerCidade.SelectedItem = cidade;
                    }
                    //var cidade = new Municipio
                    //{
                    //    NmMunicipio = _usuario.Cidade,
                    //    Nome = _usuario.Cidade
                    //};

                    //cadastroViewModel.ListaCidades.Add(new Municipio { NmMunicipio = _usuario.Cidade });
                    //var cidade = cadastroViewModel.ListaCidades.FirstOrDefault(e => e.Nome == _usuario.Cidade);

                    //pickerCidade.SelectedItem = cidade;

                }
            }
        }

        private async void Foto_Tapped(object sender, EventArgs e)
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
                            await TakePhotoAsync();
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
                                    await TakePhotoAsync();
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
                    await PicPhotoAsync();
                }
            }
            catch (Exception)
            {
                await Global.MessageService.ShowMessageAsync("Não foi possível alterar a foto!", "Erro");
            }
        }

        private async Task TakePhotoAsync()
        {
            var result = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions { Title = "Tire sua foto." });

            if (result != null)
            {
                Stream stream = await result.OpenReadAsync();
                ImageSourceToBase64(stream, true);
            }
        }

        private void ImageSourceToBase64(Stream stream, bool inCamera)
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


            _usuario.imgFotoPerfil = $"data:image/jpeg;base64,{foto64}";

            foto.HorizontalOptions = LayoutOptions.FillAndExpand;
            foto.VerticalOptions = LayoutOptions.FillAndExpand;

            foto.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(foto64)));

            frameFoto.CornerRadius = 75;
        }

        private async Task PicPhotoAsync()
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Selecione a foto." });

            if (result != null)
            {
                Stream stream = await result.OpenReadAsync();
                ImageSourceToBase64(stream, false);
            }
        }

        private async void PickerEstado_Unfocused(object sender, FocusEventArgs e)
        {
            if (pickerEstado.SelectedItem is Estado estado)
            {
                try
                {
                    await cadastroViewModel.FiltraListaMunicipios(estado);
                }
                catch (Exception ex)
                {
                    await Global.MessageService.ShowMessageAsync(ex.Message, "Erro");
                }
            }
        }
    }
}