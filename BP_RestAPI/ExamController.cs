using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        [HttpGet("/exam/Find/{classId}")]
        public async Task<ActionResult<ExamsModel>> GetExamsInClass(int classId)
        {
            var ExamModel = _dbContext.ExamSets.Where(x => x.ClassID == classId);
            if (ExamModel == null)
            {
                return NotFound();
            }
            return Ok(ExamModel);
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
            var existingData = _dbContext.ExamSets
                .FirstOrDefault(x => x.ClassID == exam.ClassID && x.PIN == exam.PIN);

            while (existingData != null)
            {
                string newPIN = GenerateNewPIN();
                existingData = _dbContext.ExamSets
                    .FirstOrDefault(x => x.ClassID == exam.ClassID && x.PIN == newPIN);
                exam.PIN = newPIN;
            }

            _dbContext.ExamSets.Add(exam);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = exam.Id }, exam);
        }
        private string GenerateNewPIN()
        {
            Random rnd = new Random();
            string vygPin = "";
            for (int i = 0; i < 5; i++)
            {
                vygPin += Convert.ToString(rnd.Next(1, 10));
            }
            return vygPin;
        }
    }
}
