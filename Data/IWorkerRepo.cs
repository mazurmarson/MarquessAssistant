using System.Collections.Generic;
using System.Threading.Tasks;
using MarqueesAssistant.API.Controllers;

namespace MarqueesAssistant.API.Data
{
    public interface IWorkerRepo
    {
        Task<IEnumerable<Worker>> GetWorkers();
        Task<Worker> GetWorker(int id);

        

        int GetMaxWorkerId();

        
    }
}