using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BP_RestAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamAnswerController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        public ExamAnswerController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("/answer/FindAll/")]
        public async Task<ActionResult<IEnumerable<ExamAnswersModel>>> Get()
        {
            return await _dbContext.ExamAnswer.ToListAsync();
        }
        [HttpGet("/answer/Find/{userID}/{examID}")]
        public async Task<ActionResult<IEnumerable<ExamAnswersModel>>> GetAll(int userID, int examID)
        {
            var answers = _dbContext.ExamAnswer.Where(x => x.UserID == userID && x.ExamID == examID);
            return Ok(answers);
        }
        [HttpPost("/answer/Create/")]
        public async Task<ActionResult<ExamAnswersModel>> AddAnswer(ExamAnswersModel answer)
        {

            _dbContext.ExamAnswer.Add(answer);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = answer.AnswerID }, answer);
        }
    }
}
