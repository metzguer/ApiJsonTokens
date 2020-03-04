using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPaises.Entities;

namespace WebApiPaises.Mappers
{
    public static class ProvinceMapper
    {
        public static Province setMap (Province province){
            return new Province() {
                Id=province.Id,
                Name = province.Name,
                PaisId= province.PaisId
            };
        }
    }
}
