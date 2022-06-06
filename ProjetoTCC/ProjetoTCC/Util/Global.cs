using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoTCC.Model;

namespace Util
{
    public static class Global
    {
        public static IUsuarioLocal IUsuario { get; set; } 

        public static Usuario UsuarioGlobal { get; set; }
    }
}
