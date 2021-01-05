using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MarqueesAssistant.API.Controllers;
using MarqueesAssistant.API.Dtos;
using MarqueesAssistant.API.Helpers;
using MarqueesAssistant.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MarqueesAssistant.API.Data
{
    public class WorkerRepo : GenRepo, IWorkerRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public WorkerRepo(DataContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public int GetMaxWorkerId()
        {
            int id = _context.Workers.Select(u => u.Id).Max();
            return id;
        }

        public async Task<Worker> GetWorker(int id)
        {
            var worker = await _context.Workers.FirstOrDefaultAsync(x => x.Id == id);
            return worker;
        }

        public async Task<IEnumerable<Worker>> GetWorkers()
        {
            var workers = await _context.Workers.ToListAsync();
            return workers;
        }

        public async Task<PagedList<WorkerDisplayDto>> GetWorkers2(OwnerParameters ownerParameters)
        {
            var workers = await _context.Workers.ToListAsync();
            List<WorkerDisplayDto> workersToReturn = _mapper.Map<List<WorkerDisplayDto>>(workers);

            return PagedList<WorkerDisplayDto>.ToPagedList(workersToReturn, ownerParameters.PageNumber, ownerParameters.PageSize);
        }

        public async Task<PagedList<WorkerDisplayDto>> GetSearchedWorkers(OwnerParameters ownerParameters, string searchString)
        {
            searchString = searchString.ToLower();
            var workers = await _context.Workers.Where(x => x.Login.ToLower().Contains(searchString) || x.FirstName.ToLower().Contains(searchString) || x.LastName.ToLower().Contains(searchString)).ToListAsync();
            List<WorkerDisplayDto> workersToReturn = _mapper.Map<List<WorkerDisplayDto>>(workers);

            return PagedList<WorkerDisplayDto>.ToPagedList(workersToReturn, ownerParameters.PageNumber, ownerParameters.PageSize);
        }

        public async Task<PagedList<WorkerDisplayDto>> GetSearchedWorkersAndSorted(OwnerParameters ownerParameters, string searchString, int sortBy)
        {
            searchString = searchString.ToLower();
            List<WorkerDisplayDto> workersToReturn;
            var workers = await _context.Workers.Where(x => x.Login.ToLower().Contains(searchString) || x.FirstName.ToLower().Contains(searchString) || x.LastName.ToLower().Contains(searchString)).OrderBy(x => x.Login).ToListAsync();

            switch (sortBy)
            {
                case 1:

                    workersToReturn = _mapper.Map<List<WorkerDisplayDto>>(workers);
                    workersToReturn = workersToReturn.OrderBy(x => x.Login).ToList();
                    return PagedList<WorkerDisplayDto>.ToPagedList(workersToReturn, ownerParameters.PageNumber, ownerParameters.PageSize);


                case 2:
                    workersToReturn = _mapper.Map<List<WorkerDisplayDto>>(workers);
                    workersToReturn = workersToReturn.OrderByDescending(x => x.Login).ToList();
                    return PagedList<WorkerDisplayDto>.ToPagedList(workersToReturn, ownerParameters.PageNumber, ownerParameters.PageSize);


                case 3:
                    workersToReturn = _mapper.Map<List<WorkerDisplayDto>>(workers);
                    workersToReturn = workersToReturn.OrderBy(x => x.FirstName).ToList();
                    return PagedList<WorkerDisplayDto>.ToPagedList(workersToReturn, ownerParameters.PageNumber, ownerParameters.PageSize);

                case 4:
                    workersToReturn = _mapper.Map<List<WorkerDisplayDto>>(workers);
                    workersToReturn = workersToReturn.OrderByDescending(x => x.FirstName).ToList();
                    return PagedList<WorkerDisplayDto>.ToPagedList(workersToReturn, ownerParameters.PageNumber, ownerParameters.PageSize);

                case 5:
                    workersToReturn = _mapper.Map<List<WorkerDisplayDto>>(workers);
                    workersToReturn = workersToReturn.OrderBy(x => x.LastName).ToList();
                    return PagedList<WorkerDisplayDto>.ToPagedList(workersToReturn, ownerParameters.PageNumber, ownerParameters.PageSize);

                case 6:
                    workersToReturn = _mapper.Map<List<WorkerDisplayDto>>(workers);
                    workersToReturn = workersToReturn.OrderByDescending(x => x.LastName).ToList();
                    return PagedList<WorkerDisplayDto>.ToPagedList(workersToReturn, ownerParameters.PageNumber, ownerParameters.PageSize);




            }

            workersToReturn = _mapper.Map<List<WorkerDisplayDto>>(workers);

            return PagedList<WorkerDisplayDto>.ToPagedList(workersToReturn, ownerParameters.PageNumber, ownerParameters.PageSize);



        }






        
    }
}