using System.Collections.Generic;
using System.Threading.Tasks;
using MarqueesAssistant.API.Controllers;
using MarqueesAssistant.API.Dtos;
using MarqueesAssistant.API.Helpers;
using MarqueesAssistant.API.Models;

namespace MarqueesAssistant.API.Data
{
    public interface IWorkerRepo : IGenRepo
    {
        Task<IEnumerable<Worker>> GetWorkers();
        Task<PagedList<WorkerDisplayDto>> GetWorkers2(OwnerParameters ownerParameters);
        Task<Worker> GetWorker(int id);
      //  Task<PagedList<WorkerDisplayDto>> GetSearchedWorkers(OwnerParameters ownerParameters, string searchString);
        Task<PagedList<WorkerDisplayDto>> GetSearchedWorkersAndSorted(OwnerParameters ownerParameters, 
        string searchString, int sortBy);
        int GetMaxWorkerId();


    }
}