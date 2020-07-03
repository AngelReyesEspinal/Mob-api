using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BL.Dtos;
using BL.Services.BaseRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Api.Controllers
{
    public class EvaluationController : BaseController<Evaluation>
    {
        private readonly IMapper _mapper;

        public EvaluationController(IBaseRepository<Evaluation> baseRepository, IMapper mapper)
            : base(baseRepository)
        {
            _mapper = mapper;
        }

        [HttpGet("GetBySubjectId/{id}")]
        public IActionResult GetByUserId(int id)
        {
            var evaluations =  _baseRepository.GetContext()
                                                .Evaluations
                                                .AsQueryable().Include(x => x.Questions)
                                                .Where(x => x.SubjectId == id);
            var dtos = _mapper.Map<List<EvaluationDto>>(evaluations);
            return Ok(dtos);
        }

    }
}
