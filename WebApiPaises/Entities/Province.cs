using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPaises.Entities
{
    public class Province
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Pais")]
        public int PaisId { get; set; }
        public virtual Pais Pais { get; set; }
    }
}
