using System.Collections.Generic;
using MarqueesAssistant.API.Controllers;

namespace MarqueesAssistant.API.Data
{
    public interface IWorkersRepo
    {
        IEnumerable<Worker> GetWorkers();
        
    }
}