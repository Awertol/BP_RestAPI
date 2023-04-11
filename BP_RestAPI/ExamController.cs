using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BP_RestAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        public ExamController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamsModel>>> Get()
        {
            return await _dbContext.ExamSets.ToListAsync();
        }

        [HttpGet("/exam/Find/{pin}/{classId}")]
        public async Task<ActionResult<ExamsModel>> GetSecret(string pin, int classId)
        {
            var ExamModel = await _dbContext.ExamSets.FirstOrDefaultAsync(x => x.PIN == pin && x.ClassID == classId);
            if (ExamModel == null)
            {
                return NotFound();
            }
            return ExamModel;
        }
        [HttpPost("/exam/Create/")]
        public async Task<ActionResult<ExamsModel>> CreateOwnExam(ExamsModel exam)
        {
            _dbContext.ExamSets.Add(exam);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = exam.Id }, exam);
        }
    }
}
