using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace BP_RestAPI
{
    public class ApiDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExamAnswersModel>()
                .ToTable(tb => tb.HasTrigger("TriggerUsers"));
            modelBuilder.Entity<ClassModel>()
                .ToTable(tb => tb.HasTrigger("TriggerClasses"));
            modelBuilder.Entity<ExamsModel>()
                .ToTable(tb => tb.HasTrigger("TriggerExams"));
            modelBuilder.Entity<ExamAnswersModel>()
                .ToTable(tb => tb.HasTrigger("TriggerAnswers"));
        }
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ExamsModel> ExamSets { get; set; }
        public DbSet<ExamAnswersModel> ExamAnswer { get; set; }
        public DbSet<ClassModel> Classes { get; set; }
    }
}
