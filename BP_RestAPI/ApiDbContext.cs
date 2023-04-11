using Microsoft.EntityFrameworkCore;

namespace BP_RestAPI
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<ExamsModel> Exams { get; set; }
        public DbSet<ExamAnswersModel> ExamAnswer { get; set; }
        public DbSet<ClassModel> Classes { get; set; }
    }
}
