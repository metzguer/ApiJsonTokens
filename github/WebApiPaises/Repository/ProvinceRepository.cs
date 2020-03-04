using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPaises.Connexion;
using WebApiPaises.Entities;

namespace WebApiPaises.Repository
{
    public class ProvinceRepository : IProvinceRepository
    {
        private readonly ConnectDbContext _context;
        public ProvinceRepository(ConnectDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<Province> Add(Province province)
        {
            var newProvince = await _context.Provinces.AddAsync(province);
            await _context.SaveChangesAsync();
            return newProvince.Entity;
        }

        public async Task<bool> Delete(int id)
        {
            var province = await _context.Provinces.FindAsync(id);
            if (province == null) return false;
            _context.Provinces.Remove(province);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Province> Get(int id) => 
            await _context.Provinces.Include(x => x.Pais).FirstOrDefaultAsync(x=>x.Id==id);


        public async Task<IEnumerable<Province>> Index() => await _context.Provinces.ToListAsync();
    

        public async Task<Province> Update(int id, Province province)
        {
            _context.Entry(province).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return province;
        }
        
    }
}
