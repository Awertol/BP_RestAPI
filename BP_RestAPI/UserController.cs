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

        [HttpGet("/secret")]
        public async Task<ActionResult<UserModel>> Post(UserModel UserModel)
        {
            _dbContext.Users.Add(UserModel);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = UserModel.Id }, UserModel);
        }

        [HttpPost("/user/{id}")]
        public async Task<ActionResult<UserModel>> Get(int id)
        {
            var UserModel = await _dbContext.Users.FindAsync(id);
            if (UserModel == null)
            {
                return NotFound();
            }
            UserModel.UserPassword = "#####";
            return UserModel;
        }

        [HttpPost("/user/")]
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
            if(newAvatar > 0 && newAvatar < 8)
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
        [HttpPost("/user/UpdateNickname/{id}/{newFullname}")]
        public async Task<ActionResult<UserModel>> PostUpdateFullname(int id, string newFullname)
        {
            var UserModel = await _dbContext.Users.FindAsync(id);
            if (UserModel == null)
            {
                return NotFound();
            }
            UserModel.FullName = newFullname;
            _dbContext.Users.Update(UserModel);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
