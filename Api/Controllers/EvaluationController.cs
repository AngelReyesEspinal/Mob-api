using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BL.Dtos;
using BL.Services.BaseRepository;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("GetByIdFull/{id}")]
        public async Task<IActionResult> GetByIdFull(int id)
        {
            var evaluation = _baseRepository.GetContext()
                                                .Evaluations
                                                .AsQueryable()
                                                .Include(x => x.Questions).ThenInclude(x => x.Document)
                                                .Include(x => x.Questions).ThenInclude(x => x.QuestionAnswerOptions)
                                                .FirstOrDefault(x => x.Id == id);

            var questionsDto = new List<QuestionDto>();

            foreach (var question in evaluation.Questions) // hay que buscarle la vuelta con el mapper idk
            {
                var dto = new QuestionDto
                {
                    Id = question.Id,
                    UserId = question.UserId,
                    Name = question.Name,
                    Wildcard = question.Wildcard,
                    Img = await ConvertToBase64(question.Document.Path + question.Document.FileName),
                    EvaluationId = question.EvaluationId,
                    QuestionAnswerOptions = _mapper.Map<List<QuestionAnswerOptionDto>>(question.QuestionAnswerOptions)
                };
                questionsDto.Add(dto);
            }

            var dtos = _mapper.Map<EvaluationDto>(evaluation);
            dtos.QuestionsFrontEnd = questionsDto;
            return Ok(dtos);
        }

        [HttpGet("GetBySecretKey/{key}")]
        public IActionResult GetBySecretKey(string key)
        {
            var evaluations = _baseRepository.GetContext()
                                                .Evaluations
                                                .AsQueryable().Include(x => x.Questions)
                                                .Where(x => x.Subject.SecretKey == key);
            var dtos = _mapper.Map<List<EvaluationDto>>(evaluations);
            return Ok(dtos);
        }

        private async Task Save(IFormFile fileToSave, string folder)
        {
            using (var stream = new FileStream(path: folder, mode: FileMode.Create))
            {
                await fileToSave.CopyToAsync(stream);
            }
        }

        private async Task<string> ConvertToBase64(string path)
        {
            Byte[] byteArray = System.IO.File.ReadAllBytes(path).ToArray();
            string base64String = Convert.ToBase64String(byteArray);
            return "data:image/png;base64," + base64String;
        }
    }
}
