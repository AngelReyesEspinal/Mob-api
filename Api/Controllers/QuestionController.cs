using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BL.Dtos;
using BL.Mappings.Extensions;
using BL.Services.BaseRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Api.Controllers
{
    // todo: refactorizar
    public class QuestionController : BaseController<Question>
    {
        private IHostingEnvironment _env;
        private readonly IBaseRepository<Document> _documentRepository;
        private readonly IMapper _mapper;

        public QuestionController(IBaseRepository<Question> baseRepository, IBaseRepository<Document> documentRepository, IHostingEnvironment env, IMapper mapper)
            : base(baseRepository)
        {
            _env = env;
            _mapper = mapper;
            _documentRepository = documentRepository;
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var question = _baseRepository.GetContext()
                                               .Questions
                                               .AsQueryable()
                                               .Include(x => x.QuestionAnswerOptions)
                                               .Include(x => x.Document)
                                               .FirstOrDefault(x => x.Id == id);
            var dto = new QuestionDto
            {
                Id = question.Id,
                Wildcard = question.Wildcard,
                UserId = question.UserId,
                Name = question.Name,
                FileName = question.Document.OriginalName,
                EvaluationId = question.EvaluationId,
                Img = await ConvertToBase64(question.Document.Path + question.Document.FileName),
                QuestionAnswerOptions = _mapper.Map<List<QuestionAnswerOptionDto>>(question.QuestionAnswerOptions)
            };
            return Ok(dto);
        }

        [HttpGet("GetByEvaluationId/{id}")]
        public async Task<IActionResult> GetByUserId(int id)
        {
            var questions = _baseRepository.GetContext()
                                                .Questions
                                                .AsQueryable()
                                                .Include(x => x.QuestionAnswerOptions)
                                                .Include(x => x.Document)
                                                .Where(x => x.EvaluationId == id);
                
            var dtos = new List<QuestionDto>();
            
            foreach (var question in questions) // hay que buscarle la vuelta con el mapper idk
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
                dtos.Add(dto);
            }

            return Ok(dtos);
        }

        [HttpPost("question")]
        public async Task<IActionResult> Upload([FromForm] string userId, [FromForm] string name, [FromForm] string pista, [FromForm] string evaluationId, [FromForm]  IFormFile file)
        {
            if (file == null)
                return UnprocessableEntity();

            try
            {
                var path = _env.WebRootPath + "\\Upload\\";
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(path, fileName);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                await Save(file, filePath);

                var documentId = _documentRepository.Create(new Document
                {
                    OriginalName = file.FileName,
                    FileName = fileName,
                    Path = path
                }).Id;

                _documentRepository.SaveChanges();

                var createdQuestion = _baseRepository.Create(new Question
                {
                    Name = name,
                    Wildcard = pista,
                    EvaluationId = Convert.ToInt32(evaluationId),
                    DocumentId = documentId,
                    UserId = Convert.ToInt32(userId)
                });

                _baseRepository.SaveChanges();

                return Ok(createdQuestion);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("answers")]
        public async Task<IActionResult> Answers([FromBody] Question question)
        {
            var questionDb = _baseRepository.GetById(question.Id);
            questionDb.QuestionAnswerOptions = question.QuestionAnswerOptions;
            _baseRepository.SaveChanges();
            return Ok();
        }

        [HttpPost("updateSubject")]
        public async Task<IActionResult> UploadUpdate([FromForm] string id, [FromForm] string name, [FromForm] string pista, [FromForm] IFormFile file)
        {
            var subjectId = Convert.ToInt32(id);
            var question = _baseRepository.GetById(subjectId);

            question.Name = name;
            question.Wildcard = pista;

            if (file != null)
            {
                try
                {
                    var path = _env.WebRootPath + "\\Upload\\";
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(path, fileName);

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    await Save(file, filePath);

                    var documentId = _documentRepository.Create(new Document
                    {
                        OriginalName = file.FileName,
                        FileName = fileName,
                        Path = path
                    }).Id;

                    _documentRepository.SaveChanges();
                    question.DocumentId = documentId;
                    _baseRepository.SaveChanges();

                    return Ok();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            else
                _baseRepository.SaveChanges();

            return Ok();
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
