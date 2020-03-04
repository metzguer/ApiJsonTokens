using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPaises.Entities
{
    public class Pais
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }

        public virtual ICollection<Province> Provinces { get; set; }
    }
}
