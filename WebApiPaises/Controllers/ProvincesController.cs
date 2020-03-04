using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiPaises.Entities;
using WebApiPaises.Repository;

namespace WebApiPaises.Controllers
{
    [Route("api/paises/{PaisId}/[controller]")]
    [ApiController]
    public class ProvincesController : ControllerBase
    {
        private readonly IProvinceRepository _repository;
        public ProvincesController(IProvinceRepository provinceRepository)
        {
            _repository = provinceRepository;
        }
        [HttpGet]
        public async Task<ActionResult> Index() =>  Ok( await _repository.Index() );

        [HttpPost]
        public async Task<ActionResult> Add(Province province) =>
            (ModelState.IsValid && await _repository.Add(province) != null) ? (ActionResult)  Ok(province) : BadRequest();

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id) =>
             (await _repository.Get(id) != null) ? (ActionResult) Ok(await _repository.Get(id)) : NotFound();
        
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Province province) => 
            (!ModelState.IsValid || id != province.Id || await _repository.Update(id, province) == null) ? 
            (ActionResult) BadRequest(ModelState.Values) : Ok(province);
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) =>
            await _repository.Delete(id) ? (ActionResult) Ok(true): NotFound();
    }
}