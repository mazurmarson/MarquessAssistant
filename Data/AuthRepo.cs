using System;
using System.Text;
using System.Threading.Tasks;
using MarqueesAssistant.API.Controllers;
using MarqueesAssistant.API.Dtos;
using Microsoft.EntityFrameworkCore;


namespace MarqueesAssistant.API.Data
{
    public class AuthRepo : IAuthRepo
    {
        private readonly DataContext _context;
        public AuthRepo(DataContext context)
        {
            _context = context;
        }
        public async Task<Worker> Login(string login, string password)
        {
            var worker = await _context.Workers.FirstOrDefaultAsync(w => w.Login == login);

            if (worker == null)
                return null;

            if (!VerifyPasswordHash(password, worker.PasswordHash, worker.PasswordSalt))
                return null;

            return worker;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {

                var computtedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computtedHash.Length; i++)
                {
                    if (computtedHash[i] != passwordHash[i])
                    {
                        return false;
                    }

                }

                return true;

            }
        }

        public async Task<Worker> Register(Worker worker, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePassword(password, out passwordHash, out passwordSalt);

            worker.PasswordHash = passwordHash;
            worker.PasswordSalt = passwordSalt;

            await _context.Workers.AddAsync(worker);
            await _context.SaveChangesAsync();

            return worker;
        }

        public async Task<Worker> EditWorker(Worker worker, string newPassword)
        {
            byte[] passwordHash, passwordSalt;
            CreatePassword(newPassword, out passwordHash, out passwordSalt);

            worker.PasswordHash = passwordHash;
            worker.PasswordSalt = passwordSalt;
            _context.Workers.Update(worker);
            await _context.SaveChangesAsync();

            return worker;
        }



        public async Task<bool> WorkerIsExist(string login)
        {
            if (await _context.Workers.AnyAsync(x => x.Login == login))
            {
                return true;
            }


            return false;
        }

        public async Task<bool> CanEditWorker(WorkerEditDto workerEditDto)
        {
            if (await _context.Workers.AnyAsync(x => x.Login == workerEditDto.Login && x.Id == workerEditDto.Id))
            {
                return true;
            }
            return false;
        }

        private void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }


        }
    }
}