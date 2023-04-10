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
            return await _dbContext.Exams.ToListAsync();
        }

        [HttpGet("/exam/Find/{id}")]
        public async Task<ActionResult<ExamsModel>> GetSecret(int id)
        {
            var ExamModel = await _dbContext.Exams.FindAsync(id);
            if (ExamModel == null)
            {
                return NotFound();
            }
            return ExamModel;
        }
        [HttpPost("/exam/CreateOwn/")]
        public async Task<ActionResult<ExamsModel>> CreateOwnExam(ExamsModel exam)
        {
            _dbContext.Exams.Add(exam);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = exam.Id }, exam);
        }

        [HttpPost("/exam/CreateGen/{username}/{password}")]
        public async Task<ActionResult<ExamsModel>> CreateGenExam(string username, string password)
        {
            var UserModel = await _dbContext.Users.FirstOrDefaultAsync(x => x.Nickname == username && x.UserPassword == password);
            if (UserModel == null)
            {
                return NotFound();
            }
            return Ok(UserModel);
        }

        [HttpPost("/exam/RegisterUser")]
        public async Task<ActionResult<UserModel>> PostNew(UserBase UserBase)
        {
            UserModel userModel = new UserModel();
            userModel.Nickname = UserBase.Nickname;
            userModel.UserPassword = UserBase.UserPassword;
            _dbContext.Users.Add(userModel);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = userModel.Id }, userModel);
        }
        [HttpPost("/exam/UpdateMedal/{id}/{medal}/{add}")]
        public async Task<ActionResult<UserModel>> PostUpdateMedal(int id, int medal, bool add)
        {
            var UserModel = await _dbContext.Users.FindAsync(id);
            if (UserModel == null)
            {
                return NotFound();
            }
            switch (medal)
            {
                case 1:
                    if (add) UserModel.BronzeMedals++; else UserModel.BronzeMedals--;
                    break;
                case 2:
                    if (add) UserModel.SilverMedals++; else UserModel.SilverMedals--;
                    break;
                case 3:
                    if (add) UserModel.GoldMedals++; else UserModel.GoldMedals--;
                    break;
                default:
                    return BadRequest();

            }
            _dbContext.Users.Update(UserModel);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("/exam/UpdateAvatar/{id}/{newAvatar}")]
        public async Task<ActionResult<UserModel>> PostUpdateAvatar(int id, int newAvatar)
        {
            var UserModel = await _dbContext.Users.FindAsync(id);
            if (UserModel == null)
            {
                return NotFound();
            }
            if (newAvatar > 0 && newAvatar < 7)
            {
                UserModel.Avatar = newAvatar;
            }
            else
            {
                return BadRequest();
            }
            _dbContext.Users.Update(UserModel);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("/exam/UpdateTeacher/{id}/{changeTeacherStatus}")]
        public async Task<ActionResult<UserModel>> PostUpdateIsTeacher(int id, bool changeTeacherStatus)
        {
            var UserModel = await _dbContext.Users.FindAsync(id);
            if (UserModel == null)
            {
                return NotFound();
            }
            UserModel.isTeacher = changeTeacherStatus ? 1 : 0;
            _dbContext.Users.Update(UserModel);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("/exam/UpdateNickname/{id}/{newNickname}")]
        public async Task<ActionResult<UserModel>> PostUpdateNickname(int id, string newNickname)
        {
            var UserModel = await _dbContext.Users.FindAsync(id);
            if (UserModel == null)
            {
                return NotFound();
            }
            UserModel.Nickname = newNickname;
            _dbContext.Users.Update(UserModel);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("/exam/UpdateFirstname/{id}/{newFirstname}")]
        public async Task<ActionResult<UserModel>> PostUpdateFirstname(int id, string newFirstname)
        {
            var UserModel = await _dbContext.Users.FindAsync(id);
            if (UserModel == null)
            {
                return NotFound();
            }
            UserModel.Firstname = newFirstname;
            _dbContext.Users.Update(UserModel);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("/exam/UpdateSurname/{id}/{newSurname}")]
        public async Task<ActionResult<UserModel>> PostUpdateSurname(int id, string newSurname)
        {
            var UserModel = await _dbContext.Users.FindAsync(id);
            if (UserModel == null)
            {
                return NotFound();
            }
            UserModel.Surname = newSurname;
            _dbContext.Users.Update(UserModel);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("/exam/UpdatePassword/{id}/{newPassword}")]
        public async Task<ActionResult<UserModel>> PostUpdatePassword(int id, string newPassword)
        {
            var UserModel = await _dbContext.Users.FindAsync(id);
            if (UserModel == null)
            {
                return NotFound();
            }
            UserModel.UserPassword = newPassword;
            _dbContext.Users.Update(UserModel);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("/exam/UpdateTitle/{id}/{titleNo}")]
        public async Task<ActionResult<UserModel>> PostUpdateTitle(int id, int titleNo)
        {
            var UserModel = await _dbContext.Users.FindAsync(id);
            if (UserModel == null)
            {
                return NotFound();
            }
            switch (titleNo)
            {
                case 1:
                    UserModel.Title1 = 1;
                    break;
                case 2:
                    UserModel.Title2 = 1;
                    break;
                case 3:
                    UserModel.Title3 = 1;
                    break;
                case 4:
                    UserModel.Title4 = 1;
                    break;
                case 5:
                    UserModel.Title5 = 1;
                    break;
                case 6:
                    UserModel.Title6 = 1;
                    break;
                default: return BadRequest();
            }
            _dbContext.Users.Update(UserModel);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
