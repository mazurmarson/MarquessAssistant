using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarqueesAssistant.API.Dtos;
using MarqueesAssistant.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarqueesAssistant.API.Data
{
    public class MarqueeRepo : GenRepo, IMarqueeRepo
    {
        private readonly DataContext _context;
        public MarqueeRepo(DataContext context) : base(context)
        {
            _context = context;
        }
        // public async Task<Marquee> AddMarquee(Marquee marquee)
        // {
        //     await _context.Marquees.AddAsync(marquee);
        //     await _context.SaveChangesAsync();
        //     return marquee;
        // }



        // public async Task<IActionResult> DeleteMarquee(int id)
        // {
        //     Marquee marquee = await _context.Marquees.FirstOrDefaultAsync(x => x.Id == id);

        //     _context.Marquees.Remove(marquee);
        //     await _context.SaveChangesAsync();


        //     return Ok();
        // }



        public async Task<IEnumerable<Marquee>> GetMarquees()
        {
            
            var marquees = await _context.Marquees.ToListAsync();
            return marquees;
        }

        public async Task<Marquee> GetMarquee(int id)
        {
            
            var marquee = await _context.Marquees.FirstOrDefaultAsync(x => x.Id == id);
            return marquee;
        }


    }
}