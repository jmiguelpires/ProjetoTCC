using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProjetoTCC.API
{
    public class Usuario
    {
        public static async Task<Model.Usuario> CadastrarPostAsync(Model.Usuario usuario)
        {
            //string senhaOriginal = usuario.senha;
            try
            {
                //usuario.senha = senhaOriginal; //Util.Rotinas.SHA1(cliente.Senha); //encripta a senha para gravar no banco

                var json = JsonConvert.SerializeObject(usuario, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                var url = "CreateUsuario"; //"Usuario /CreateUsuario";

                var servico = new Servico();

                var retorno = await servico.PostAsync<Model.Usuario>(url, json);

                //retorno.senha = senhaOriginal; //volta a senha para poder fazer login
                return retorno;
            }
            catch (Exception e)
            {
                //usuario.senha = senhaOriginal; //volta a senha caso tenha dado erro
                throw e;
            }
        }

        public static async Task<Model.Usuario> UsuarioCPFGetAsync(Model.Usuario usuario)
        {
            try
            {
                string cpfcnpj = usuario.CPFCNPJ.ToString().Replace(".", "").Replace("-", "");
                var url = $"GetUsuarioPorCPF?cpfcnpj={cpfcnpj}";
                var servico = new Servico();

                var json = JsonConvert.SerializeObject(usuario, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

                var Usuario = await servico.GetUsuarioAsync<Model.Usuario>(url, json);                     

                return Usuario;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task<Model.Usuario> GetUsuarioLogin(Model.Usuario login)
        {
            try
            {
                var url = $"GetUsuarioLogin?email={login.email}&senha={login.senha}"; //"Usuario /CreateUsuario"; //"v2/crm/clientes/PostCliente/";
                var servico = new Servico();

                var json = JsonConvert.SerializeObject(login, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

                var Usuarioresult = await servico.GetUsuarioAsync<Model.Usuario>(url, json);

                Usuarioresult.DtUltimoLogin = DateTime.Today;

                Util.Global.IUsuario.SalvarUsuarioLocal(Usuarioresult);

                return Usuarioresult;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task<Model.Usuario> AtualizarPutAsync(Model.Usuario usuario)
        {
            //string senhaOriginal = cliente.Senha;
            try
            {
                string cpfcnpj = usuario.CPFCNPJ.ToString().Replace(".", "").Replace("-", "");
                var json = JsonConvert.SerializeObject(usuario);
                var url = $"UpdateUsuario?cpfcnpj={cpfcnpj}";

                var servico = new Servico();

                var retorno = await servico.PutAsync<Model.Usuario>(url, json);

                return retorno;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
