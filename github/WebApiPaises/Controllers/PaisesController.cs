using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiPaises.Entities;
using WebApiPaises.Repository;

namespace WebApiPaises.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaisesController : ControllerBase
    {
        private readonly IPasesRepository _repository;
        public PaisesController(IPasesRepository pasesRepository)
        {
            _repository = pasesRepository;               
        }
        [HttpGet]
        public async Task<ActionResult> Index() {

            var countrys = await _repository.Index();

            return (countrys.Count()>0) ? Ok(countrys) : Ok("No existen registros en Base de Datos");
        }
        [HttpPost]
        public async Task<ActionResult> Add(Pais pais) {

            if (!ModelState.IsValid)  return BadRequest(ModelState);

            var newPais = await _repository.Add(pais);
            
            return Created("https://localhost:44373/api/paises/"+newPais.Id, newPais);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {

            var country = await _repository.Get(id);

            if (country == null)  return NotFound();

            return Ok(country);

        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Pais pais) {

            if (!ModelState.IsValid || id!=pais.Id)
                return BadRequest(ModelState);

            var newCountry = await _repository.Update(id, pais);

            if (newCountry == null) return NotFound();

            return Ok(newCountry);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) {
            var oper = await _repository.Delete(id);

            if (!oper) return BadRequest();

            return NoContent();
        }
    }
}