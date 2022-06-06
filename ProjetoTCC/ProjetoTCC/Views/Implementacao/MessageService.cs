
using System.Threading.Tasks;

namespace ProjetoTCC.Views.Implementacao
{
    public class MessageService : ProjetoTCC.Interface.IMessageService
    {
        public async Task ShowMessageAsync(string message, string titulo)
        {
            if (titulo.ToUpper() == "ERRO")
            {
                titulo = "Ops!";
            }
            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(titulo, message, "OK");
        }

        public async Task<bool> ShowMessageYesNoAsync(string message, string titulo)
        {
            if (titulo.ToUpper() == "ERRO")
            {
                titulo = "Ops!";
            }
            bool result = await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(titulo, message, "Sim", "Não");
            return result;
        }
    }
}
