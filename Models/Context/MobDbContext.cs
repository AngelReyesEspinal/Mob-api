using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Context
{
    public class MobDbContext : BaseDbContext, IMobDbContext
    {
        public MobDbContext(DbContextOptions<MobDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionAnswerOption> QuestionAnswerOption { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<T> GetDbSet<T>() where T : class, IBase => Set<T>();
    }
}
