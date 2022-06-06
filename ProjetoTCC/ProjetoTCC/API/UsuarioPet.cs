using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProjetoTCC.API
{
    public class UsuarioPet
    {
        //public static async Task<Model.Usuario> CadastrarPostAsync(Model.Usuario usuario)
        //{
        //    //string senhaOriginal = usuario.senha;
        //    try
        //    {
        //        //usuario.senha = senhaOriginal; //Util.Rotinas.SHA1(cliente.Senha); //encripta a senha para gravar no banco

        //        var json = JsonConvert.SerializeObject(usuario, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        //        var url = "CreateUsuario"; //"Usuario /CreateUsuario";

        //        var servico = new Servico();

        //        var retorno = await servico.PostAsync<Model.Usuario>(url, json);

        //        //retorno.senha = senhaOriginal; //volta a senha para poder fazer login
        //        return retorno;
        //    }
        //    catch (Exception e)
        //    {
        //        //usuario.senha = senhaOriginal; //volta a senha caso tenha dado erro
        //        throw e;
        //    }
        //}

        public static async Task<List<Model.UsuarioPet>> ListaPetsGetAsync()
        {
            try
            {
                var url = "GetPetUsuario";

                var servico = new Servico();
                var result = await servico.GetAsync<Model.UsuarioPet>(url);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task<Model.UsuarioPet> UsuarioPetGetAsync()
        {
            try
            {
                var url = "GetPetUsuario";

                var servico = new Servico();
                var result = await servico.GetItemAsync<Model.UsuarioPet>(url);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task<Model.UsuarioPet> CadastrarPostAsync(Model.UsuarioPet usuarioPet)
        {
            try
            {
                var json = JsonConvert.SerializeObject(usuarioPet, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                var url = "CreatePetUsuario"; 

                var servico = new Servico();

                var retorno = await servico.PostAsync<Model.UsuarioPet>(url, json);

                return retorno;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task ExcluirPetDeleteAsync(Model.UsuarioPet usuarioPet)
        {
            try
            {
                var url = $"DeletePetUsuario?idPet={usuarioPet.idPet}";
                var servico = new Servico();
                await servico.DeleteAsync(url);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task<Model.Usuario> AtualizarPetPutAsync(Model.UsuarioPet usuarioPet)
        {
            //string senhaOriginal = cliente.Senha;
            try
            {
                string cpfcnpj = usuarioPet.CPFCNPJ.ToString().Replace(".", "").Replace("-", "");
                long idpet = usuarioPet.idPet;
                var json = JsonConvert.SerializeObject(usuarioPet);
                var url = $"UpdatePetUsuario?cpfcnpj={cpfcnpj}&idPet={idpet}";

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
