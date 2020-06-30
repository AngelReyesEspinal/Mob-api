using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.Services.BaseRepository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAllPolicy")]
    public class BaseController<T> : ControllerBase where T : class
    {
        protected readonly IBaseRepository<T> _baseRepository;
        public BaseController(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        [HttpGet]
        public virtual IActionResult Get()
        {
            var data = _baseRepository.GetAll();
            return Ok(data.ToList());
        }

        [HttpGet("{id}")]
        public virtual IActionResult Get(int id)
        {
            return Ok(_baseRepository.GetById(id));
        }

        [HttpPost]
        public virtual IActionResult Post([FromBody] T entity)
        {
            var createdEntity = _baseRepository.Create(entity);
            return Ok(createdEntity);
        }

        [HttpPut]
        public virtual IActionResult Put([FromBody] T entity)
        {
            _baseRepository.Update(entity);
            return Ok();
        }

        [HttpDelete("{id}")]
        public virtual IActionResult Delete(int id)
        {
            _baseRepository.Delete(id);
            return Ok();
        }
    }
}
