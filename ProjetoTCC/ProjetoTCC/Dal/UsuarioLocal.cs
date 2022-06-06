using Dal;
using ProjetoTCC.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTCC.Dal
{
    public class UsuarioLocal : Util.IUsuarioLocal
    {
        public Usuario BuscarUsuarioLocal()
        {
            var dal = new AcessoDados<Usuario>();
            return dal.GetItemAsync().Result;
        }

        public void ExcluirUsuarioLocal()
        {
            var dal = new AcessoDados<Usuario>();
            dal.DeleteAllAsync();
        }

        public void SalvarUsuarioLocal(Usuario usuario)
        {
            Util.Global.UsuarioGlobal = usuario;

            var dal = new AcessoDados<Usuario>();
            var u = dal.GetItemAsync().Result;
            if (u == null)
            {
                dal.SaveItemAsync(usuario).Wait();
            }
            else
            {
                dal.UpdateItemAsync(usuario).Wait();
            }
        }
    }
}
