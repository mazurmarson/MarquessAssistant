using System.Threading.Tasks;
using MarqueesAssistant.API.Controllers;

namespace MarqueesAssistant.API.Data
{
    public interface IAuthRepo
    {
         Task<Worker> Login(string login, string password);
         Task<Worker> Register(Worker worker, string password);
         
         Task<bool> WorkerIsExist(string login);
    }
}