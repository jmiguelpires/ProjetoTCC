
using System.Threading.Tasks;

namespace ProjetoTCC.Interface
{
    public interface IMessageService
    {
        Task ShowMessageAsync(string message, string titulo);
        Task<bool> ShowMessageYesNoAsync(string message, string titulo);
    }

}
