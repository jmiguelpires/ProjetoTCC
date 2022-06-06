using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using ProjetoTCC.Model;
using Util;

namespace ProjetoTCC
{
    public static class Global
    {
        public static Interface.IMessageService MessageService { get; set; }
        public static Interface.INavigationService NavigationService { get; set; }

        //private static Model.Usuario _usuario;
        //public static Model.Usuario Usuario
        //{
        //    get { return _usuario; }

        //    set
        //    {
        //        _usuario = value;
        //        Global.Usuario = _usuario;
        //    }
        //}

        private static Model.Usuario _usuarioGlobal;
        public static Model.Usuario UsuarioGlobal
        {
            get { return _usuarioGlobal; }

            set
            {
                _usuarioGlobal = value;
                Util.Global.UsuarioGlobal = _usuarioGlobal;
            }
        }
    }
}
