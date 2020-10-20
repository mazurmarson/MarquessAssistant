using System.Threading.Tasks;

namespace MarqueesAssistant.API.Data
{
    public interface IGenRepo
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;

         void Edit<T>(T entity) where T: class;
         Task<bool> SaveAll();  
    }
}