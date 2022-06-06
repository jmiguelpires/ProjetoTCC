using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public interface IUsuarioLocal
    {
        public void SalvarUsuarioLocal(ProjetoTCC.Model.Usuario usuario);
        public ProjetoTCC.Model.Usuario BuscarUsuarioLocal();
        public void ExcluirUsuarioLocal(); 
    }
}
