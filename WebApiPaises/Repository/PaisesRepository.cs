using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPaises.Connexion;
using WebApiPaises.Entities;

namespace WebApiPaises.Repository
{
    public class PaisesRepository : IPasesRepository
    {
        private readonly ConnectDbContext _context;
        public PaisesRepository(ConnectDbContext pasesRepository)
        {
            _context = pasesRepository;
        }
        //agregar
        public async Task<Pais> Add(Pais pais)
        {
            var newPais = await _context.Paises.AddAsync(pais);
            await _context.SaveChangesAsync();

            return newPais.Entity;

        }
        //elimina
        public async Task<bool> Delete(int id)
        {
          
            var pais = await _context.Paises.FindAsync(id);

            if (pais == null)
                return false;

            _context.Paises.Remove(pais);
            await _context.SaveChangesAsync();

            return true;
        }
        //consulta uno
        public async Task<Pais> Get(int id) => 
            await _context.Paises.Include(a=>a.Provinces).FirstOrDefaultAsync(x=>x.Id==id);
        
        //obtner todos
        public async Task<IEnumerable<Pais>> Index() => 
            await _context.Paises.Include(x=>x.Provinces).ToListAsync();

        //actualizar
        public async Task<Pais> Update(int id, Pais pais)
        {
            if (id != pais.Id) return null;

            _context.Entry(pais).State =EntityState.Modified;
            await _context.SaveChangesAsync();

            return pais;
        }
    }
}
