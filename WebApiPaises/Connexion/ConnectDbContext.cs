using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPaises.Entities;

namespace WebApiPaises.Connexion
{
    public class ConnectDbContext : IdentityDbContext<User>
    {
        public ConnectDbContext(DbContextOptions<ConnectDbContext> options):base(options)
        {

        }

        public DbSet<Pais> Paises { get; set; }

        public DbSet<Province> Provinces { get; set; }

        public DbSet<User> Users { get; set; }

    }
}
