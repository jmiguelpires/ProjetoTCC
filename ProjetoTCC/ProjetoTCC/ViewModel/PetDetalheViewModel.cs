using ProjetoTCC.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace ProjetoTCC.ViewModel
{
    public class PetDetalheViewModel 
    {

        private UsuarioPet _usuarioPet;
        public UsuarioPet UsuarioPet
        {
            get { return _usuarioPet; }
            set
            {
                _usuarioPet = value;
                Notify(nameof(UsuarioPet));
            }
        }

        private List<ImagensPet> listaImgPet;
        public List<ImagensPet> ListaImgPet
        {
            get
            {
                return listaImgPet;
            }
            set
            {
                listaImgPet = value;
                this.Notify(nameof(ListaImgPet));
            }
        }

        private List<string> listaImgPersonalidadePet;
        public List<string> ListaImgPersonalidadePet
        {
            get
            {
                return listaImgPersonalidadePet;
            }
            set
            {
                listaImgPersonalidadePet = value;
                this.Notify(nameof(ListaImgPersonalidadePet));
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

        public PetDetalheViewModel(UsuarioPet pet)
        {
            PreencheListaComImagensDosPets(pet);
            PreencheListaComImagensPersonalidade();
        }

        private void PreencheListaComImagensDosPets(UsuarioPet pet)
        {
            //Preenche a lista UsuarioPet com as informações de detalhe do Pet
            UsuarioPet = pet;

            List<ImagensPet> ListaImagens = new List<ImagensPet> { };

            if (!string.IsNullOrEmpty(UsuarioPet.ImgPet1))
            {
                ListaImagens.Add(new ImagensPet { ImgSourcePet = UsuarioPet.ImgSourcePet1 });
            }

            if (!string.IsNullOrEmpty(UsuarioPet.ImgPet2))
            {
                ListaImagens.Add(new ImagensPet { ImgSourcePet = UsuarioPet.ImgSourcePet2 });
            }

            if (!string.IsNullOrEmpty(UsuarioPet.ImgPet3))
            {
                ListaImagens.Add(new ImagensPet { ImgSourcePet = UsuarioPet.ImgSourcePet3 });
            }

            if (!string.IsNullOrEmpty(UsuarioPet.ImgPet4))
            {
                ListaImagens.Add(new ImagensPet { ImgSourcePet = UsuarioPet.ImgSourcePet4 });
            }

            ListaImgPet = ListaImagens;
        }

        private void PreencheListaComImagensPersonalidade()
        {
            List<string> ListaImgsPersonalidade = new List<string>();

            if (UsuarioPet.inPersonalidadeDocil)
            {
                ListaImgsPersonalidade.Add("personalidadeDocil.png");
            }
            if (UsuarioPet.inPersonalidadeTranquilo)
            {
                ListaImgsPersonalidade.Add("personalidadeTranquilo.png");
            }
            if (UsuarioPet.inPersonalidadeAlerta)
            {
                ListaImgsPersonalidade.Add("personalidadeAlerta.png");
            }
            if (UsuarioPet.inPersonalidadeAgressivo)
            {
                ListaImgsPersonalidade.Add("personalidadeAgressivo.png");
            }
            if (UsuarioPet.inPersonalidadeAtivoMinimo)
            {
                ListaImgsPersonalidade.Add("personalidadeAtivoPouco.png");
            }
            if (UsuarioPet.inPersonalidadeAtivoMedio)
            {
                ListaImgsPersonalidade.Add("personalidadeAtivoMedio.png");
            }
            if (UsuarioPet.inPersonalidadeAtivoMaximo)
            {
                ListaImgsPersonalidade.Add("personalidadeAtivoMuito.png");
            }
            if (UsuarioPet.inPersonalidadeCarinhoso)
            {
                ListaImgsPersonalidade.Add("personalidadeCarinhoso.png");
            }
            if (UsuarioPet.inPersonalidadeAssustado)
            {
                ListaImgsPersonalidade.Add("personalidadeAssustado.png");
            }
            if (UsuarioPet.inPersonalidadePreguicoso)
            {
                ListaImgsPersonalidade.Add("personalidadePreguicoso.png");
            }
            if (UsuarioPet.inPersonalidadeExplorador)
            {
                ListaImgsPersonalidade.Add("personalidadeExplorador.png");
            }
            if (UsuarioPet.inPersonalidadeCurioso)
            {
                ListaImgsPersonalidade.Add("personalidadeCurioso.png");
            }
            if (UsuarioPet.inPersonalidadeMedroso)
            {
                ListaImgsPersonalidade.Add("personalidadeMedroso.png");
            }

            ListaImgPersonalidadePet = ListaImgsPersonalidade;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class ImagensPet
        {
            public ImageSource ImgSourcePet { get; set; }
        }


    }
}
