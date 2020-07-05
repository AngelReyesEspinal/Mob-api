using AutoMapper;
using BL.Dtos;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Mappings
{
    public class MobProfile : Profile
    {
        public MobProfile()
        {
            CreateMap<Subject, SubjectDto>();

            CreateMap<Evaluation, EvaluationDto>()
                .ForMember(x => x.QuestionQuantity, opt => opt.MapFrom(x => x.Questions.Count));

            CreateMap<QuestionAnswerOption, QuestionAnswerOptionDto>();
        }
    }
}
