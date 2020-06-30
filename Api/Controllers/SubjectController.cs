using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BL.Services.BaseRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Api.Controllers
{
    public class SubjectController : BaseController<Subject>
    {
        private IHostingEnvironment _env;
        private readonly IBaseRepository<Document> _documentRepository;
        public SubjectController(IBaseRepository<Subject> baseRepository, IBaseRepository<Document> documentRepository, IHostingEnvironment env)
            : base(baseRepository)
        {
            _env = env;
            _documentRepository = documentRepository;
        }

        [HttpGet("userId/{id}")]
        public async Task<IActionResult> GetByUserId(int id)
        {
            var subject = await _baseRepository.GetContext().Subject.Include(x => x.Document).FirstOrDefaultAsync(x => x.UserId == id);
            var imagePath = subject.Document.Path + subject.Document.FileName;
            var img = await ConvertToBase64(imagePath);
            var base64 = "data:image/png;base64," + img;
            return Ok(base64);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] string name, IFormFile file)
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
                    UserId = 1
                });

                _baseRepository.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task Save(IFormFile fileToSave, string folder)
        {
            using (var stream = new FileStream(path: folder, mode: FileMode.Create))
            {
                await fileToSave.CopyToAsync(stream);
            }
        }

        private async Task<string> ConvertToBase64(string path) {
            Byte[] byteArray = System.IO.File.ReadAllBytes(path).ToArray();
            string base64String = Convert.ToBase64String(byteArray);
            return base64String;
        }

    }
}
