using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BP_RestAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        public ClassesController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassModel>>> Get()
        {
            return await _dbContext.Classes.ToListAsync();
        }
        [HttpGet("/class/Find/{id}")]
        public async Task<ActionResult<ClassModel>> GetClass(int id)
        {
            var ClassModel = await _dbContext.Classes.FindAsync(id);
            if (ClassModel == null)
            {
                return NotFound();
            }
            return ClassModel;
        }
        [HttpGet("/class/FindByName/{name}/{grade}")]
        public async Task<ActionResult<ClassModel>> GetClassByName(string name, int grade)
        {
            var ClassModel = await _dbContext.Classes.FirstOrDefaultAsync(x => x.SchoolName == name.Trim() && x.Grade == grade);
            if (ClassModel == null)
            {
                return NotFound();
            }
            return ClassModel;
        }
        [HttpPost("/class/Create/")]
        public async Task<ActionResult<ClassModel>> CreateOwnClass(ClassModel addedClass)
        {
            _dbContext.Classes.Add(addedClass);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = addedClass.Id }, addedClass);
        }
        [HttpDelete("/class/Delete/{name}/{grade}")]
        public async Task<ActionResult<ClassModel>> DeleteClass(string name, int grade)
        {
            var deletedClass = await _dbContext.Classes.FirstOrDefaultAsync(x => x.SchoolName == name.Trim() && x.Grade == grade);
            if(deletedClass == null)
            {
                return NotFound();
            }
            else
            {
                _dbContext.Classes.Remove(deletedClass);
                await _dbContext.SaveChangesAsync();
            }
            return NoContent();
        }
    }
}
