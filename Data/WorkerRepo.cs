using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarqueesAssistant.API.Controllers;
using Microsoft.EntityFrameworkCore;

namespace MarqueesAssistant.API.Data
{
    public class WorkerRepo : IWorkerRepo
    {
        private readonly DataContext _context;



        public WorkerRepo(DataContext context)
        {
            _context = context;
        }

        public int GetMaxWorkerId()
        {
            int id =  _context.Workers.Select(  u => u.Id).Max();
            return id;
        }

        public async Task<Worker> GetWorker(int id)
        {
            var worker = await _context.Workers.FirstOrDefaultAsync(x => x.Id == id);
            return worker;
        }

        public async  Task<IEnumerable<Worker>> GetWorkers()
        {
            var workers = await _context.Workers.ToListAsync();
            return workers;
        }

    }
}