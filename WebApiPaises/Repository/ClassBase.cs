using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPaises.Connexion;

namespace WebApiPaises.Repository
{
    public static class ClassBase
    {
        
        public static async Task<T> updateModel(ConnectDbContext _context, T model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }
    }
    public class T  { }
}
