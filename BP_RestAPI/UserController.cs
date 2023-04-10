using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BP_RestAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        public UserController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> Get()
        {
            return await _dbContext.Users.ToListAsync();
        }

        [HttpGet("/secret/{id}")]
        public async Task<ActionResult<UserModel>> GetSecret(int id)
        {
            var UserModel = await _dbContext.Users.FindAsync(id);
            if (UserModel == null)
            {
                return NotFound();
            }
            return UserModel;
        }

        [HttpGet("/user/LoginUser/{username}/{password}")]
        public async Task<ActionResult<UserModel>> LoginUser(string username, string password)
        {
            var UserModel = await _dbContext.Users.FirstOrDefaultAsync(x => x.Nickname == username && x.UserPassword == password);
            if (UserModel == null)
            {
                return NotFound();
            }
            return Ok(UserModel);
        }

        [HttpPost("/user/RegisterUser")]
        public async Task<ActionResult<UserModel>> PostNew(UserBase UserBase)
        {
            UserModel userModel = new UserModel();
            userModel.Nickname = UserBase.Nickname;
            userModel.UserPassword = UserBase.UserPassword;
            _dbContext.Users.Add(userModel);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = userModel.Id }, userModel);
        }
        [HttpPost("/user/UpdateMedal/{id}/{medal}/{add}")]
        public async Task<ActionResult<UserModel>> PostUpdateMedal(int id, int medal, bool add)
        {
            var UserModel = await _dbContext.Users.FindAsync(id);
            if (UserModel == null)
            {
                return NotFound();
            }
            switch(medal)
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
        [HttpPost("/user/UpdateAvatar/{id}/{newAvatar}")]
        public async Task<ActionResult<UserModel>> PostUpdateAvatar(int id, int newAvatar)
        {
            var UserModel = await _dbContext.Users.FindAsync(id);
            if (UserModel == null)
            {
                return NotFound();
            }
            if(newAvatar > 0 && newAvatar < 7)
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
        [HttpPost("/user/UpdateTeacher/{id}/{changeTeacherStatus}")]
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
        [HttpPost("/user/UpdateNickname/{id}/{newNickname}")]
        public async Task<ActionResult<UserModel>> PostUpdateNickname(int id, string newNickname)
        {
            var UserModel = await _dbContext.Users.FindAsync(id);
            if (UserModel == null)
            {
                return NotFound();
            }
            UserModel.Nickname= newNickname;
            _dbContext.Users.Update(UserModel);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("/user/UpdateFirstname/{id}/{newFirstname}")]
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
        [HttpPost("/user/UpdateSurname/{id}/{newSurname}")]
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
        [HttpPost("/user/UpdatePassword/{id}/{newPassword}")]
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
        [HttpPost("/user/UpdateTitle/{id}/{titleNo}")]
        public async Task<ActionResult<UserModel>> PostUpdateTitle(int id, int titleNo)
        {
            var UserModel = await _dbContext.Users.FindAsync(id);
            if (UserModel == null)
            {
                return NotFound();
            }
            switch(titleNo)
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
