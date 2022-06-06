using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoTCC.Interface
{
    public interface INavigationService
    {
        Task NavigateToCadastro(Model.Usuario usuarioCarregado = null);

        Task NavigateToLogin();

        Task NavigateToHome(bool convidado);
        
        Task NavigateToAdotarPet(bool convidado);

        Task NavigateToPetDetalhe(Model.UsuarioPet pet);

        Task NavigateToCadastrarPet(Model.UsuarioPet usuarioPet = null);

        Task NavigateToMeusPets(Model.Usuario usuario);
    }
}
