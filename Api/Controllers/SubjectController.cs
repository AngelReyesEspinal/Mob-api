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
    public class SubjectController : BaseController<Subject>
    {
        private IHostingEnvironment _env;
        private readonly IBaseRepository<Document> _documentRepository;
        private readonly IMapper _mapper;

        public SubjectController(IBaseRepository<Subject> baseRepository, IBaseRepository<Document> documentRepository, IHostingEnvironment env, IMapper mapper)
            : base(baseRepository)
        {
            _env = env;
            _mapper = mapper;
            _documentRepository = documentRepository;
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var subject = await _baseRepository.GetContext()
                                               .Subject.Include(x => x.Document)
                                               .FirstOrDefaultAsync(x => x.Id == id);
            var dto = new SubjectDto
            {
                Id = subject.Id,
                UserId = subject.UserId,
                Name = subject.Name,
                FileName = subject.Document.OriginalName,
                Logo = await ConvertToBase64(subject.Document.Path + subject.Document.FileName),
                SecretKey = subject.SecretKey
            };
            return Ok(dto);
        }

        [HttpGet("GetBySecretKey/{key}")]
        public async Task<IActionResult> GetBySecretKey(string key)
        {
            var subject = await _baseRepository.GetContext()
                                               .Subject.Include(x => x.Document)
                                               .FirstOrDefaultAsync(x => x.SecretKey == key);
            var dto = new SubjectDto
            {
                Id = subject.Id,
                UserId = subject.UserId,
                Name = subject.Name,
                FileName = subject.Document.OriginalName,
                Logo = await ConvertToBase64(subject.Document.Path + subject.Document.FileName),
                SecretKey = subject.SecretKey
            };
            return Ok(dto);
        }

        [HttpGet("userId/{id}")]
        public async Task<IActionResult> GetByUserId(int id)
        {
            var dtos = new List<SubjectDto>();
            var subjects = await _baseRepository.GetContext()
                                               .Subject.Include(x => x.Document)
                                               .Where(x => x.UserId == id)
                                               .ToListAsync();
            foreach (var subject in subjects) // hay que buscarle la vuelta con el mapper idk
            {
                var dto = new SubjectDto
                {
                    Id = subject.Id,
                    UserId = subject.UserId,
                    Name = subject.Name,
                    Logo = await ConvertToBase64(subject.Document.Path + subject.Document.FileName),
                    SecretKey = subject.SecretKey
                };
                dtos.Add(dto);
            }
            return Ok(dtos);
        }

        [HttpPost("subject")]
        public async Task<IActionResult> Upload([FromForm] string name, [FromForm] string userId, IFormFile file)
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

                var documentId = _documentRepository.Create(new Document { 
                    OriginalName = file.FileName,
                    FileName = fileName,
                    Path = path
                }).Id;

                _documentRepository.SaveChanges();

                _baseRepository.Create(new Subject { 
                    Name = name,
                    SecretKey = Guid.NewGuid().ToString(),
                    DocumentId = documentId,
                    UserId = Convert.ToInt32(userId)
                });

                _baseRepository.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("updateSubject")]
        public async Task<IActionResult> UploadUpdate([FromForm] string id, [FromForm] string name, IFormFile file)
        {
            var subjectId = Convert.ToInt32(id);
            var subject = _baseRepository.GetById(subjectId);

            subject.Name = name;

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
                    subject.DocumentId = documentId;
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
